using System;
using System.Collections.Generic;
using System.Linq;

using CrosswordSolverLib.CrosswordClasses;

namespace CrosswordSolverLib.SolverClasses
{
    public class Solver
    {
        #region Public Methods and Operators

        public bool Solve(Crossword crossword)
        {
            var allLetters = GetAllLetters().ToArray();
            var unresolvedCellInfos = crossword.GetCells().Select(cell => new CellInfo(cell) { AvailableCharacters = allLetters.ToList() }).ToList();
            while (true)
            {
                foreach (var cellInfo in unresolvedCellInfos)
                {
                    var crosswordCell = cellInfo.Cell;
                   

                    var questions = crossword.GetQuestionsForCell(crosswordCell).ToArray();

                    var availableCharacters = cellInfo.AvailableCharacters;

                    foreach (var character in availableCharacters.ToArray())
                    {
                        crossword.ApplyCharacter(crosswordCell, character);
                        if (!CheckQuestionsForCell(crossword, questions, crosswordCell))
                        {
                            availableCharacters.Remove(character);
                           
                            if (availableCharacters.Count == 0)
                            {
                                throw new ArgumentException("Invalid crossword data", "crossword");
                            }
                        }
                    }

                    crossword.ApplyCharacter(crosswordCell, '\0');
                }

                var resolvedCellInfos = unresolvedCellInfos.Where(info => info.AvailableCharacters.Count == 1).ToArray();
                if (resolvedCellInfos.Length == 0)
                {
                    break;
                }

                foreach (var cellInfo in resolvedCellInfos)
                {
                    crossword.ApplyCharacter(cellInfo.Cell, cellInfo.AvailableCharacters[0]);
                    unresolvedCellInfos.Remove(cellInfo);
                }
            }

            if (unresolvedCellInfos.Count == 0)
            {
                return true;
            }

            var rootCellInfo = unresolvedCellInfos.OrderBy(cell => cell.AvailableCharacters.Count).First();
            return Attempt(crossword, rootCellInfo.Cell, unresolvedCellInfos.ToDictionary(x => x.Cell, x => x));
        }

        #endregion

        #region Methods

        private bool Attempt(Crossword crossword, CrosswordCell cell, IDictionary<CrosswordCell, CellInfo> cellInfos)
        {
            var cellInfo = cellInfos[cell];
            var questions = crossword.GetQuestionsForCell(cell).ToArray();
            foreach (var character in cellInfo.AvailableCharacters)
            {
                crossword.ApplyCharacter(cell, character);
                if (!CheckQuestionsForCell(crossword,questions,  cell))
                {
                    continue;
                }

                if (!crossword.GetCells().Any(crossword.IsEmptyCell))
                {
                    return true;
                }

                foreach (var affectedCell in crossword.GetQuestionsForCell(cell).SelectMany(crossword.GetCellsForQuestion).Where(crossword.IsEmptyCell))
                {
                    if (Attempt(crossword, affectedCell, cellInfos))
                    {
                        return true;
                    }
                }
            }

            crossword.ApplyCharacter(cell, '\0');
            return false;
        }

        private bool CheckQuestionsForCell(Crossword crossword, IReadOnlyCollection<CrosswordQuestion> questions, CrosswordCell cell)
        {
            foreach (var question in questions)
            {
                var line = crossword.GetLineForQuestion(question);
                var checker = new Checker(line);
                if (!checker.Check(question.Expression)) 
                    return false;
            }

            return true;
        }
         

        private IEnumerable<char> GetAllLetters()
        {
            for (char c = 'a'; c <= 'z'; c++)
                yield return c;
        }

        #endregion
    }
}