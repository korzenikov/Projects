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
            bool expectGoupId = false;
            int position = 0;
            bool startNewBlock = false;
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
                        if (startNewBlock || !currentContainer.Blocks.Any() || !IsCharacterContainer(currentContainer.PeekBlock()))
                        {
                            currentContainer.PushBlock(new TextBlock(string.Empty));
                        }

                        var characterContainer = currentContainer.PopBlock();
                        currentContainer.PushBlock(AddCharacter(characterContainer, c));
                    }

                    startNewBlock = false;
                }
                else
                {
                    startNewBlock = false;
                    switch (c)
                    {
                        case '.':
                            currentContainer.PushBlock(new AnyCharacterBlock());
                            break;
                        case '+':
                            {
                                SplitTextBlock(currentContainer);

                                var previousBlock = currentContainer.PopBlock();
                                currentContainer.PushBlock(new OneOrMoreBlock(previousBlock));
                            }

                            break;
                        case '*':
                            {
                                SplitTextBlock(currentContainer);

                                var previousBlock = currentContainer.PopBlock();
                                currentContainer.PushBlock(new ZeroOrMoreBlock(previousBlock));
                            }

                            break;
                        case '?':
                            {
                                SplitTextBlock(currentContainer);

                                var previousBlock = currentContainer.PopBlock();
                                currentContainer.PushBlock(new ZeroOrOneBlock(previousBlock));
                            }

                            break;
                        case '[':
                            currentContainer.PushBlock(new InclusiveSetBlock(string.Empty));
                            break;
                        case ']':
                            startNewBlock = true;
                            break;
                        case '^':
                            {
                                var inclusiveSetBlock = currentContainer.PopBlock() as InclusiveSetBlock;
                                if (inclusiveSetBlock == null || inclusiveSetBlock.Characters.Any()) throw new ArgumentException("Invalid position for character ^", "pattern");
                                currentContainer.PushBlock(new ExclusiveSetBlock(inclusiveSetBlock.Characters));
                            }

                            break;
                        case '(':
                            containers.Push(new BlockContainer());
                            break;
                        case ')':
                            {
                                if (currentContainer.Type == BlockContainerType.Undefined) currentContainer.Type = BlockContainerType.AndContainer;
                                GroupBlock containerBlock = ConvertToBlock(currentContainer);
                                containers.Pop();
                                var outerBlockContainer = containers.Peek();
                                if (outerBlockContainer == null) throw new ArgumentException("Invalid position for character ]", "pattern");
                                outerBlockContainer.PushBlock(containerBlock);
                            }

                            break;
                        case '|':
                            startNewBlock = true;
                            currentContainer.Type = BlockContainerType.OrContainer;
                            break;
                        case '\\':
                            expectGoupId = true;
                            break;
                        default:
                            throw new ArgumentException("Unrecognized symbol in pattern", "pattern");
                    }
                }

                position++;
            }

            var container = containers.Pop();

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

        private static void SplitTextBlock(BlockContainer container)
        {
            var block = container.PopBlock();
            var textBlock = block as TextBlock;
            if (textBlock == null)
            {
                container.PushBlock(block);
                return;
            }

            var character = ExtractCharacter(ref textBlock);
            if (textBlock.Text.Any())
            {
                container.PushBlock(textBlock);
            }

            container.PushBlock(new TextBlock(string.Empty + character));
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

        private static char ExtractCharacter(ref TextBlock textBlock)
        {
            var text = textBlock.Text;
            var c = text.Last();
            var newText = text.Remove(text.Length - 1, 1);
            textBlock = new TextBlock(newText);
            return c;
        }

        private static bool IsCharacterContainer(RegexBlock block)
        {
            return block is TextBlock || block is SetBlock;
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
    }
}
