using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using CrosswordSolverLib.RegexBlocks;

namespace CrosswordSolverLib.RegexClasses
{
    public class RegexParser
    {
        public RegularExpression Parse(string pattern)
        {
            var containers = new Stack<BlockContainer>();
            containers.Push(new BlockContainer { Type = BlockContainerType.AndContainer });
            RegexBlock characterContainer = null;
            bool expectGoupId = false;
            int position = 0;
            while (position < pattern.Length)
            {
                char c = pattern[position];
                var currentContainer = containers.Peek();
                if (currentContainer == null)
                    throw new ArgumentException("Invalid position for character" + c, "pattern");
                if (char.IsLetterOrDigit(c))
                {
                    if (expectGoupId)
                    {
                        var backreferenceBlock = new BackreferenceBlock(int.Parse(c.ToString(CultureInfo.InvariantCulture)));
                        currentContainer.PushBlock(backreferenceBlock);
                        expectGoupId = false;
                    }
                    else
                    {
                        if (characterContainer == null)
                        {
                            characterContainer = new TextBlock(string.Empty);
                        }

                        characterContainer = AddCharacter(characterContainer, c);
                    }
                }
                else
                {
                    switch (c)
                    {
                        case '.':
                            characterContainer = FlushCharacterContainer(characterContainer, currentContainer, false);

                            currentContainer.PushBlock(new AnyCharacterBlock());
                            break;
                        case '+':
                            {
                                characterContainer = FlushCharacterContainer(characterContainer, currentContainer, true);

                                RegexBlock previousBlock = currentContainer.PopBlock();
                                currentContainer.PushBlock(WrapToOneOrMoreBlock(previousBlock));
                            }

                            break;
                        case '*':
                            {
                                characterContainer = FlushCharacterContainer(characterContainer, currentContainer, true);

                                RegexBlock previousBlock = currentContainer.PopBlock();
                                currentContainer.PushBlock(WrapToZeroOrMoreBlock(previousBlock));
                            }

                            break;
                        case '?':
                            {
                                characterContainer = FlushCharacterContainer(characterContainer, currentContainer, true);
                                RegexBlock previousBlock = currentContainer.PopBlock();
                                currentContainer.PushBlock(WrapToZeroOrOneBlock(previousBlock));
                            }

                            break;
                        case '[':
                            characterContainer = FlushCharacterContainer(characterContainer, currentContainer, false);

                            characterContainer = new InclusiveSetBlock(string.Empty); 
                            break;
                        case ']':
                            {
                                if (characterContainer == null)
                                    throw new ArgumentException("Invalid position for character ]", "pattern");

                                currentContainer.PushBlock(characterContainer);
                                characterContainer = null;
                            }

                            break;
                        case '^':
                            var inclusiveSetBlock = characterContainer as InclusiveSetBlock;
                            if (inclusiveSetBlock == null)
                                throw new ArgumentException("Invalid position for character ^", "pattern");
                            characterContainer = new ExclusiveSetBlock(inclusiveSetBlock.Characters);
                            break;
                        case '(':
                            characterContainer = FlushCharacterContainer(characterContainer, currentContainer, false);

                            containers.Push(new BlockContainer());
                            break;
                        case ')':
                            {
                                characterContainer = FlushCharacterContainer(characterContainer, currentContainer, false);

                                if (currentContainer.Type == BlockContainerType.Undefined)
                                    currentContainer.Type = BlockContainerType.AndContainer;
                                GroupBlock containerBlock = ConvertToBlock(currentContainer);
                                containers.Pop();
                                var outerBlockContainer = containers.Peek();
                                if (outerBlockContainer == null)
                                    throw new ArgumentException("Invalid position for character ]", "pattern");
                                outerBlockContainer.PushBlock(containerBlock);
                            }

                            break;
                        case '|':
                            characterContainer = FlushCharacterContainer(characterContainer, currentContainer, false);

                            currentContainer.Type = BlockContainerType.OrContainer;
                            break;
                        case '\\':
                            characterContainer = FlushCharacterContainer(characterContainer, currentContainer, false);

                            expectGoupId = true;
                            break;
                        default:
                            throw new ArgumentException("Unrecognized symbol in pattern", "pattern");
                    }
                }

                position++;
            }

            var container = containers.Pop();

            if (characterContainer != null)
            {
                container.PushBlock(characterContainer);
            }

            GroupBlock block = ConvertToBlock(container);

            // Remove redundant group group if any.
            if (block.InnerBlocks.Count() == 1)
            {
                var orGroupBlock = block.InnerBlocks.First() as OrGroupBlock;
                if (orGroupBlock != null)
                    block = orGroupBlock;
            }

            return new RegularExpression(block);
        }

