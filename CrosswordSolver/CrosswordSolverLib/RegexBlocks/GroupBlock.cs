using System.Collections.Generic;

namespace CrosswordSolverLib.RegexBlocks
{
    public abstract class GroupBlock : RegexBlock
    {
        protected GroupBlock(IReadOnlyList<RegexBlock> innerBlocks)
        {
            InnerBlocks = innerBlocks;
        }

        public IReadOnlyList<RegexBlock> InnerBlocks { get; private set; }
    }
}
