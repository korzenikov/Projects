using CrosswordSolverLib.RegexBlocks;

namespace CrosswordSolverLib.RegexClasses
{
    public abstract class RegexVisitor
    {
        #region Methods

        protected object Visit(RegularExpression expression)
        {
            return Visit(expression.InnerBlock);
        }

        protected object Visit(RegexBlock block)
        {
            var textBlock = block as TextBlock;
            if (textBlock != null)
            {
                return VisitTextBlock(textBlock);
            }

            var anyCharacterBlock = block as AnyCharacterBlock;
            if (anyCharacterBlock != null)
            {
                return VisitAnyCharacterBlock(anyCharacterBlock);
            }

            var inclusiveSetBlock = block as InclusiveSetBlock;
            if (inclusiveSetBlock != null)
            {
                return VisitInclusiveSetBlock(inclusiveSetBlock);
            }

            var exclusiveSetBlock = block as ExclusiveSetBlock;
            if (exclusiveSetBlock != null)
            {
                return VisitExclusiveSetBlock(exclusiveSetBlock);
            }

            var zeroOrOneBlock = block as ZeroOrOneBlock;
            if (zeroOrOneBlock != null)
            {
                return VisitZeroOrOneBlock(zeroOrOneBlock);
            }

            var zeroOrMoreBlock = block as ZeroOrMoreBlock;
            if (zeroOrMoreBlock != null)
            {
                return VisitZeroOrMoreBlock(zeroOrMoreBlock);
            }

            var oneOrMoreBlock = block as OneOrMoreBlock;
            if (oneOrMoreBlock != null)
            {
                return VisitOneOrMoreBlock(oneOrMoreBlock);
            }

            var orGroupBlock = block as OrGroupBlock;
            if (orGroupBlock != null)
            {
                return VisitOrGroupBlock(orGroupBlock);
            }

            var andGroupBlock = block as AndGroupBlock;
            if (andGroupBlock != null)
            {
                return VisitAndGroupBlock(andGroupBlock);
            }

            var backreferenceBlock = block as BackreferenceBlock;
            if (backreferenceBlock != null)
            {
                return VisitBackreferenceBlock(backreferenceBlock);
            }

            return block;
        }

        protected virtual object VisitTextBlock(TextBlock block)
        {
            return block;
        }

        protected virtual object VisitAnyCharacterBlock(AnyCharacterBlock block)
        {
            return block;
        }

        protected virtual object VisitAndGroupBlock(AndGroupBlock block)
        {
            foreach (RegexBlock innerBlock in block.InnerBlocks)
            {
                Visit(innerBlock);
            }

            return block;
        }

        protected virtual object VisitBackreferenceBlock(BackreferenceBlock block)
        {
            return block;
        }

        protected virtual object VisitExclusiveSetBlock(ExclusiveSetBlock block)
        {
            return block;
        }

        protected virtual object VisitInclusiveSetBlock(InclusiveSetBlock block)
        {
            return block;
        }

        protected virtual object VisitOneOrMoreBlock(OneOrMoreBlock block)
        {
            return Visit(block.InnerBlock);
        }

        protected virtual object VisitOrGroupBlock(OrGroupBlock block)
        {
            foreach (RegexBlock innerBlock in block.InnerBlocks)
            {
                Visit(innerBlock);
            }

            return block;
        }

        protected virtual object VisitZeroOrMoreBlock(ZeroOrMoreBlock block)
        {
            return Visit(block.InnerBlock);
        }

        protected virtual object VisitZeroOrOneBlock(ZeroOrOneBlock block)
        {
            return Visit(block.InnerBlock);
        }

        #endregion
    }
}