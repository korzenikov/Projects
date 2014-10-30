using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrosswordSolverLib.RegexBlocks
{
    public class ZeroOrMoreBlock : QuantifierBlock
    {
        public ZeroOrMoreBlock(RegexBlock innerBlock) : base(innerBlock)
        {
        }

        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(this, obj))
                return true;
            if (obj == null || GetType() != obj.GetType())
                return false;
            return Equals((ZeroOrMoreBlock)obj);
        }

        private bool Equals(ZeroOrMoreBlock obj)
        {
            return this.InnerBlock.Equals(obj.InnerBlock);
        }
    }
}
