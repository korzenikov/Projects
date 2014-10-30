using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CrosswordSolverLibTest
{
    public class BaseTestClass
    {
        protected void CheckAgainstRegularExpression(string pattern, string line, bool greedyCheck = true)
        {
            if (greedyCheck)
                pattern = WrapToEntireStringOnlyPattern(pattern);
            
            Regex regex = new Regex(pattern);
            Assert.IsTrue(regex.IsMatch(line), "Generated string {0} doesn't match regular expression {1}", line, pattern);
        }

        protected void CheckGeneratedLines(List<string> lines, string regexPattern)
        {
            foreach (var line in lines)
            {
                CheckAgainstRegularExpression(regexPattern, line);
            }
        }

        private static string WrapToEntireStringOnlyPattern(string pattern)
        {
            return "^" + pattern + "$";
        }
    }
}
