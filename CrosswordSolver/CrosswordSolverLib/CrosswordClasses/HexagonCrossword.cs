using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using CrosswordSolverLib.RegexClasses;

namespace CrosswordSolverLib.CrosswordClasses
{
    public class HexagonCrossword : Crossword
    {
        #region Fields

        private readonly int _size;

        private readonly CrosswordQuestion[] _bottomTopQuestions;

        private readonly CrosswordQuestion[] _leftRightQuestions;

        private readonly CrosswordQuestion[] _topBottomQuestions;

        private char[][] _field;

        #endregion

        #region Constructors and Destructors

        public HexagonCrossword(
            int size,
            IEnumerable<string> leftRightExpressions,
            IEnumerable<string> bottomTopExpressions,
            IEnumerable<string> topBottomExpressions)
        {
            _size = size;
            InitializeField();
            var parser = new RegexParser();
            _leftRightQuestions = leftRightExpressions.Select(item => new CrosswordQuestion(parser.Parse(item), item)).ToArray();
            _bottomTopQuestions = bottomTopExpressions.Select(item => new CrosswordQuestion(parser.Parse(item), item)).ToArray();
            _topBottomQuestions = topBottomExpressions.Select(item => new CrosswordQuestion(parser.Parse(item), item)).ToArray();
        }

        #endregion

        #region Public Methods and Operators

        public override IEnumerable<CrosswordCell> GetCells()
        {
            int linesCount = 2 * _size + 1;
            int symbolsInLine = _size + 1;
            for (int i = 0; i < _size + 1; i++)
            {
                for (int j = 0; j < symbolsInLine; j++)
                {
                    yield return new CrosswordCell(i, j);
                }

                symbolsInLine++;
            }

            symbolsInLine -= 2;

            for (int i = _size + 1; i < linesCount; i++)
            {
                for (int j = 0; j < symbolsInLine; j++)
                {
                    yield return new CrosswordCell(i, j);
                }
                symbolsInLine--;
            }
        }

        public override void ApplyCharacter(CrosswordCell cell, char c)
        {
            _field[cell.RowIndex][cell.ColumnIndex] = c;
        }

        public override char GetCellCharacter(CrosswordCell cell)
        {
            return _field[cell.RowIndex][cell.ColumnIndex];
        }

        public override IReadOnlyCollection<CrosswordCell> GetCellsForQuestion(CrosswordQuestion question)
        {
            int index = Array.IndexOf(_leftRightQuestions, question);
            if (index != -1)
            {
                return GetLeftRightLineCells(index);
            }

            index = Array.IndexOf(_bottomTopQuestions, question);
            if (index != -1)
            {
                return GetBottomTopLineCells(index);
            }

            index = Array.IndexOf(_topBottomQuestions, question);
            if (index != -1)
            {
                return GetTopBottomLineCells(index);
            }

            return null;
        }

        public override bool IsSolved()
        {
            foreach (var question in _leftRightQuestions.Concat(_topBottomQuestions).Concat(_bottomTopQuestions))
            {
                string pattern = question.Pattern;
                string line = GetLineForQuestion(question);
                if (!Regex.IsMatch(line, "^" + pattern + "$"))
                {
                    return false;
                }
            }

            return true;
        }

        public override string GetLineForQuestion(CrosswordQuestion question)
        {
            int index = Array.IndexOf(_leftRightQuestions, question);
            if (index != -1)
            {
                return GetLeftRightLine(index);
            }

            index = Array.IndexOf(_bottomTopQuestions, question);
            if (index != -1)
            {
                return GetBottomTopLine(index);
            }

            index = Array.IndexOf(_topBottomQuestions, question);
            if (index != -1)
            {
                return GetTopBottomLine(index);
            }

            return null;
        }

        public override IEnumerable<CrosswordQuestion> GetQuestionsForCell(CrosswordCell cell)
        {
            var rowIndex = cell.RowIndex;
            var columnIndex = cell.ColumnIndex;
            yield return GetLeftRightQuestionForCell(rowIndex, columnIndex);
            yield return GetBottomTopQuestionForCell(rowIndex, columnIndex);
            yield return GetTopBottomQuestionForCell(rowIndex, columnIndex);
        }

        public void Print()
        {
            int indentSize = 1;
            var indent = new string(' ', indentSize);

            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size - i; j++)
                {
                    Console.Write(indent);
                }

                foreach (char c in _field[i])
                {
                    Console.Write((c == 0 ? '.' : c) + indent);
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

                foreach (char c in _field[lineNumber + i])
                {
                    Console.Write((c == 0 ? '.' : c) + indent);
                }

                Console.WriteLine();
            }
        }

        #endregion

        #region Methods

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

        private string GetLeftRightLine(int number)
        {
            var sb = new StringBuilder();
            foreach (CrosswordCell cell in GetLeftRightLineCells(number))
            {
                sb.Append(_field[cell.RowIndex][cell.ColumnIndex]);
            }

            return sb.ToString();
        }

        private string GetBottomTopLine(int number)
        {
            var sb = new StringBuilder();
            foreach (CrosswordCell cell in GetBottomTopLineCells(number))
            {
                sb.Append(_field[cell.RowIndex][cell.ColumnIndex]);
            }

            return sb.ToString();
        }

        private string GetTopBottomLine(int number)
        {
            var sb = new StringBuilder();
            foreach (CrosswordCell cell in GetTopBottomLineCells(number))
            {
                sb.Append(_field[cell.RowIndex][cell.ColumnIndex]);
            }

            return sb.ToString();
        }

        private CrosswordQuestion GetLeftRightQuestionForCell(int i, int j)
        {
            return _leftRightQuestions[i];
        }

        private CrosswordQuestion GetBottomTopQuestionForCell(int i, int j)
        {
            if (i < _size)
            {
                return _bottomTopQuestions[j + _size - i];
            }

            return _bottomTopQuestions[j];
        }

        private CrosswordQuestion GetTopBottomQuestionForCell(int i, int j)
        {
            if (i < _size)
            {
                return _topBottomQuestions[j];
            }

            return _topBottomQuestions[j + i - _size];
        }

        private IReadOnlyCollection<CrosswordCell> GetLeftRightLineCells(int number)
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

        private IReadOnlyCollection<CrosswordCell> GetTopBottomLineCells(int number)
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
                {
                    j--;
                }

                cells[t] = new CrosswordCell(i, j);
                i++;
            }

            return cells;
        }

        private IReadOnlyCollection<CrosswordCell> GetBottomTopLineCells(int number)
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
                {
                    j--;
                }

                cells[t] = new CrosswordCell(i, j);
                i--;
            }

            return cells;
        }


        #endregion
    }
}