using System.Collections.Generic;
using System.Linq;

using CrosswordSolverLib.RegexBlocks;

namespace CrosswordSolverLib.RegexClasses
{
    public class BlockContainer : Container
    {
        private readonly Stack<RegexBlock> _blocks;

        public BlockContainer()
        {
            _blocks = new Stack<RegexBlock>();
        }
        
        public BlockContainerType Type { get; set; }

        public IEnumerable<RegexBlock> Blocks
        {
            get
            {
                return _blocks.Reverse();
            }
        }

        public void PushBlock(RegexBlock block)
        {
            _blocks.Push(block);
        }

        public RegexBlock PopBlock()
        {
            return _blocks.Pop();
        }

        public RegexBlock PeekBlock()
        {
            return _blocks.Peek();
        }
    }
}
