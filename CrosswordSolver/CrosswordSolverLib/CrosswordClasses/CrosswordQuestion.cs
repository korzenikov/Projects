using CrosswordSolverLib.RegexClasses;

namespace CrosswordSolverLib.CrosswordClasses
{
    public class CrosswordQuestion
    {
        private readonly string _pattern;

        public CrosswordQuestion(RegularExpression expression, string pattern)
        {
            Expression = expression;
            _pattern = pattern;
        }

        public RegularExpression Expression { get; private set; }

        public string Pattern
        {
            get
            {
                return _pattern;
            }
        }
    }
}
