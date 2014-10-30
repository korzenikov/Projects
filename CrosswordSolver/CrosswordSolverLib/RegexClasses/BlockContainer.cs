using CrosswordSolverLib.RegexBlocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrosswordSolverLib.RegexClasses
{
    public class BlockContainer
    {
        private Stack<RegexBlock> _blocks;

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
    }
}
