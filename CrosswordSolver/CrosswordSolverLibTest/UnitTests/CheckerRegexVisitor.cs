using System.Collections.Generic;

using CrosswordSolverLib.LineBuilderClasses;
using CrosswordSolverLib.RegexBlocks;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CrosswordSolverLibTest
{
    [TestClass]
    public class CheckerRegexVisitorTestClass : BaseTestClass
    {
        #region Public Methods and Operators

        [TestMethod]
        public void GetPositionsFromAnyCharacterBlockTest()
        {
            Assert.Inconclusive();
            //string input = "\0";
            //var visitor1 = new BuilderRegexVisitor(0, input, null, null);

            //var block = new AnyCharacterBlock();

            //var expectedLines = new List<string>();
            //for (char c = 'a'; c < 'z'; c++)
            //{
            //    expectedLines.Add(c.ToString());
            //}

            //CheckVisitorResult(visitor1, block, expectedLines);

            //input = "a";
            //var visitor2 = new BuilderRegexVisitor(0, input, null, null);

            //CheckVisitorResult(visitor2, block, new[] { "a" });
        }

        [TestMethod]
        public void GetPositionsFromBackreferenceBlockTest()
        {
            var groupValues = new Dictionary<int, string>();
            string input = "\0\0'";
            int groupId = 1;
            var visitor1 = new CheckerRegexVisitor(0, input, groupValues);

            groupValues[groupId] = "ab";
            var backreferenceBlock = new BackreferenceBlock(groupId);

            string[] expectedLines = { "ab" };
            var positions1 = visitor1.GetPositions(backreferenceBlock);
            positions1.Should().BeEquivalentTo(new[] { 2 });

            groupValues[groupId] = "abc";
            var visitor2 = new CheckerRegexVisitor(0, input, groupValues);
            var positions2 = visitor1.GetPositions(backreferenceBlock);
            positions2.Should().BeEmpty();
        }

        [TestMethod]
        public void GetPositionsFromExclusiveSetBlockTest()
        {
            string input = "\0";
            var visitor1 = new CheckerRegexVisitor(0, input, null);
            string characters = "abc";
            var exclusiveSetBlock = new ExclusiveSetBlock(characters);

            var positions1 = visitor1.GetPositions(exclusiveSetBlock);
            positions1.Should().BeEquivalentTo(new[] { 1 });
            
            input = "a";
            var visitor2 = new CheckerRegexVisitor(0, input, null);

            var positions2 = visitor2.GetPositions(exclusiveSetBlock);
            positions2.Should().BeEmpty();

            input = "d";
            var visitor3 = new CheckerRegexVisitor(0, input, null);

            var positions3 = visitor3.GetPositions(exclusiveSetBlock);
            positions3.Should().BeEquivalentTo(new[] { 1 });
        }

        [TestMethod]
        public void GetPositionsFromInclusiveSetBlockTest()
        {
            string input = "\0";
            var visitor1 = new CheckerRegexVisitor(0, input, null);
            var inclusiveSetBlock = new InclusiveSetBlock("abc");

            var positions1 = visitor1.GetPositions(inclusiveSetBlock);
            positions1.Should().BeEquivalentTo(new[] { 1 });

            input = "a";
            var visitor2 = new CheckerRegexVisitor(0, input, null);
            var positions2 = visitor2.GetPositions(inclusiveSetBlock);
            positions2.Should().BeEquivalentTo(new[] { 1 });

            input = "d";
            var visitor3 = new CheckerRegexVisitor(0, input, null);

            var positions3 = visitor3.GetPositions(inclusiveSetBlock);
            positions3.Should().BeEmpty();
        }

        [TestMethod]
        public void GetPositionsFromTextBlockTest()
        {
            Assert.Inconclusive();
            //var visitor1 = new BuilderRegexVisitor(0, "\0", null, null);
            //var textBlock = new TextBlock("a");

            //CheckVisitorResult(visitor1, textBlock, new[] { "a" });

            //var visitor2 = new BuilderRegexVisitor(1, "\0", null, null);
            //CheckVisitorResult(visitor2, textBlock, Enumerable.Empty<string>());

            //// Text block longer than symbols that remain unfilled.
            //textBlock = new TextBlock("aa");

            //var visitor3 = new BuilderRegexVisitor(0, "\0", null, null);
            //CheckVisitorResult(visitor3, textBlock, Enumerable.Empty<string>());

            //string input = "aa";
            //var visitor4 = new BuilderRegexVisitor(0, input, null, null);
            //CheckVisitorResult(visitor4, textBlock, new[] { "aa" });

            //var visitor5 = new BuilderRegexVisitor(0, input, null, null);
            //textBlock = new TextBlock("ab");

            //CheckVisitorResult(visitor5, textBlock, Enumerable.Empty<string>());
        }

        #endregion

        #region Methods

        //private void CheckVisitorResult(BuilderRegexVisitor visitor, RegexBlock block, IEnumerable<string> expectedLines)
        //{
        //    visitor.Visit(block);
        //    IEnumerable<string> lines = visitor.Result;
        //    CollectionAssert.AreEquivalent(expectedLines.ToList(), lines.ToList());
        //}

        #endregion
    }
}