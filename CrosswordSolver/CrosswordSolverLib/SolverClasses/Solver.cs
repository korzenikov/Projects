using CrosswordSolverLib.CrosswordClasses;
using CrosswordSolverLib.LineBuilderClasses;
using CrosswordSolverLib.RegexClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrosswordSolverLib.SolverClasses
{
    public class Solver
    {
        private Crossword _crossword;

        private Dictionary<int, QuestionState> _questionStates = new Dictionary<int, QuestionState>();

        private Dictionary<int, int> _uncertaintyLevels = new Dictionary<int, int>();

        #region Public Methods

        public bool Solve(Crossword crossword)
        {
            _crossword = crossword;
            var questions = _crossword.GetQuestions();
            CalculateUncertaintyLevels(questions);
            return AnswerQuestions(questions);
        }

        public int GetUncertaintyLevel(RegularExpression expression)
        {
            UncertaintyLevelRegexVisitor visitor = new UncertaintyLevelRegexVisitor();
            visitor.Visit(expression);
            return visitor.UncertaintyLevel;
        }

        #endregion

        #region Private methods

        private bool AnswerQuestions(IEnumerable<CrosswordQuestion> questions)
        {
            foreach (var question in questions.OrderBy(item => GetUncertaintyLevel(item.QuestionId)))
            {
                int questionId = question.QuestionId;
                if (GetQuestionState(question.QuestionId) == QuestionState.Unanswered)
                {
                    SetQuestionState(questionId, QuestionState.Answering);
                    var expression = question.Expression;
                    string pattern = _crossword.GetLineForQuestion(question);
                    Dictionary<int, List<char>> incorrectSymbolsDictionary = new Dictionary<int, List<char>>();
                    while (true)
                    {
                        var builder = new LineBuilder(pattern, incorrectSymbolsDictionary);

                        bool answerApplied = false;
                        bool generatorReset = false;
                        foreach (var line in builder.GetLines(expression))
                        {
                            IEnumerable<int> incorrectSymbols = SwallowCheck(question, line);
                            if (!incorrectSymbols.Any())
                            {
                                if (DepthCheck(question, line))
                                {
                                    answerApplied = true;
                                    break;
                                }
                            }
                            else
                            {
                                generatorReset = true;
                                foreach (var position in incorrectSymbols)
                                {
                                    var c = line[position];
                                    List<char> symbols = GetOrCreateList(incorrectSymbolsDictionary, position);
                                    symbols.Add(c);
                                }
                                break;
                                
                            }
                        }

                        if (answerApplied)
                        {
                            SetQuestionState(questionId, QuestionState.Answered);
                            break;
                        }
                        else if (!generatorReset)
                        {
                            SetQuestionState(questionId, QuestionState.Unanswered);
                            return false;
                        }
                    }
                }
            }
            
            return true;
        }

        private List<char> GetOrCreateList(Dictionary<int, List<char>> dictionary, int key)
        {
            List<char> list;
            if (!dictionary.TryGetValue(key, out list))
            {
                list = new List<char>();
                dictionary.Add(key, list);
            }

            return list;
        }

        private bool DepthCheck(CrosswordQuestion question, string line)
        {
            return true;
        }

        private bool FindAnyAnswerToQuestion(CrosswordQuestion question)
        {
            var expression = question.Expression;
            string pattern = _crossword.GetLineForQuestion(question);
            var builder = new LineBuilder(pattern, null);
            return builder.GetLines(expression).Any();
        }

        private IEnumerable<int> SwallowCheck(CrosswordQuestion question, string line)
        {
            AnswerAttempt attempt = _crossword.ApplyAnswer(question, line);
            var cells = _crossword.GetCellsForQuestion(question);
            int t = 0;
            List<int> incorrectSymbols = new List<int>();
            foreach (var cell in cells)
            {
                bool correctSymbol = true;
                if (attempt.AffectedCells.Any(item => item.RowIndex == cell.RowIndex && item.ColumnIndex == cell.ColumnIndex))
                {
                    var affectedQuestions = _crossword.GetQuestionsForCell(cell.RowIndex, cell.ColumnIndex).Where(item => item.QuestionId != question.QuestionId);
                    foreach (var affectedQuestion in affectedQuestions)
                    {
                        if (!FindAnyAnswerToQuestion(affectedQuestion))
                        {
                            correctSymbol = false;
                            break;
                        }
                    }
                }
                if (!correctSymbol)
                    incorrectSymbols.Add(t);
                t++;
            }

            if (incorrectSymbols.Count != 0)
                _crossword.RollbackAnswerAttempt(attempt);
            return incorrectSymbols;
        }

        private QuestionState GetQuestionState(int questionId)
        {
            QuestionState state;
            _questionStates.TryGetValue(questionId, out state);
            return state;
        }

        private void SetQuestionState(int questionId, QuestionState state)
        {
            _questionStates[questionId] = state;
        }

        private int GetUncertaintyLevel(int questionId)
        {
            return _uncertaintyLevels[questionId];
        }

        private void SetUncertaintyLevel(int questionId, int uncertaintyLevel)
        {
            _uncertaintyLevels[questionId] = uncertaintyLevel;
        }

        private void CalculateUncertaintyLevels(IEnumerable<CrosswordQuestion> questions)
        {
            foreach (var question in questions)
            {
                int uncertaintyLevel = GetUncertaintyLevel(question.Expression);
                SetUncertaintyLevel(question.QuestionId, uncertaintyLevel);
            }
        }

        #endregion
    }
}
