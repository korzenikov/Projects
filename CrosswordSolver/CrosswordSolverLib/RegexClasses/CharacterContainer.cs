using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrosswordSolverLib.RegexClasses
{
    public class CharacterContainer
    {
        private StringBuilder _stringBuilder;

        public CharacterContainer()
        {
            _stringBuilder = new StringBuilder();
        }

        public CharacterContainerType Type { get; set; }

        public string Characters
        {
            get
            {
                return _stringBuilder.ToString();
            }
        }

        public void AddCharacter(char c)
        {
            _stringBuilder.Append(c);
        }
    }
}
