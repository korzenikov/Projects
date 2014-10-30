using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrosswordSolverLib.RegexBlocks
{
    public class TextBlock : RegexBlock
    {
        public TextBlock(string text)
        {
            Text = text;
        }

        public string Text { get; private set; }

        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(this, obj))
                return true;
            if (obj == null || GetType() != obj.GetType())
                return false;
            return Equals((TextBlock)obj);
        }

        private bool Equals(TextBlock obj)
        {
            return Text == obj.Text;
        }
    }
}
