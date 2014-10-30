using CrosswordSolverLib.RegexBlocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrosswordSolverLib.RegexClasses
{
    public class RegularExpression
    {
        public RegularExpression(GroupBlock innerBlock)
        {
            InnerBlock = innerBlock;
        }

        public GroupBlock InnerBlock { get; private set; }

        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(this, obj))
                return true;
            if (obj == null || GetType() != obj.GetType())
                return false;

            return Equals((RegularExpression)obj);
        }

        private bool Equals(RegularExpression obj)
        {
            return this.InnerBlock.Equals(obj.InnerBlock);
        }
    }
}
