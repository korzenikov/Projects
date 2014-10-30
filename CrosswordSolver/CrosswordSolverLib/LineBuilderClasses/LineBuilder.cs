using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrosswordSolverLib.RegexClasses;
using CrosswordSolverLib.RegexBlocks;

namespace CrosswordSolverLib.LineBuilderClasses
{
    public class LineBuilder
    {
        private string _input;

        private Dictionary<int, string> _groupValues = new Dictionary<int, string>();
        private Dictionary<int, List<char>> _incorrectSymbolsDictionary;
        private int _nextGroupId;

        #region Constructor

        public LineBuilder(string input, Dictionary<int, List<char>> incorrectSymbolsDictionary)
        {
            _input = input;
            _incorrectSymbolsDictionary = incorrectSymbolsDictionary;
        }

        #endregion

        #region Properties

        

        #endregion

        #region Public Methods

        public IEnumerable<string> GetLines(RegularExpression expression)
        {
            foreach (string line in GetLines(0, expression.InnerBlock))
                if (line.Length == _input.Length)
                    yield return line;
        }

        public IEnumerable<string> GetLines(int position, RegexBlock block)
        {
            ZeroOrOneBlock zeroOrOneBlock = block as ZeroOrOneBlock;
            if (zeroOrOneBlock != null)
                return GetLinesFromZeroOrOneBlock(position, zeroOrOneBlock);

            ZeroOrMoreBlock zeroOrMoreBlock = block as ZeroOrMoreBlock;
            if (zeroOrMoreBlock != null)
                return GetLinesFromZeroOrMoreBlock(position, zeroOrMoreBlock);

            OneOrMoreBlock oneOrMoreBlock = block as OneOrMoreBlock;
            if (oneOrMoreBlock != null)
                return GetLinesFromOneOrMoreBlock(position, oneOrMoreBlock);

            OrGroupBlock orGroupBlock = block as OrGroupBlock;
            if (orGroupBlock != null)
                return GetLinesFromOrGroupBlock(position, orGroupBlock);

            AndGroupBlock anGroupBlock = block as AndGroupBlock;
            if (anGroupBlock != null)
                return GetLinesFromAndGroupBlock(position, anGroupBlock);

            BuilderRegexVisitor visitor = new BuilderRegexVisitor(position, _input, _groupValues, _incorrectSymbolsDictionary);
            visitor.Visit(block);
            return visitor.Result;

            throw new ArgumentException("Argument type is not recognized", "block");
        }

        public IEnumerable<string> GetLinesFromZeroOrOneBlock(int position, ZeroOrOneBlock block)
        {
            yield return string.Empty;

            foreach (var line in GetLines(position, block.InnerBlock))
            {
                yield return line;
            }
        }

        public IEnumerable<string> GetLinesFromOneOrMoreBlock(int position, OneOrMoreBlock block)
        {
            return GetLinesFromQuanitifierBlock(position, block);
        }

        public IEnumerable<string> GetLinesFromZeroOrMoreBlock(int position, ZeroOrMoreBlock block)
        {
            yield return string.Empty;
            foreach (var line in GetLinesFromQuanitifierBlock(position, block))
            {
                yield return line;
            }
        }
                
        public IEnumerable<string> GetLinesFromOrGroupBlock(int position, OrGroupBlock orGroupBlock)
        {
            int groupId = _nextGroupId++;
            foreach (var block in orGroupBlock.InnerBlocks)
            {
                foreach (var line in GetLines(position, block))
                {
                    SetGroupValue(groupId, line);
                    yield return line;
                }
            }
        }

        public IEnumerable<string> GetLinesFromAndGroupBlock(int position, AndGroupBlock andGroupbBlock)
        {
            int groupId = _nextGroupId++;
            foreach (var line in GetLinesFromBlocks(position, string.Empty, andGroupbBlock.InnerBlocks))
            {
                SetGroupValue(groupId, line);
                yield return line;
            }
        }

        public void SetGroupValue(int groupId, string value)
        {
            _groupValues[groupId] = value;
        }

        #endregion

        #region Private Methods

        private IEnumerable<string> GetLinesFromBlocks(int position, string prefix, IEnumerable<RegexBlock> blocks)
        {
            var firstBlock  = blocks.FirstOrDefault();
            if (firstBlock == null)
                yield return prefix;
            else
            {
                var restBlocks = blocks.Skip(1).ToArray();
                foreach (var line in GetLines(position + prefix.Length, firstBlock))
                {
                    string newPrefix = prefix + line;
                    foreach (var newLine in GetLinesFromBlocks(position, newPrefix, restBlocks))
                    {
                        yield return newLine;
                    }
                }
            }
        }

        private IEnumerable<string> GetLinesFromBlock(int position, string prefix, RegexBlock block)
        {
            foreach (var line in GetLines(position + prefix.Length, block))
            {
                string newPrefix = prefix + line;
                yield return newPrefix;
                foreach (var newLine in GetLinesFromBlock(position, newPrefix, block))
                {
                    yield return newLine;
                }
            }
        }

        private IEnumerable<string> GetLinesFromQuanitifierBlock(int position, QuantifierBlock block)
        {
            return GetLinesFromBlock(position, string.Empty, block.InnerBlock);
        }

        #endregion
    }
}