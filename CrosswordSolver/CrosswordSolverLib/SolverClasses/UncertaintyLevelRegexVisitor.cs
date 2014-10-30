using CrosswordSolverLib.RegexBlocks;
using CrosswordSolverLib.RegexClasses;

namespace CrosswordSolverLib.SolverClasses
{
    public class UncertaintyLevelRegexVisitor : RegexVisitor
    {
        public int UncertaintyLevel { get; set; }

        protected override void VisitExclusiveSetBlock(ExclusiveSetBlock block)
        {
            UncertaintyLevel = 1;
            base.VisitExclusiveSetBlock(block);
        }

        protected override void VisitAnyCharacterBlock(AnyCharacterBlock block)
        {
            UncertaintyLevel = 2;
            base.VisitAnyCharacterBlock(block);
        }
    }
}
