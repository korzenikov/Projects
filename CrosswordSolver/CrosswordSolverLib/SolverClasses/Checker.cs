using System.Collections.Generic;
using System.Linq;

using CrosswordSolverLib.RegexBlocks;
using CrosswordSolverLib.RegexClasses;

namespace CrosswordSolverLib.SolverClasses
{
    public class Checker
    {
        private readonly string _input;

        private readonly Dictionary<int, string> _groupValues = new Dictionary<int, string>();

        private int _nextGroupId;

        #region Constructor

        public Checker(string input)
        {
            _input = input;
        }

        #endregion

        #region Properties

        #endregion

        #region Public Methods

        public bool Check(RegularExpression expression)
        {
            return GetPositions(0, expression.InnerBlock).Any(position => position == _input.Length);
        }

        public IEnumerable<int> GetPositions(int position, RegexBlock block)
        {
            var zeroOrOneBlock = block as ZeroOrOneBlock;
            if (zeroOrOneBlock != null)
                return GetPositionsFromZeroOrOneBlock(position, zeroOrOneBlock);

            var zeroOrMoreBlock = block as ZeroOrMoreBlock;
            if (zeroOrMoreBlock != null)
                return GetPositionsFromZeroOrMoreBlock(position, zeroOrMoreBlock);

            var oneOrMoreBlock = block as OneOrMoreBlock;
            if (oneOrMoreBlock != null)
                return GetPositionsFromOneOrMoreBlock(position, oneOrMoreBlock);

            var orGroupBlock = block as OrGroupBlock;
            if (orGroupBlock != null)
                return GetPositionsFromOrGroupBlock(position, orGroupBlock);

            var andGroupBlock = block as AndGroupBlock;
            if (andGroupBlock != null)
                return GetPositionsFromAndGroupBlock(position, andGroupBlock);

            var visitor = new CheckerRegexVisitor(position, _input, _groupValues);
            return visitor.GetPositions(block);
        }

        public IEnumerable<int> GetPositionsFromZeroOrOneBlock(int position, ZeroOrOneBlock block)
        {
            yield return position;

            foreach (var nextPosition in GetPositions(position, block.InnerBlock))
            {
                yield return nextPosition;
            }
        }

        public IEnumerable<int> GetPositionsFromOneOrMoreBlock(int position, OneOrMoreBlock block)
        {
            return GetPositionsFromQuanitifierBlock(position, block);
        }

        public IEnumerable<int> GetPositionsFromZeroOrMoreBlock(int position, ZeroOrMoreBlock block)
        {
            yield return position;
            foreach (var nextPosition in GetPositionsFromQuanitifierBlock(position, block))
            {
                yield return nextPosition;
            }
        }

        public IEnumerable<int> GetPositionsFromOrGroupBlock(int position, OrGroupBlock orGroupBlock)
        {
            int groupId = _nextGroupId++;
            foreach (var block in orGroupBlock.InnerBlocks)
            {
                foreach (var nextPosition in GetPositions(position, block))
                {
                    SetGroupValue(groupId, _input.Substring(position, nextPosition - position));
                    yield return nextPosition;
                }
            }
        }

        public IEnumerable<int> GetPositionsFromAndGroupBlock(int position, AndGroupBlock andGroupbBlock)
        {
            int groupId = _nextGroupId++;
            foreach (var nextPosition in GetPositionsFromBlocks(position, andGroupbBlock.InnerBlocks))
            {
                SetGroupValue(groupId, _input.Substring(position, nextPosition - position));
                yield return nextPosition;
            }

            _nextGroupId--;
        }

        public void SetGroupValue(int groupId, string value)
        {
            _groupValues[groupId] = value;
        }

        #endregion

        #region Private Methods

        private IEnumerable<int> GetPositionsFromBlocks(int position, IEnumerable<RegexBlock> blocks)
        {
            var firstBlock  = blocks.FirstOrDefault();
            if (firstBlock == null)
                yield return position;
            else
            {
                var restBlocks = blocks.Skip(1).ToArray();
                foreach (var nextPosition in GetPositions(position, firstBlock))
                {
                    if (restBlocks.Length == 0)
                    {
                        yield return nextPosition;
                    }
                    else
                        foreach (var nestedNextPosition in GetPositionsFromBlocks(nextPosition, restBlocks))
                        {
                            yield return nestedNextPosition;
                        }
                }
            }
        }

        private IEnumerable<int> GetPositionsFromQuanitifierBlock(int position, QuantifierBlock block)
        {
            return GetPositionsFromBlock(position, block.InnerBlock);
        }

        private IEnumerable<int> GetPositionsFromBlock(int position, RegexBlock block)
        {
            foreach (var nextPosition in GetPositions(position, block))
            {
                yield return nextPosition;
                foreach (var nestedNextPosition in GetPositionsFromBlock(nextPosition, block))
                {
                    yield return nestedNextPosition;
                }
            }
        }

        #endregion
    }
}
