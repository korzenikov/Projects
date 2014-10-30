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
            var containers = new Stack<Container>();
            containers.Push(new BlockContainer { Type = BlockContainerType.AndContainer });
            CharacterContainer characterContainer = null;
            bool expectGoupId = false;
            int position = 0;
            while (position < pattern.Length)
            {
                var currentContainer = containers.Peek();
                char c = pattern[position];
                if (char.IsLetterOrDigit(c))
                {
                    if (expectGoupId)
                    {
                        var backreferenceBlock = new BackreferenceBlock(int.Parse(c.ToString()));
                        var blockContainer = currentContainer as BlockContainer;
                        if (blockContainer == null)
                            throw new ArgumentException("Invalid position for character +", "pattern");
                        blockContainer.PushBlock(backreferenceBlock);
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
                    var blockContainer = currentContainer as BlockContainer;
                    if (c != '^')
                    {
                    
                        if (blockContainer != null && characterContainer != null)
                        {
                            if (IsQuantifier(c))
                            {
                                var character = characterContainer.PopCharacter();
                                if (characterContainer.Characters.Length != 0)
                                {
                                    blockContainer.PushBlock(ConvertToBlock(characterContainer));
                                }

                                blockContainer.PushBlock(new TextBlock(character.ToString(CultureInfo.InvariantCulture)));
                            }
                            else
                            {
                                blockContainer.PushBlock(ConvertToBlock(characterContainer));
                            }

                            characterContainer = null;
                        }
                    }

                    if (c == '.')
                    {
                        if (blockContainer == null)
                            throw new ArgumentException("Invalid position for character .", "pattern");
                        blockContainer.PushBlock(new AnyCharacterBlock());
                    }
                    else if (c == '+')
                    {
                        if (blockContainer == null)
                            throw new ArgumentException("Invalid position for character +", "pattern");
                        RegexBlock previousBlock = blockContainer.PopBlock();
                        blockContainer.PushBlock(WrapToOneOrMoreBlock(previousBlock));
                    }
                    else if (c == '*')
                    {
                        if (blockContainer == null)
                            throw new ArgumentException("Invalid position for character *", "pattern");
                        RegexBlock previousBlock = blockContainer.PopBlock();
                        blockContainer.PushBlock(WrapToZeroOrMoreBlock(previousBlock));
                    }
                    else if (c == '?')
                    {
                        if (blockContainer == null)
                            throw new ArgumentException("Invalid position for character ?", "pattern");
                        RegexBlock previousBlock = blockContainer.PopBlock();
                        blockContainer.PushBlock(WrapToZeroOrOneBlock(previousBlock));
                    }
                    else if (c == '[')
                    {
                        containers.Push(new CharacterSetContainer { Type = SetContainerType.InclusiveSet });
                    }
                    else if (c == ']')
                    {
                        var characterSetContainer = currentContainer as CharacterSetContainer;
                        if (characterSetContainer == null)
                            throw new ArgumentException("Invalid position for character ]", "pattern");
                         if (characterContainer == null)
                             throw new ArgumentException("Invalid position for character ]", "pattern");

                        var setBlock = ConvertToBlock(characterSetContainer, characterContainer.Characters);
                        characterContainer = null;
                        containers.Pop();
                        var outerBlockContainer = containers.Peek() as BlockContainer;
                        if (outerBlockContainer == null)
                            throw new ArgumentException("Invalid position for character ]", "pattern");
                        outerBlockContainer.PushBlock(setBlock);
                    }
                    else if (c == '^')
                    {
                        var characterSetContainer = currentContainer as CharacterSetContainer;
                        if (characterSetContainer == null || characterSetContainer.Type != SetContainerType.InclusiveSet)
                            throw new ArgumentException("Invalid position for character ^", "pattern");
                        characterSetContainer.Type = SetContainerType.ExclusiveSet;
                    }
                    else if (c == '(')
                    {
                        containers.Push(new BlockContainer());
                    }
                    else if (c == ')')
                    {
                        if (blockContainer == null)
                            throw new ArgumentException("Invalid position for character )", "pattern");

                        if (blockContainer.Type == BlockContainerType.Undefined)
                            blockContainer.Type = BlockContainerType.AndContainer;
                        GroupBlock containerBlock = ConvertToBlock(blockContainer);
                        containers.Pop();
                        var outerBlockContainer = containers.Peek() as BlockContainer;
                        if (outerBlockContainer == null)
                            throw new ArgumentException("Invalid position for character ]", "pattern");
                        outerBlockContainer.PushBlock(containerBlock);

                    }
                    else if (c == '|')
                    {
                        if (blockContainer == null)
                            throw new ArgumentException("Invalid position for character )", "pattern");
                        blockContainer.Type = BlockContainerType.OrContainer;
                    }
                    else if (c == '\\')
                    {
                        expectGoupId = true;
                    }
                    else throw new ArgumentException("Unrecognized symbol in pattern", "pattern");
                }

                position++;
            }

            var container = containers.Pop() as BlockContainer;

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

        private bool IsQuantifier(char c)
        {
            return "?+*".IndexOf(c) != -1;
        }

        private SetBlock ConvertToBlock(CharacterSetContainer container, string characters)
        {
            switch (container.Type)
            {
                case SetContainerType.InclusiveSet:
                    return new InclusiveSetBlock(characters);
                case SetContainerType.ExclusiveSet:
                    return new ExclusiveSetBlock(characters);
                default:
                    return null;
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


        private TextBlock ConvertToBlock(CharacterContainer container)
        {
            return new TextBlock(container.Characters);
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

        private AndGroupBlock WrapToAndGroupBlock(IEnumerable<RegexBlock> blocks)
        {
            return new AndGroupBlock(blocks.ToArray());
        }

        private OrGroupBlock WrapToOrGroupBlock(IEnumerable<RegexBlock> blocks)
        {
            return new OrGroupBlock(blocks.ToArray());
        }
    }
}
