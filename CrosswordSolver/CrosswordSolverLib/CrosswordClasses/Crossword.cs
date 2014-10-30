using System.Collections.Generic;

namespace CrosswordSolverLib.CrosswordClasses
{
    public abstract class Crossword
    {
        public abstract bool IsSolved();

        public abstract string GetLineForQuestion(CrosswordQuestion question);

        public abstract IReadOnlyCollection<CrosswordCell> GetCellsForQuestion(CrosswordQuestion question);

        public abstract IEnumerable<CrosswordQuestion> GetQuestionsForCell(CrosswordCell cell);

        public abstract IEnumerable<CrosswordCell> GetCells();

        public abstract void ApplyCharacter(CrosswordCell cell, char c);

        public abstract char GetCellCharacter(CrosswordCell cell);
    }
}
