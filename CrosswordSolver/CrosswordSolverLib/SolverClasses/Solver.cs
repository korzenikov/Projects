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
            var unresolvedCells = crossword.GetCells().ToDictionary(cell => cell, cell => allLetters.ToList());
            while (true)
            {
                foreach (var unresolvedCell in unresolvedCells)
                {
                    var cell = unresolvedCell.Key;

                    var questions = crossword.GetQuestionsForCell(cell).ToArray();

                    var availableCharacters = unresolvedCell.Value;

                    foreach (var character in availableCharacters.ToArray())
                    {
                        crossword.ApplyCharacter(cell, character);
                        if (!CheckQuestions(crossword, questions))
                        {
                            availableCharacters.Remove(character);
                           
                            if (availableCharacters.Count == 0)
                            {
                                throw new ArgumentException("Invalid crossword data", "crossword");
                            }
                        }
                    }

                    crossword.ApplyCharacter(cell, '\0');
                }

                var resolvedCellInfos = unresolvedCells.Where(info => info.Value.Count == 1).ToArray();
                if (resolvedCellInfos.Length == 0)
                {
                    break;
                }

                foreach (var cellInfo in resolvedCellInfos)
                {
                    crossword.ApplyCharacter(cellInfo.Key, cellInfo.Value[0]);
                    unresolvedCells.Remove(cellInfo.Key);
                }
            }

            if (unresolvedCells.Count == 0)
            {
                return true;
            }

            while (unresolvedCells.Count > 0)
            {
                var unrosolvedCell = unresolvedCells.First();
                if (!Attempt(crossword, unrosolvedCell.Key, unresolvedCells))
                    return false;
                unresolvedCells.Remove(unrosolvedCell.Key);
            }

            return true;
        }

        #endregion

        #region Methods

        private bool Attempt(Crossword crossword, CrosswordCell cell, IDictionary<CrosswordCell, List<char>> unresolvedCells)
        {
            var characters = unresolvedCells[cell];
            var questions = crossword.GetQuestionsForCell(cell).ToArray();
            foreach (var character in characters)
            {
                crossword.ApplyCharacter(cell, character);
                if (!CheckQuestions(crossword, questions))
                {
                    continue;
                }

                if (crossword.GetCells().All(c => crossword.GetCellCharacter(c) != 0))
                {
                    return true;
                }

                foreach (var affectedCell in crossword.GetQuestionsForCell(cell).SelectMany(crossword.GetCellsForQuestion).Where(c => crossword.GetCellCharacter(c) == 0))
                {
                    if (Attempt(crossword, affectedCell, unresolvedCells))
                    {
                        return true;
                    }
                }
            }

            crossword.ApplyCharacter(cell, '\0');
            return false;
        }

        private bool CheckQuestions(Crossword crossword, IEnumerable<CrosswordQuestion> questions)
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