using System.Collections.Generic;

namespace CrosswordSolverLib.RegexBlocks
{
    public class AndGroupBlock : GroupBlock
    {
        public AndGroupBlock(IReadOnlyList<RegexBlock> innerBlocks)
            : base(innerBlocks)
        {
        }
    }
}
