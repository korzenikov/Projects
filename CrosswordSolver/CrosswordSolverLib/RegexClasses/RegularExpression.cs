using CrosswordSolverLib.RegexBlocks;

namespace CrosswordSolverLib.RegexClasses
{
    public class RegularExpression
    {
        public RegularExpression(GroupBlock innerBlock)
        {
            InnerBlock = innerBlock;
        }

        public GroupBlock InnerBlock { get; private set; }
    }
}
