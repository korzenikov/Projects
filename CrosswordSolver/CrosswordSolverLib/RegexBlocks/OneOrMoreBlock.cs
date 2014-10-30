using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrosswordSolverLib.RegexBlocks
{
    public class OneOrMoreBlock : QuantifierBlock
    {
        public OneOrMoreBlock(RegexBlock innerBlock) : base(innerBlock)
        {
        }

        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(this, obj))
                return true;
            if (obj == null || GetType() != obj.GetType())
                return false;
            return Equals((OneOrMoreBlock)obj);
        }

        private bool Equals(OneOrMoreBlock obj)
        {
            return this.InnerBlock.Equals(obj.InnerBlock);
        }
    }
}
