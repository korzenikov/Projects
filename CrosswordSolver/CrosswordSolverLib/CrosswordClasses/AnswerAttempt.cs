using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrosswordSolverLib.CrosswordClasses
{
    public class AnswerAttempt
    {
        public AnswerAttempt(List<CrosswordCell> cells, List<CrosswordQuestion> questions)
        {
            AffectedCells = cells;
            AffectedQuestions = questions;
        }

        public List<CrosswordCell> AffectedCells
        {
            get;
            private set;
        }

        public List<CrosswordQuestion> AffectedQuestions
        {
            get;
            private set;
        }
    }
}
