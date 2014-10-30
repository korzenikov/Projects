using CrosswordSolverLib.RegexClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrosswordSolverLib.CrosswordClasses
{
    public class HexagonCrossword : Crossword
    {
        private char[][] _field;
        private int _size;
        private string[] _leftRightExpressions;
        private string[] _bottomTopExpressions;
        private string[] _topBottomExpressions;
        private CrosswordQuestion[] _leftRightQuestions;
        private CrosswordQuestion[] _bottomTopQuestions;
        private CrosswordQuestion[] _topBottomQuestions;

        public HexagonCrossword(int size, string[] leftRightExpressions, string[] bottomTopExpressions, string[] topBottomExpressions)
        {
            _size = size;
            InitializeField();
            RegexParser parser = new RegexParser();
            _leftRightExpressions = leftRightExpressions;
            _bottomTopExpressions = bottomTopExpressions;
            _topBottomExpressions = topBottomExpressions;
            int id = 0;
            _leftRightQuestions = leftRightExpressions.Select(item => new CrosswordQuestion(id++, parser.Parse(item))).ToArray();
            _bottomTopQuestions = bottomTopExpressions.Select(item => new CrosswordQuestion(id++, parser.Parse(item))).ToArray();
            _topBottomQuestions = topBottomExpressions.Select(item => new CrosswordQuestion(id++, parser.Parse(item))).ToArray();
        }
        
        public override IEnumerable<CrosswordQuestion> GetQuestions()
        {
            foreach (var question in _leftRightQuestions)
                yield return question;

            foreach (var question in _bottomTopQuestions)
                yield return question;

            foreach (var question in _topBottomQuestions)
                yield return question;
        }

        public override string GetLineForQuestion(CrosswordQuestion question)
        {
            int index = Array.IndexOf(_leftRightQuestions, question);
            if (index != -1)
                return GetLeftRightLine(index);
            index = Array.IndexOf(_bottomTopQuestions, question);
            if (index != -1)
                return GetBottomTopLine(index);
            index = Array.IndexOf(_topBottomQuestions, question);
            if (index != -1)
                return GetTopBottomLine(index);
            return null;
        }

        public override CrosswordCell[] GetCellsForQuestion(CrosswordQuestion question)
        {
            int index = Array.IndexOf(_leftRightQuestions, question);
            if (index != -1)
                return GetLeftRightLineCells(index);
            index = Array.IndexOf(_bottomTopQuestions, question);
            if (index != -1)
                return GetBottomTopLineCells(index);
            index = Array.IndexOf(_topBottomQuestions, question);
            if (index != -1)
                return GetTopBottomLineCells(index);
            return null;
        }

        public override IEnumerable<CrosswordQuestion> GetQuestionsForCell(int rowIndex, int columnIndex)
        {
            yield return GetLeftRightQuestionForCell(rowIndex, columnIndex);
            yield return GetBottomTopQuestionForCell(rowIndex, columnIndex);
            yield return GetTopBottomQuestionForCell(rowIndex, columnIndex);
        }

        public override AnswerAttempt ApplyAnswer(CrosswordQuestion question, string answer)
        {
            int index = Array.IndexOf(_leftRightQuestions, question);
            if (index != -1)
                return ApplyAnswerToLeftRightQuestion(index, answer);
            index = Array.IndexOf(_bottomTopQuestions, question);
            if (index != -1)
                return ApplyAnswerToBottomTopQuestion(index, answer);
            index = Array.IndexOf(_topBottomQuestions, question);
            if (index != -1)
                return ApplyAnswerToTopBottomQuestion(index, answer);
            return null;
        }

        public override void RollbackAnswerAttempt(AnswerAttempt attempt)
        {
            foreach (var cell in attempt.AffectedCells)
            {
                _field[cell.RowIndex][cell.ColumnIndex] = (char)0;
            }
        }

        public override bool IsSolved()
        {
            throw new NotImplementedException();
        }

        public void Print()
        {
            int indentSize = 1;
            string indent = new string(' ', indentSize);

            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size - i; j++)
                {
                    Console.Write(indent);
                }

                foreach (var c in _field[i])
                {
                    Console.Write(c + indent);
                }
                Console.WriteLine();
            }

            int lineNumber = _size;
            
            for (int i = 0; i < _size + 1; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    Console.Write(indent);
                }

                foreach (var c in _field[lineNumber + i])
                {
                    Console.Write(c + indent);
                }
                Console.WriteLine();
            }
        }

        public string GetLeftRightLine(int number)
        {
            var sb = new StringBuilder();
            foreach (var cell in GetLeftRightLineCells(number))
            {
                sb.Append(_field[cell.RowIndex][cell.ColumnIndex]);
            }

            return sb.ToString();
        }

        public string GetBottomTopLine(int number)
        {
            var sb = new StringBuilder();
            foreach (var cell in GetBottomTopLineCells(number))
            {
                sb.Append(_field[cell.RowIndex][cell.ColumnIndex]);
            }

            return sb.ToString();
        }

        public string GetTopBottomLine(int number)
        {
            var sb = new StringBuilder();
            foreach (var cell in GetTopBottomLineCells(number))
            {
                sb.Append(_field[cell.RowIndex][cell.ColumnIndex]);
            }

            return sb.ToString();
        }

        public CrosswordCell[] GetLeftRightLineCells(int number)
        {
            int lineLength;
            int i = number;
            int j = 0;
            if (number <= _size)
            {
                lineLength = _size + number + 1;
            }
            else
            {
                lineLength = 3 * _size - number + 1;
            }

            var cells = new CrosswordCell[lineLength];

            for (int t = 0; t < lineLength; t++)
            {
                cells[t] = new CrosswordCell(i, j);
                j++;
            }

            return cells;
        }

        public CrosswordCell[] GetBottomTopLineCells(int number)
        {
            int lineLength;
            int i;
            if (number <= _size)
            {
                i = 2 * _size;
                lineLength = _size + number + 1;
            }
            else
            {
                lineLength = 3 * _size - number + 1;
                i = 3 * _size - number;
            }
            var cells = new CrosswordCell[lineLength];

            int j = number;
            for (int t = 0; t < lineLength; t++)
            {
                if (i < 6)
                    j--;
                cells[t] = new CrosswordCell(i, j);
                i--;
            }

            return cells;
        }

        public CrosswordCell[] GetTopBottomLineCells(int number)
        {
            int lineLength;
            int i;
            if (number <= _size)
            {
                lineLength = _size + number + 1;
                i = 0;
            }
            else
            {
                lineLength = 3 * _size - number + 1;
                i = number - _size;
            }
            var cells = new CrosswordCell[lineLength];

            int j = number;
            for (int t = 0; t < lineLength; t++)
            {
                if (i > 6)
                    j--;
                cells[t] = new CrosswordCell(i, j);
                i++;
            }

            return cells;
        }

        public CrosswordQuestion GetLeftRightQuestionForCell(int i, int j)
        {
            return _leftRightQuestions[i];
        }

        public CrosswordQuestion GetTopBottomQuestionForCell(int i, int j)
        {
            return _topBottomQuestions[j];
        }

        public CrosswordQuestion GetBottomTopQuestionForCell(int i, int j)
        {
            if (i < _size)
                return _bottomTopQuestions[j + _size - i];
            return _bottomTopQuestions[j];
        }

        #region Private Methods

        private void InitializeField()
        {
            int linesCount = 2 * _size + 1;
            _field = new char[linesCount][];
            int symbolsInLine = _size + 1;
            for (int i = 0; i < _size + 1; i++)
            {
                _field[i] = new char[symbolsInLine];
                symbolsInLine++;
            }

            symbolsInLine -= 2;

            for (int i = _size + 1; i < linesCount; i++)
            {
                _field[i] = new char[symbolsInLine];
                symbolsInLine--;
            }
        }

        private AnswerAttempt ApplyAnswerToLeftRightQuestion(int index, string answer)
        {
            var questions = new List<CrosswordQuestion>();
            var cells = new List<CrosswordCell>();
            int t = 0;
            foreach (var cell in GetLeftRightLineCells(index))
            {
                var i = cell.RowIndex;
                var j = cell.ColumnIndex;
                if (_field[i][j] == 0)
                {
                    _field[i][j] = answer[t];
                    cells.Add(cell);
                    var bottomTopQuestion = GetBottomTopQuestionForCell(i, j);
                    questions.Add(bottomTopQuestion);
                    var topBottomQuestion = GetTopBottomQuestionForCell(i, j);
                    questions.Add(topBottomQuestion);
                }
                t++;
            }

            var attempt = new AnswerAttempt(cells, questions);
            return attempt;
        }

        private AnswerAttempt ApplyAnswerToBottomTopQuestion(int index, string answer)
        {
            var questions = new List<CrosswordQuestion>();
            var cells = new List<CrosswordCell>();
            int t = 0;
            foreach (var cell in GetBottomTopLineCells(index))
            {
                var i = cell.RowIndex;
                var j = cell.ColumnIndex;
                if (_field[i][j] == 0)
                {
                    _field[i][j] = answer[t];
                    cells.Add(cell);
                    var leftRightQuestion = GetLeftRightQuestionForCell(i, j);
                    questions.Add(leftRightQuestion);
                    var topBottomQuestion = GetTopBottomQuestionForCell(i, j);
                    questions.Add(topBottomQuestion);
                }
                t++;
            }

            var attempt = new AnswerAttempt(cells, questions);
            return attempt;
        }

        private AnswerAttempt ApplyAnswerToTopBottomQuestion(int index, string answer)
        {
            var questions = new List<CrosswordQuestion>();
            var cells = new List<CrosswordCell>();
            int t = 0;
            foreach (var cell in GetTopBottomLineCells(index))
            {
                var i = cell.RowIndex;
                var j = cell.ColumnIndex;
                if (_field[i][j] == 0)
                {
                    _field[i][j] = answer[t];
                    cells.Add(cell);
                    var leftRightQuestion = GetLeftRightQuestionForCell(i, j);
                    questions.Add(leftRightQuestion);
                    var bottomTopQuestion = GetBottomTopQuestionForCell(i, j);
                    questions.Add(bottomTopQuestion);
                }
                t++;
            }

            var attempt = new AnswerAttempt(cells, questions);
            return attempt;
        }

        #endregion
    }
}
