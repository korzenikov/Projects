using CrosswordSolverLib.RegexBlocks;
using CrosswordSolverLib.RegexClasses;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CrosswordSolverLib.LineBuilderClasses
{
    public class BuilderRegexVisitor : RegexVisitor
    {
        private readonly int _position;
        private readonly string _input;
        private readonly Dictionary<int, string> _groupValues;
        private readonly Dictionary<int, List<char>> _incorrectSymbolsDictionary;

        public BuilderRegexVisitor(int position, string input, Dictionary<int, string> groupValues, Dictionary<int, List<char>> incorrectSymbolsDictionary)
        {
            _position = position;
            _input = input;
            _groupValues = groupValues;
            _incorrectSymbolsDictionary = incorrectSymbolsDictionary;
            Result = Enumerable.Empty<string>();
        }

        #region Properties

        public IEnumerable<string> Result { get; private set; }

        public int MaxWidth { get; set; }

        #endregion

        protected override void VisitTextBlock(TextBlock block)
        {
            string text = block.Text;
            if (CanUseText(_position, text))
                Result = new [] { text };
        }

        protected override void VisitAnyCharacterBlock(AnyCharacterBlock block)
        {
            int length = _input.Length;
            if (_position < length)
            {
                var currentSymbol = _input[_position];
                if (currentSymbol == 0)
                {
                    Result = GetAllLetters().Select(c => c.ToString());
                }
                else
                    Result = new[] { currentSymbol.ToString() };
            }
        }

        protected override void VisitExclusiveSetBlock(ExclusiveSetBlock block)
        {
            string characters = block.Characters;
            int length = _input.Length;
            if (_position < length)
            {
                var currentSymbol = _input[_position];
                if (currentSymbol == 0)
                    Result = GetAllLetters().Where(c => characters.IndexOf(c) == -1 && !GetForbiddenSymbols(_position).Contains(c)).Select(c => c.ToString());
                else if (characters.IndexOf(currentSymbol) == -1)
                    Result = new[] { currentSymbol.ToString() };
            }
        }

        protected override void VisitInclusiveSetBlock(InclusiveSetBlock block)
        {
            string characters = block.Characters;
            int length = _input.Length;
            if (_position < length)
            {
                var currentSymbol = _input[_position];
                if (currentSymbol == 0)
                    Result = characters.Where(c =>!GetForbiddenSymbols(_position).Contains(c)).Select(c => c.ToString());
                else if (characters.IndexOf(currentSymbol) != -1)
                    Result = new[] { currentSymbol.ToString() };
            }
        }

        protected override void VisitBackreferenceBlock(BackreferenceBlock block)
        {
            string text;
            if (!_groupValues.TryGetValue(block.GroupIndex, out text))
                throw new ArgumentException("Reference to unspecified group", "block");
            if (CanUseText(_position, text))
                Result = new[] { text };
        }

        #region Private Methods

        private bool CanUseText(int position, string text)
        {
            int length = _input.Length;
            if (position + text.Length > length)
                return false;

            for (int i = 0; i < text.Length; i++)
                if ((_input[position + i] != 0 && text[i] != _input[position + i]) || GetForbiddenSymbols(position + i).Contains(text[i]))
                    return false;

            return true;
        }

        private IEnumerable<char> GetForbiddenSymbols(int position)
        {
            List<char> symbols;
            if (_incorrectSymbolsDictionary != null && _incorrectSymbolsDictionary.TryGetValue(position, out symbols))
                return symbols;
            return Enumerable.Empty<char>();
        }


        private IEnumerable<char> GetAllLetters()
        {
            for (char c = 'a'; c < 'z'; c++)
                yield return c;
        }

        #endregion
    }
}
