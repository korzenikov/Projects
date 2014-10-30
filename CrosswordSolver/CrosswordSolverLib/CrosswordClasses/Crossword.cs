using CrosswordSolverLib.SolverClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrosswordSolverLib.CrosswordClasses
{
    public abstract class Crossword
    {
        public abstract IEnumerable<CrosswordQuestion> GetQuestions();

        public abstract string GetLineForQuestion(CrosswordQuestion question);

        public abstract CrosswordCell[] GetCellsForQuestion(CrosswordQuestion question);

        public abstract AnswerAttempt ApplyAnswer(CrosswordQuestion question, string answer);

        public abstract void RollbackAnswerAttempt(AnswerAttempt attempt);

        public abstract bool IsSolved();

        public abstract IEnumerable<CrosswordQuestion> GetQuestionsForCell(int rowIndex, int columnIndex);
    }
}
