using System.Collections.Generic;

using CrosswordSolverLib.RegexBlocks;
using CrosswordSolverLib.SolverClasses;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CrosswordSolverLibTest.UnitTests
{
    [TestClass]
    public class CheckerRegexVisitorTestClass 
    {
        #region Public Methods and Operators

        [TestMethod]
        public void GetPositionsFromAnyCharacterBlockTest()
        {
            string input = "\0";
            var visitor1 = new CheckerRegexVisitor(0, input, null);

            var block = new AnyCharacterBlock();
            var positions1 = visitor1.GetPositions(block);
            positions1.Should().BeEquivalentTo(new[] { 1 });

            input = "a";
            var visitor2 = new CheckerRegexVisitor(0, input, null);
            var positions2 = visitor2.GetPositions(block);
            positions2.Should().BeEquivalentTo(new[] { 1 });
        }

        [TestMethod]
        public void GetPositionsFromBackreferenceBlockTest()
        {
            var groupValues = new Dictionary<int, string>();
            string input = "\0\0";
            int groupId = 1;
            var visitor1 = new CheckerRegexVisitor(0, input, groupValues);

            groupValues[groupId] = "ab";
            var backreferenceBlock = new BackreferenceBlock(groupId);

            var positions1 = visitor1.GetPositions(backreferenceBlock);
            positions1.Should().BeEquivalentTo(new[] { 2 });

            groupValues[groupId] = "abc";
            var visitor2 = new CheckerRegexVisitor(0, input, groupValues);
            var positions2 = visitor2.GetPositions(backreferenceBlock);
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
            var textBlock1 = new TextBlock("a");
            var visitor1 = new CheckerRegexVisitor(0, "\0", null);

            var positions1 = visitor1.GetPositions(textBlock1);
            positions1.Should().BeEquivalentTo(new[] { 1 });

            var visitor2 = new CheckerRegexVisitor(1, "\0", null);
            var positions2 = visitor2.GetPositions(textBlock1);
            positions2.Should().BeEmpty();

            // Text block longer than symbols that remain unfilled.
            var textBlock2 = new TextBlock("aa");

            var visitor3 = new CheckerRegexVisitor(0, "\0", null);
            var positions3 = visitor3.GetPositions(textBlock2);
            positions3.Should().BeEmpty();

            var visitor4 = new CheckerRegexVisitor(0, "aa", null);
            var positions4 = visitor4.GetPositions(textBlock2);
            positions4.Should().BeEquivalentTo(new[] { 2 });

            var textBlock3 = new TextBlock("ab");
            var visitor5 = new CheckerRegexVisitor(0, "aa", null);
            var positions5 = visitor5.GetPositions(textBlock3);
            positions5.Should().BeEmpty();
        }

        #endregion

        #region Private Methods


        #endregion
    }
}