using System.Collections.Generic;

namespace CrosswordSolverLib.RegexBlocks
{
    public class OrGroupBlock : GroupBlock
    {
        public OrGroupBlock(IReadOnlyList<RegexBlock> innerBlocks)
            : base(innerBlocks)
        {
        }
    }
}
