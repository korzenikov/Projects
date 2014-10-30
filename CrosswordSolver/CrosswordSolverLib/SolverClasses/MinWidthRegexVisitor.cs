using CrosswordSolverLib.RegexBlocks;
using CrosswordSolverLib.RegexClasses;

namespace CrosswordSolverLib.SolverClasses
{
    public class MinWidthRegexVisitor : RegexVisitor
    {
        public int MinWidth { get; set; }

        protected override void VisitTextBlock(TextBlock block)
        {
            MinWidth = block.Text.Length;
        }

        protected override void VisitAnyCharacterBlock(AnyCharacterBlock block)
        {
            MinWidth = 1;
        }

        protected override void VisitInclusiveSetBlock(InclusiveSetBlock block)
        {
            MinWidth = 1;
        }

        protected override void VisitExclusiveSetBlock(ExclusiveSetBlock block)
        {
            MinWidth = 1;
        }

        protected override void VisitZeroOrMoreBlock(ZeroOrMoreBlock block)
        {
            MinWidth = 0;
        }

        protected override void VisitZeroOrOneBlock(ZeroOrOneBlock block)
        {
            MinWidth = 0;
        }

        protected override void VisitOneOrMoreBlock(OneOrMoreBlock block)
        {
            var visitor = new MinWidthRegexVisitor();
            visitor.Visit(block.InnerBlock);
            MinWidth = visitor.MinWidth;
        }

        protected override void VisitOrGroupBlock(OrGroupBlock block)
        {
            MinWidth = int.MaxValue;
            foreach (var innerBlock in block.InnerBlocks)
            {
                var visitor = new MinWidthRegexVisitor();
                visitor.Visit(innerBlock);
                if (visitor.MinWidth < MinWidth)
                    MinWidth = visitor.MinWidth;
            }
        }

        protected override void VisitAndGroupBlock(AndGroupBlock block)
        {
            MinWidth = 0;
            foreach (var innerBlock in block.InnerBlocks)
            {
                var visitor = new MinWidthRegexVisitor();
                visitor.Visit(innerBlock);
                MinWidth += visitor.MinWidth;
            }
        }
    }
}
