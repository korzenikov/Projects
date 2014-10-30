using System.Collections.Generic;

namespace CrosswordSolverLib.RegexBlocks
{
    public class AndGroupBlock : GroupBlock
    {
        public AndGroupBlock(IReadOnlyList<RegexBlock> innerBlocks)
            : base(innerBlocks)
        {
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;
            if (obj == null || GetType() != obj.GetType())
                return false;
            return Equals((AndGroupBlock)obj);
        }

        private bool Equals(AndGroupBlock obj)
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
