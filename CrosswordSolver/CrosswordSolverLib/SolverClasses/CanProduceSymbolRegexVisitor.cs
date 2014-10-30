using CrosswordSolverLib.RegexBlocks;
using CrosswordSolverLib.RegexClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrosswordSolverLib.SolverClasses
{
    public class CanProduceSymbolRegexVisitor : RegexVisitor
    {
        public bool Success { get; set; }

        public char Symbol { get; set; }

        public CanProduceSymbolRegexVisitor(char symbol)
        {
            Symbol = symbol;
        }

        protected override void VisitInclusiveSetBlock(InclusiveSetBlock block)
        {
            if (block.Characters.Contains(Symbol))
                Success = true;
        }


        protected override void VisitExclusiveSetBlock(ExclusiveSetBlock block)
        {
            if (!block.Characters.Contains(Symbol))
                Success = true;
        }

        protected override void VisitAnyCharacterBlock(AnyCharacterBlock block)
        {
            Success = true;
        }

        protected override void VisitTextBlock(TextBlock block)
        {
            if (block.Text.Contains(Symbol))
                Success = true;
        }
       
    }
}
