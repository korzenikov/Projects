using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrosswordSolverLib.RegexBlocks
{
    public abstract class QuantifierBlock : RegexBlock
    {
        public QuantifierBlock(RegexBlock innerBlock)
        {
            InnerBlock = innerBlock;
        }

        public RegexBlock InnerBlock { get; private set; }
    }
}
