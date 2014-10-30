using CrosswordSolverLib.RegexClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrosswordSolverLib.CrosswordClasses
{
    public class CrosswordQuestion
    {
        
        public CrosswordQuestion(int questionId, RegularExpression expression)
        {
            QuestionId = questionId;
            Expression = expression;
        }

        public int QuestionId { get; private set; }
        
        public RegularExpression Expression { get; private set; }
    }
}
