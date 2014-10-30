using System.Text;

namespace CrosswordSolverLib.RegexClasses
{
    public class CharacterContainer
    {
        private readonly StringBuilder _stringBuilder;

        public CharacterContainer()
        {
            _stringBuilder = new StringBuilder();
        }

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

        public char PopCharacter()
        {
            var character = _stringBuilder[_stringBuilder.Length - 1];
            _stringBuilder.Remove(_stringBuilder.Length - 1, 1);
            return character;
        }
    }
}
