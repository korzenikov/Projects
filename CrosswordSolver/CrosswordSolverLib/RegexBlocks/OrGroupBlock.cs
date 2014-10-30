using System.Collections.Generic;

namespace CrosswordSolverLib.RegexBlocks
{
    public class OrGroupBlock : GroupBlock
    {
        public OrGroupBlock(IReadOnlyList<RegexBlock> innerBlocks)
            : base(innerBlocks)
        {
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;
            if (obj == null || GetType() != obj.GetType())
                return false;
            return Equals((OrGroupBlock)obj);
        }

        private bool Equals(OrGroupBlock obj)
        {
            if (InnerBlocks.Count != obj.InnerBlocks.Count)
                return false;
            for (int i = 0; i < obj.InnerBlocks.Count; i++)
            {
                if (!InnerBlocks[i].Equals(obj.InnerBlocks[i]))
                    return false;
            }

            return true;
        }
    }
}
