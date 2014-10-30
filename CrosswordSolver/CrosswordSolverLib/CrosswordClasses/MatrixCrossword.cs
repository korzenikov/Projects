using CrosswordSolverLib.RegexClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CrosswordSolverLib.CrosswordClasses
{
    public class MatrixCrossword : Crossword
    {
        private CrosswordQuestion[] _horizontalQuestions;
        private CrosswordQuestion[] _verticalQuestions;

        private string[] _horizontalExpressions;
        private string[] _verticalExpressions;

        private char[,] _field;
        private int _size;
        
        public MatrixCrossword(int size, string[] horizontalExpressions, string[] verticalExpressions)
        {
            _size = size;
            _field = new char[size, size];
            RegexParser parser = new RegexParser();
            _horizontalExpressions = horizontalExpressions;
            _verticalExpressions = verticalExpressions;
            int id = 0;
            _horizontalQuestions = horizontalExpressions.Select(item => new CrosswordQuestion(id++ ,parser.Parse(item))).ToArray();
            _verticalQuestions = verticalExpressions.Select(item => new CrosswordQuestion(id++, parser.Parse(item))).ToArray();

        }

        public override IEnumerable<CrosswordQuestion> GetQuestions()
        {
            foreach (var question in _horizontalQuestions)
                yield return question;

            foreach (var question in _verticalQuestions)
                yield return question;
        }

        public override string GetLineForQuestion(CrosswordQuestion question)
        {
            int rowIndex = Array.IndexOf(_horizontalQuestions, question);
            if (rowIndex != -1)
                return MatrixHelper.GetHorizontalLine(_field, rowIndex);
            int columnIndex = Array.IndexOf(_verticalQuestions, question);
            if (columnIndex != -1)
                return MatrixHelper.GetVerticalLine(_field, columnIndex);
            return null;
        }

        public override AnswerAttempt ApplyAnswer(CrosswordQuestion question, string answer)
        {
            int rowIndex = Array.IndexOf(_horizontalQuestions, question);
            if (rowIndex != -1)
                return ApplyAnswerToHorizontalQuestion(rowIndex, answer);
            int columnIndex = Array.IndexOf(_verticalQuestions, question);
            if (columnIndex != -1)
                return ApplyAnswerToVerticalQuestion(columnIndex, answer);
            return null;
        }

        public override void RollbackAnswerAttempt(AnswerAttempt attempt)
        {
            foreach (var cell in attempt.AffectedCells)
            {
                _field[cell.RowIndex, cell.ColumnIndex] = (char)0;
            }
        }

        public override bool IsSolved()
        {
            for (int i = 0; i < _size; i++)
            {
                var pattern = _horizontalExpressions[i];
                var line = MatrixHelper.GetHorizontalLine(_field, i);
                if (!Regex.IsMatch(line, "^" + pattern + "$"))
                    return false;
            }

            for (int j = 0; j < _size; j++)
            {
                var pattern = _verticalExpressions[j];
                var line = MatrixHelper.GetVerticalLine(_field, j);
                if (!Regex.IsMatch(line, "^" + pattern + "$"))
                    return false;
            }

            return true;
        }

        private AnswerAttempt ApplyAnswerToHorizontalQuestion(int rowIndex, string answer)
        {
            List<CrosswordQuestion> questions = new List<CrosswordQuestion>();
            List<CrosswordCell> cells = new List<CrosswordCell>();
            for (int j = 0; j < _size; j++)
                if (_field[rowIndex, j] == 0)
                {
                    _field[rowIndex, j] = answer[j];
                    cells.Add(new CrosswordCell(rowIndex, j));
                    questions.Add(_verticalQuestions[j]);
                }

            AnswerAttempt attempt = new AnswerAttempt(cells, questions);
            return attempt;
        }

        private AnswerAttempt ApplyAnswerToVerticalQuestion(int columnIndex, string answer)
        {
            List<CrosswordQuestion> questions = new List<CrosswordQuestion>();
            List<CrosswordCell> cells = new List<CrosswordCell>();
            for (int i = 0; i < _size; i++)
                if (_field[i, columnIndex] == 0)
                {
                    _field[i, columnIndex] = answer[i];
                    cells.Add(new CrosswordCell(i, columnIndex));
                    questions.Add(_horizontalQuestions[i]);
                }

            AnswerAttempt attempt = new AnswerAttempt(cells, questions);
            return attempt;
        }

        public override CrosswordCell[] GetCellsForQuestion(CrosswordQuestion question)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<CrosswordQuestion> GetQuestionsForCell(int rowIndex, int columnIndex)
        {
            throw new NotImplementedException();
        }
    }
}
