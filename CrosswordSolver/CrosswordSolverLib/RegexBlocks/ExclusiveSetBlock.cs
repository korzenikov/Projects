using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrosswordSolverLib.RegexBlocks
{
    public class ExclusiveSetBlock : SetBlock
    {
        public ExclusiveSetBlock(string characters)
            : base(characters)
        {
        }

        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(this, obj))
                return true;
            if (obj == null || GetType() != obj.GetType())
                return false;
            return Equals((ExclusiveSetBlock)obj);
        }

        private bool Equals(ExclusiveSetBlock obj)
        {
            return Characters == obj.Characters;
        }
    }
}
