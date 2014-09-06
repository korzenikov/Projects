using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RedPill.Contract;
using RedPill.Implementation;

namespace RedPill.Test
{
    [TestClass]
    public class RedPillTest
    {
        [TestMethod]
        public void FibonacciNumberTest()
        {
            var service = new RedPillService();

            service.FibonacciNumber(-7).Should().Be(13);

            service.FibonacciNumber(-3).Should().Be(2);
            service.FibonacciNumber(-2).Should().Be(-1);
            service.FibonacciNumber(-1).Should().Be(1);

            service.FibonacciNumber(0).Should().Be(0);

            service.FibonacciNumber(1).Should().Be(1);
            service.FibonacciNumber(2).Should().Be(1);
            service.FibonacciNumber(3).Should().Be(2);
            service.FibonacciNumber(7).Should().Be(13);

            Action a1 = () => service.FibonacciNumber(93);
            a1.ShouldThrow<ArgumentOutOfRangeException>();

            Action a2 = () => service.FibonacciNumber(-93);
            a2.ShouldThrow<ArgumentOutOfRangeException>();
        }

        [TestMethod]
        public void ReverseWordsTest()
        {
            var service = new RedPillService();

            service.ReverseWords("And I think to myself, what a wonderful world.").Should().Be("dnA I kniht ot ,flesym tahw a lufrednow .dlrow");
            service.ReverseWords("If I ask to come with me, will you go?").Should().Be("fI I ksa ot emoc htiw ,em lliw uoy ?og");

            Action a = () => service.ReverseWords(null);
            a.ShouldThrow<ArgumentNullException>();
        }

        [TestMethod]
        public void WhatShapeIsThisTest()
        {
            var service = new RedPillService();

            service.WhatShapeIsThis(5, 5, 5 ).Should().Be(TriangleType.Equilateral);
            service.WhatShapeIsThis(3, 4, 5).Should().Be(TriangleType.Scalene);
            service.WhatShapeIsThis(2, 2, 3).Should().Be(TriangleType.Isosceles);
            service.WhatShapeIsThis(2, 2, 4).Should().Be(TriangleType.Error);
            service.WhatShapeIsThis(-1, -1, -1).Should().Be(TriangleType.Error);
            service.WhatShapeIsThis(int.MaxValue, int.MaxValue, int.MaxValue).Should().Be(TriangleType.Equilateral);

        }
    }
}
