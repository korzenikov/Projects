using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrosswordSolverLib.RegexBlocks
{
    public class OrGroupBlock : GroupBlock
    {
        public OrGroupBlock(RegexBlock[] innerBlocks) : base(innerBlocks)
        {
        }

        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(this, obj))
                return true;
            if (obj == null || GetType() != obj.GetType())
                return false;
            return Equals((OrGroupBlock)obj);
        }

        private bool Equals(OrGroupBlock obj)
        {
            if (InnerBlocks.Length != obj.InnerBlocks.Length)
                return false;
            for (int i = 0; i < obj.InnerBlocks.Length; i++)
            {
                if (!InnerBlocks[i].Equals(obj.InnerBlocks[i]))
                    return false;
            }

            return true;
        }
    }
}
