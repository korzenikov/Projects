using CrosswordSolverLib.RegexBlocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrosswordSolverLib.RegexClasses
{
    public class RegexParser
    {
        public RegularExpression Parse(string pattern)
        {
            Stack<BlockContainer> containers = new Stack<BlockContainer>();
            containers.Push(new BlockContainer { Type = BlockContainerType.AndContainer });
            CharacterContainer characterContainer = null;
            bool expectGoupId = false;
            int position= 0;
            while (position < pattern.Length)
            {
                var currentContainer = containers.Peek();
                char c = pattern[position];
                if (char.IsLetterOrDigit(c))
                {
                    if (expectGoupId)
                    {
                        BackreferenceBlock backreferenceBlock = new BackreferenceBlock(int.Parse(c.ToString()));
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
                    if (c != '^')
                        if (characterContainer != null)
                        {
                            RegexBlock characterBlock = ConvertToBlock(characterContainer);
                            currentContainer.PushBlock(characterBlock);
                            characterContainer = null;
                        }
                    if (c == '.')
                    {
                        currentContainer.PushBlock(new AnyCharacterBlock());
                    }
                    else if (c == '+')
                    {
                        RegexBlock previousBlock = currentContainer.PopBlock();
                        currentContainer.PushBlock(WrapToOneOrMoreBlock(previousBlock));
                    }
                    else if (c == '*')
                    {
                        RegexBlock previousBlock = currentContainer.PopBlock();
                        currentContainer.PushBlock(WrapToZeroOrMoreBlock(previousBlock));
                    }
                    else if (c == '?')
                    {
                        RegexBlock previousBlock = currentContainer.PopBlock();
                        currentContainer.PushBlock(WrapToZeroOrOneBlock(previousBlock));
                    }
                    else if (c == '[')
                    {
                        if (characterContainer == null)
                            characterContainer = new CharacterContainer { Type = CharacterContainerType.InclusiveSet };
                    }
                    else if (c == ']')
                    {

                    }
                    else if (c == '^')
                    {
                        if (characterContainer == null || characterContainer.Type != CharacterContainerType.InclusiveSet)
                            throw new ArgumentException("Invalid position for character ^", "pattern");
                        characterContainer.Type = CharacterContainerType.ExclusiveSet;
                    }
                    else if (c == '(')
                    {
                        containers.Push(new BlockContainer());
                    }
                    else if (c == ')')
                    {
                        if (currentContainer.Type == BlockContainerType.Undefined)
                            currentContainer.Type = BlockContainerType.AndContainer;
                        GroupBlock containerBlock = ConvertToBlock(currentContainer);
                        containers.Pop();
                        currentContainer = containers.Peek();
                        currentContainer.PushBlock(containerBlock);
                    }
                    else if (c == '|')
                    {
                        currentContainer.Type = BlockContainerType.OrContainer;
                    }
                    else if (c == '\\')
                    {
                        expectGoupId = true;
                    }
                    else throw new ArgumentException("Unrecognized symbol in pattern", "pattern");
                }

                position++;
            }

            BlockContainer container = containers.Pop();

            if (characterContainer != null)
            {
                var characterBlock = ConvertToBlock(characterContainer);
                container.PushBlock(characterBlock);
            }
            
            GroupBlock block = ConvertToBlock(container);

            // Remove redundant group group if any.
            if (block.InnerBlocks.Count() == 1)
            {
                OrGroupBlock orGroupBlock = block.InnerBlocks.First() as OrGroupBlock;
                if (orGroupBlock != null)
                    block = orGroupBlock;
            }
                

            return new RegularExpression(block);
        }

        private RegexBlock ConvertToBlock(CharacterContainer container)
        {
            switch (container.Type)
            {
                case CharacterContainerType.TextBlock:
                    return new TextBlock(container.Characters);
                case CharacterContainerType.InclusiveSet:
                    return new InclusiveSetBlock(container.Characters);
                case CharacterContainerType.ExclusiveSet:
                    return new ExclusiveSetBlock(container.Characters);
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
