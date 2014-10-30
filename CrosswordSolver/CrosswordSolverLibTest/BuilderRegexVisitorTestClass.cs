using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CrosswordSolverLib.LineBuilderClasses;
using System.Collections.Generic;
using CrosswordSolverLib.RegexBlocks;

namespace CrosswordSolverLibTest
{
    [TestClass]
    public class BuilderRegexVisitorTestClass : BaseTestClass
    {
        [TestMethod]
        public void GetLinesFromTextBlockTest()
        {
            var visitor1 = new BuilderRegexVisitor(0, "\0", null, null);
            var textBlock = new TextBlock("a");

            CheckVisitorResult(visitor1, textBlock, new[] { "a" });

            var visitor2 = new BuilderRegexVisitor(1, "\0", null, null);
            CheckVisitorResult(visitor2, textBlock, Enumerable.Empty<string>());

            // Text block longer than symbols that remain unfilled.
            textBlock = new TextBlock("aa");

            var visitor3 = new BuilderRegexVisitor(0, "\0", null, null);
            CheckVisitorResult(visitor3, textBlock, Enumerable.Empty<string>());

            var input = "aa";
            var visitor4 = new BuilderRegexVisitor(0, input, null, null);
            CheckVisitorResult(visitor4, textBlock, new[] { "aa" });

            var visitor5 = new BuilderRegexVisitor(0, input, null, null);
            textBlock = new TextBlock("ab");

            CheckVisitorResult(visitor5, textBlock, Enumerable.Empty<string>());
        }

        [TestMethod]
        public void GetLinesFromInclusiveSetBlockTest()
        {
            var input = "\0";
            var visitor1 = new BuilderRegexVisitor(0, input, null, null);
            var inclusiveSetBlock = new InclusiveSetBlock("abc");

            string[] expectedLines = new string[] { "a", "b", "c" };

            CheckVisitorResult(visitor1, inclusiveSetBlock, expectedLines);

            input = "a";
            var visitor2 = new BuilderRegexVisitor(0, input, null, null);

            expectedLines = new string[] { "a" };
            CheckVisitorResult(visitor2, inclusiveSetBlock, expectedLines);
        }

        [TestMethod]
        public void GetLinesFromExclusiveSetBlockTest()
        {
            var input = "\0";
            var visitor1 = new BuilderRegexVisitor(0, input, null, null);
            var characters = "abc";
            var exclusiveSetBlock = new ExclusiveSetBlock(characters);

            List<string> expectedLines = new List<string>();
            for (char c = 'a'; c < 'z'; c++)
            {
                if (characters.IndexOf(c) == -1)
                    expectedLines.Add(c.ToString());
            }

            CheckVisitorResult(visitor1, exclusiveSetBlock, expectedLines);

            input = "a";
            var visitor2 = new BuilderRegexVisitor(0, input, null, null);
            CheckVisitorResult(visitor2, exclusiveSetBlock, Enumerable.Empty<string>());

            input = "d";
            var visitor3 = new BuilderRegexVisitor(0, input, null, null);
            CheckVisitorResult(visitor3, exclusiveSetBlock, new[] { "d" });
        }

        [TestMethod]
        public void GetLinesFromAnyCharacterBlockTest()
        {
            var input = "\0";
            var visitor1 = new BuilderRegexVisitor(0, input, null, null);

            var block = new AnyCharacterBlock();

            var expectedLines = new List<string>();
            for (char c = 'a'; c < 'z'; c++)
                expectedLines.Add(c.ToString());

            CheckVisitorResult(visitor1, block, expectedLines);

            input = "a";
            var visitor2 = new BuilderRegexVisitor(0, input, null, null);

            CheckVisitorResult(visitor2, block, new[] { "a" });
        }

        [TestMethod]
        public void GetLinesFromBackreferenceBlockTest()
        {
            Dictionary<int, string> groupValues = new Dictionary<int,string>();
            var input  = "\0\0'";
            int groupId = 1;
            var visitor1 = new BuilderRegexVisitor(0, input, groupValues, null);
            
            groupValues[groupId] = "ab";
            var backreferenceBlock = new BackreferenceBlock(groupId);

            string[] expectedLines = new string[] { "ab" };
            CheckVisitorResult(visitor1, backreferenceBlock, expectedLines);

            groupValues[groupId] = "abc";
            var visitor2 = new BuilderRegexVisitor(0, input, groupValues, null);
            CheckVisitorResult(visitor2, backreferenceBlock, Enumerable.Empty<string>());

        }

        #region Private Methods

        private void CheckVisitorResult(BuilderRegexVisitor visitor, RegexBlock block, IEnumerable<string> expectedLines)
        {
            visitor.Visit(block);
            var lines = visitor.Result;
            CollectionAssert.AreEquivalent(expectedLines.ToList(), lines.ToList());
        }

        #endregion
    }
}
