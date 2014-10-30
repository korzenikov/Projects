using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using CrosswordSolverLib.RegexClasses;

namespace CrosswordSolverLib.CrosswordClasses
{
    public class MatrixCrossword : Crossword
    {
        #region Fields

        private readonly char[,] _field;

        private readonly string[] _horizontalExpressions;

        private readonly CrosswordQuestion[] _horizontalQuestions;

        private readonly int _size;

        private readonly string[] _verticalExpressions;

        private readonly CrosswordQuestion[] _verticalQuestions;

        #endregion

        #region Constructors and Destructors

        public MatrixCrossword(int size, string[] horizontalExpressions, string[] verticalExpressions)
        {
            _size = size;
            _field = new char[size, size];
            var parser = new RegexParser();
            _horizontalExpressions = horizontalExpressions;
            _verticalExpressions = verticalExpressions;
            int id = 0;
            _horizontalQuestions = horizontalExpressions.Select(item => new CrosswordQuestion(id++, parser.Parse(item), item)).ToArray();
            _verticalQuestions = verticalExpressions.Select(item => new CrosswordQuestion(id++, parser.Parse(item), item)).ToArray();
        }

        #endregion

        #region Public Methods and Operators

        public override IReadOnlyCollection<CrosswordCell> GetCellsForQuestion(CrosswordQuestion question)
        {
            var cells = new List<CrosswordCell>();
            int rowIndex = Array.IndexOf(_horizontalQuestions, question);
            if (rowIndex != -1)
            {
                cells.AddRange(Enumerable.Range(0, _size).Select(x => new CrosswordCell(rowIndex, x)));
            }

            int columnIndex = Array.IndexOf(_verticalQuestions, question);
            if (columnIndex != -1)
            {
                cells.AddRange(Enumerable.Range(0, _size).Select(x => new CrosswordCell(x, columnIndex)));
            }

            return cells;
        }

        public override string GetLineForQuestion(CrosswordQuestion question)
        {
            int rowIndex = Array.IndexOf(_horizontalQuestions, question);
            if (rowIndex != -1)
            {
                return MatrixHelper.GetHorizontalLine(_field, rowIndex);
            }

            int columnIndex = Array.IndexOf(_verticalQuestions, question);
            if (columnIndex != -1)
            {
                return MatrixHelper.GetVerticalLine(_field, columnIndex);
            }

            return null;
        }

        public override IEnumerable<CrosswordQuestion> GetQuestionsForCell(CrosswordCell cell)
        {
            yield return _horizontalQuestions[cell.RowIndex];
            yield return _verticalQuestions[cell.ColumnIndex];
        }

        public override IEnumerable<CrosswordCell> GetCells()
        {
            for (int i = 0; i < _size; i++)
            for (int j = 0; j < _size; j++)
            {
                yield return new CrosswordCell(i, j);
            }
        }

        public override void ApplyCharacter(CrosswordCell cell, char c)
        {
            _field[cell.RowIndex, cell.ColumnIndex] = c;
        }

        public override bool IsEmptyCell(CrosswordCell cell)
        {
            return _field[cell.RowIndex, cell.ColumnIndex] == 0;
        }

        public override bool IsSolved()
        {
            for (int i = 0; i < _size; i++)
            {
                string pattern = _horizontalExpressions[i];
                string line = MatrixHelper.GetHorizontalLine(_field, i);
                if (!Regex.IsMatch(line, "^" + pattern + "$"))
                {
                    return false;
                }
            }

            for (int j = 0; j < _size; j++)
            {
                string pattern = _verticalExpressions[j];
                string line = MatrixHelper.GetVerticalLine(_field, j);
                if (!Regex.IsMatch(line, "^" + pattern + "$"))
                {
                    return false;
                }
            }

            return true;
        }

        #endregion

        #region Methods

        #endregion
    }
}