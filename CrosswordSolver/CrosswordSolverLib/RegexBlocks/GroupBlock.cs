using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrosswordSolverLib.RegexBlocks
{
    public abstract class GroupBlock : RegexBlock
    {
        public GroupBlock(RegexBlock[] innerBlocks)
        {
            InnerBlocks = innerBlocks;
        }
        public RegexBlock[] InnerBlocks { get; private set; }
    }
}
