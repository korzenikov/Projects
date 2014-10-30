using CrosswordSolverLib.RegexBlocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrosswordSolverLib.RegexClasses
{
    public abstract class RegexVisitor
    {
        #region Public methods

        public void Visit(RegularExpression expression)
        {
            Visit(expression.InnerBlock);
        }

        public void Visit(RegexBlock block)
        {
            TextBlock textBlock = block as TextBlock;
            if (textBlock != null)
                VisitTextBlock(textBlock);

            AnyCharacterBlock anyCharacterBlock = block as AnyCharacterBlock;
            if (anyCharacterBlock != null)
                VisitAnyCharacterBlock(anyCharacterBlock);

            InclusiveSetBlock inclusiveSetBlock = block as InclusiveSetBlock;
            if (inclusiveSetBlock != null)
                VisitInclusiveSetBlock(inclusiveSetBlock);

            ExclusiveSetBlock exclusiveSetBlock = block as ExclusiveSetBlock;
            if (exclusiveSetBlock != null)
                VisitExclusiveSetBlock(exclusiveSetBlock);

            ZeroOrOneBlock zeroOrOneBlock = block as ZeroOrOneBlock;
            if (zeroOrOneBlock != null)
                VisitZeroOrOneBlock(zeroOrOneBlock);

            ZeroOrMoreBlock zeroOrMoreBlock = block as ZeroOrMoreBlock;
            if (zeroOrMoreBlock != null)
                VisitZeroOrMoreBlock(zeroOrMoreBlock);

            OneOrMoreBlock oneOrMoreBlock = block as OneOrMoreBlock;
            if (oneOrMoreBlock != null)
                VisitOneOrMoreBlock(oneOrMoreBlock);

            OrGroupBlock orGroupBlock = block as OrGroupBlock;
            if (orGroupBlock != null)
                VisitOrGroupBlock(orGroupBlock);

            AndGroupBlock anGroupBlock = block as AndGroupBlock;
            if (anGroupBlock != null)
                VisitAndGroupBlock(anGroupBlock);

            BackreferenceBlock backreferenceBlock = block as BackreferenceBlock;
            if (backreferenceBlock != null)
                VisitBackreferenceBlock(backreferenceBlock);
        }

        #endregion

        #region Protected Methods

        protected virtual void VisitBackreferenceBlock(BackreferenceBlock block)
        {
        }

        protected virtual void VisitAndGroupBlock(AndGroupBlock block)
        {
            foreach (var innerBlock in block.InnerBlocks)
            {
                Visit(innerBlock);
            }
        }

        protected virtual void VisitOrGroupBlock(OrGroupBlock block)
        {
            foreach (var innerBlock in block.InnerBlocks)
            {
                Visit(innerBlock);
            }
        }

        protected virtual void VisitZeroOrOneBlock(ZeroOrOneBlock block)
        {
            Visit(block.InnerBlock);
        }

        protected virtual void VisitZeroOrMoreBlock(ZeroOrMoreBlock block)
        {
            Visit(block.InnerBlock);
        }
        
        protected virtual void VisitOneOrMoreBlock(OneOrMoreBlock block)
        {
            Visit(block.InnerBlock);
        }

        protected virtual void VisitAnyCharacterBlock(AnyCharacterBlock block)
        {
        }

        protected virtual void VisitExclusiveSetBlock(ExclusiveSetBlock block)
        {
        }

        protected virtual void VisitInclusiveSetBlock(InclusiveSetBlock block)
        {
        }

        protected virtual void VisitTextBlock(TextBlock block)
        {
        }


        #endregion

        #region Private Methods

        #endregion
    }
}
