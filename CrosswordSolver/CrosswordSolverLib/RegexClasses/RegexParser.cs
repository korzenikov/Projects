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
            CharacterContainer characterContainer = null;
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
                            characterContainer = new CharacterContainer();
                        characterContainer.AddCharacter(c);
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

                            characterContainer = new CharacterContainer { Type = CharacterContainerType.InclusiveSet };
                            break;
                        case ']':
                            {
                                if (characterContainer == null)
                                    throw new ArgumentException("Invalid position for character ]", "pattern");

                                var setBlock = ConvertToBlock(characterContainer);
                                characterContainer = null;
                                currentContainer.PushBlock(setBlock);
                            }
                            break;
                        case '^':
                            if (characterContainer == null || characterContainer.Type != CharacterContainerType.InclusiveSet)
                                throw new ArgumentException("Invalid position for character ^", "pattern");
                            characterContainer.Type = CharacterContainerType.ExclusiveSet;
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
                var characterBlock = ConvertToBlock(characterContainer);
                container.PushBlock(characterBlock);
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

        private CharacterContainer FlushCharacterContainer(CharacterContainer characterContainer, BlockContainer currentContainer, bool excludeLastCharacter)
        {
            if (characterContainer != null)
            {
                if (excludeLastCharacter)
                {
                    var character = characterContainer.PopCharacter();
                    if (characterContainer.Characters.Length != 0)
                    {
                        currentContainer.PushBlock(ConvertToBlock(characterContainer));
                    }

                    characterContainer = new CharacterContainer();
                    characterContainer.AddCharacter(character);
                }

                currentContainer.PushBlock(ConvertToBlock(characterContainer));
            }


            return null;
        }

        private RegexBlock ConvertToBlock(CharacterContainer container)
        {
            switch (container.Type)
            {
                case CharacterContainerType.InclusiveSet:
                    return new InclusiveSetBlock(container.Characters);
                case CharacterContainerType.ExclusiveSet:
                    return new ExclusiveSetBlock(container.Characters);
                default:
                    return new TextBlock(container.Characters);
            }
        }

        private GroupBlock ConvertToBlock(BlockContainer container)
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

        private RegexBlock WrapToZeroOrOneBlock(RegexBlock block)
        {
            return new ZeroOrOneBlock(block);
        }

        private ZeroOrMoreBlock WrapToZeroOrMoreBlock(RegexBlock block)
        {
            return new ZeroOrMoreBlock(block);
        }

        private OneOrMoreBlock WrapToOneOrMoreBlock(RegexBlock block)
        {
            return new OneOrMoreBlock(block);
        }
    }
}