        private static RegexBlock AddCharacter(RegexBlock container, char c)
        {
            var textBlock = container as TextBlock;
            if (textBlock != null)
            {
                return new TextBlock(textBlock.Text + c);
            }

            var inclusiveSetBlock = container as InclusiveSetBlock;
            if (inclusiveSetBlock != null)
            {
                return new InclusiveSetBlock(inclusiveSetBlock.Characters + c);
            }

            var exclusiveSetBlock = container as ExclusiveSetBlock;
            if (exclusiveSetBlock != null)
            {
                return new ExclusiveSetBlock(exclusiveSetBlock.Characters + c);
            }

            return null;
        }

        private static char ExtractCharacter(ref RegexBlock container)
        {
            var text = GetText(container);
            var c = text.Last();
            var newText = text.Remove(text.Length - 1, 1);

            var textBlock = container as TextBlock;
            if (textBlock != null)
            {
                container = new TextBlock(newText);
                return c;
            }

            var inclusiveSetBlock = container as InclusiveSetBlock;
            if (inclusiveSetBlock != null)
            {
                container = new InclusiveSetBlock(newText);
                return c;
            }

            var exclusiveSetBlock = container as ExclusiveSetBlock;
            if (exclusiveSetBlock != null)
            {
                container = new ExclusiveSetBlock(newText);
                return c;
            }

            return default(char);
        }

        private static RegexBlock FlushCharacterContainer(RegexBlock characterContainer, BlockContainer currentContainer, bool excludeLastCharacter)
        {
            if (characterContainer != null)
            {
                if (excludeLastCharacter)
                {
                    var character = ExtractCharacter(ref characterContainer);
                    if (GetText(characterContainer).Length != 0)
                    {
                        currentContainer.PushBlock(characterContainer);
                    }

                    characterContainer = AddCharacter(new TextBlock(string.Empty), character);
                }

                currentContainer.PushBlock(characterContainer);
            }


            return null;
        }

        private static string GetText(RegexBlock container)
        {
            var textBlock = container as TextBlock;
            if (textBlock != null)
            {
                return textBlock.Text;
            }

            var setBlock = container as SetBlock;
            return setBlock != null ? setBlock.Characters : null;
        }

        private static GroupBlock ConvertToBlock(BlockContainer container)
        {
            switch (container.Type)
            {
                case BlockContainerType.AndContainer:
                    return new AndGroupBlock(container.Blocks.ToArray());
                case BlockContainerType.OrContainer:
                    return new OrGroupBlock(container.Blocks.ToArray());
                default:
                    return null;
            }
        }

        private static RegexBlock WrapToZeroOrOneBlock(RegexBlock block)
        {
            return new ZeroOrOneBlock(block);
        }

        private static ZeroOrMoreBlock WrapToZeroOrMoreBlock(RegexBlock block)
        {
            return new ZeroOrMoreBlock(block);
        }

        private static OneOrMoreBlock WrapToOneOrMoreBlock(RegexBlock block)
        {
            return new OneOrMoreBlock(block);
        }
    }
}
