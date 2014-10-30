using System;

using CrosswordSolverLib.CrosswordClasses;
using CrosswordSolverLib.SolverClasses;

namespace CrosswordSolverClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var solver = new Solver();
            int size = 6;

            var leftRightExpressions = new[]
                                           {
                                               ".*h.*h.*",
                                               "(di|ns|th|om)*",
                                               "f.*[ao].*[ao].*",
                                               "(o|rhh|mm)*",
                                               ".*",
                                               "c*mc(ccc|mm)*",
                                               "[^c]*[^r]*iii.*",
                                               @"(...?)\1*",
                                               "([^x]|xcc)*",
                                               "(rr|hhh)*.?",
                                               "n.*x.x.x.*e",
                                               "r*d*m*",
                                               ".(c|hh)*"
                                           };
            var bottomTopExpressions = new[]
                                           {
                                               ".*g.*v.*h.*",
                                               "[cr]*",
                                               ".*xexm*",
                                               ".*dd.*ccm.*",
                                               ".*xhcr.*x.*",
                                               @".*(.)(.)(.)(.)\4\3\2\1.*",
                                               ".*(in|se|hi)",
                                               "[^c]*mmm[^c]*",
                                               @".*(.)c\1x\1.*",
                                               "[ceimu]*oh[aemor]*",
                                               "(rx|[^r])*",
                                               "[^m]*m[^m]*",
                                               "(s|mm|hhh)*",
                                           };

            var topBottomExpressions = new[]
                                           {
                                               "(nd|et|in)[^x]*",
                                               "[chmnor]*i[chmnor]*",
                                               @"p+(..)\1.*",
                                               "(e|cr|mn)*",
                                               "([^mc]|mm|cc)*",
                                               "[am]*cm(rc)*r?",
                                               ".*",
                                               ".*prr.*ddc.*",
                                               "(hhx|[^hx])*",
                                               "([^emc]|em)*",
                                               ".*oxr.*",
                                               ".*lr.*rl.*",
                                               ".*se.*ue.*",
                                           };

            var crossword = new HexagonCrossword(size, leftRightExpressions, bottomTopExpressions, topBottomExpressions);
            solver.Solve(crossword);
            Console.WriteLine("Is solved: {0}", crossword.IsSolved());
            crossword.Print();
            Console.WriteLine("Press ENTER to exit");
            Console.ReadLine();
        }
    }
}
