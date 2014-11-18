using CrosswordSolverLib;
using CrosswordSolverLib.RegexClasses;
using CrosswordSolverLib.SolverClasses;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CrosswordSolverLibTest.IntegrationTests
{
     [TestClass]
     public class CheckerTest 
    {
         [TestMethod]
         public void CheckTest1()
         {
             var input = "\0\0\0\0\0\0c\0\0\0\0\0";
             
             string regexPattern = "[^c]*mmm[^c]*";

             var parser = new RegexParser();
             var regex = parser.Parse(regexPattern);
             var checker = new Checker(input);

             Assert.IsFalse(checker.Check(regex));
         }

         [TestMethod]
         public void CheckTest2()
         {
             var input = "\0\0mmm\0\0\0";

             string regexPattern = "[^c]*mmm[^c]*";

             var parser = new RegexParser();
             var regex = parser.Parse(regexPattern);
             var checker = new Checker(input);

             Assert.IsTrue(checker.Check(regex));
         }

         [TestMethod]
         public void CheckTest3()
         {
             var input = "\0\0\0";

             string regexPattern = "[^c]*";

             var parser = new RegexParser();
             var regex = parser.Parse(regexPattern);
             var checker = new Checker(input);

             Assert.IsTrue(checker.Check(regex));
         }
    }
}
