using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrosswordSolverLib.RegexBlocks
{
    public class InclusiveSetBlock : SetBlock
    {
        public InclusiveSetBlock(string characters) : base(characters)
        {
        }

        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(this, obj))
                return true;
            if (obj == null || GetType() != obj.GetType())
                return false;
            return Equals((InclusiveSetBlock)obj);
        }

        private bool Equals(InclusiveSetBlock obj)
        {
            return Characters == obj.Characters;
        }
    }
}
