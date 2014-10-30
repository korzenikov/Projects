using CrosswordSolverLib.RegexClasses;

namespace CrosswordSolverLib.CrosswordClasses
{
    public class CrosswordQuestion
    {
        private readonly string _pattern;

        public CrosswordQuestion(int questionId, RegularExpression expression, string pattern)
        {
            QuestionId = questionId;
            Expression = expression;
            _pattern = pattern;
        }

        public int QuestionId { get; private set; }
        
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
