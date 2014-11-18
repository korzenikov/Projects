using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

using CrosswordSolverLib.RegexBlocks;
using CrosswordSolverLib.RegexClasses;

namespace CrosswordSolverLib.SolverClasses
{
    public class CheckerRegexVisitor : RegexVisitor
    {
        private readonly int _position;
        private readonly string _input;
        private readonly Dictionary<int, string> _groupValues;

        private readonly int[] _errorResult = { };

        public CheckerRegexVisitor(int position, string input, Dictionary<int, string> groupValues)
        {
            _position = position;
            _input = input;
            _groupValues = groupValues;
        }

        #region Public Methods

        public IEnumerable<int> GetPositions(RegularExpression expression)
        {
            var enumerable = Visit(expression) as IEnumerable<int>;
            Contract.Assert(enumerable != null);
            return enumerable;
        }

        public IEnumerable<int> GetPositions(RegexBlock block)
        {
            var enumerable = Visit(block) as IEnumerable<int>;
            Contract.Assert(enumerable != null);
            return enumerable;
        }

        #endregion

        protected override object VisitTextBlock(TextBlock block)
        {
            string text = block.Text;
            if (CanUseText(_position, text))
                return new[] { _position + text.Length };
            return _errorResult;
        }

        protected override object VisitAnyCharacterBlock(AnyCharacterBlock block)
        {
            int length = _input.Length;
            if (_position < length)
            {
                return new[] { _position + 1 };
            }

            return _errorResult;
        }

        protected override object VisitExclusiveSetBlock(ExclusiveSetBlock block)
        {
            string characters = block.Characters;
            int length = _input.Length;
            if (_position < length)
            {
                var currentSymbol = _input[_position];
                if (currentSymbol == 0)
                {
                    return new[] { _position + 1 };
                }

                if (characters.IndexOf(currentSymbol) == -1)
                    return new[] { _position + 1 };
            }

            return _errorResult;
        }

        protected override object VisitInclusiveSetBlock(InclusiveSetBlock block)
        {
            string characters = block.Characters;
            int length = _input.Length;
            if (_position < length)
            {
                var currentSymbol = _input[_position];
                if (currentSymbol == 0)
                {
                    return new[] { _position + 1 };
                }

                if (characters.IndexOf(currentSymbol) != -1)
                    return new[] { _position + 1 };
            }

            return _errorResult;
        }

        protected override object VisitBackreferenceBlock(BackreferenceBlock block)
        {
            string text;
            if (!_groupValues.TryGetValue(block.GroupIndex, out text))
                throw new ArgumentException("Reference to unspecified group", "block");
            if (CanUseText(_position, text))
                return new[] { _position + text.Length };
            return _errorResult;
        }

        #region Private Methods

        private bool CanUseText(int position, string text)
        {
            int length = _input.Length;
            if (position + text.Length > length)
                return false;

            for (int i = 0; i < text.Length; i++)
                if (_input[position + i] != 0 && text[i] != 0 && text[i] != _input[position + i])
                    return false;

            return true;
        }

        #endregion
    }
}
