using CourseraTasks.CSharp;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CourseraTasks.Test
{
    [TestClass]
    public class TspTest
    {
        [TestMethod]
        public void GetShortestRouteLengthTest()
        {
            var distances = new double[,]
                {
                    { 0, 2, 1, 4 }, 
                    { 2, 0, 3, 5 }, 
                    { 1, 3, 0, 6 }, 
                    { 4, 5, 6, 0 } 
                };

            var tsp = new Tsp(4, distances);

            var length = tsp.GetShortestRouteLength();
            length.Should().Be(13);
        }


        [TestMethod]
        public void GetShortestRouteLengthTest2()
        {
            var distances = new double[,]
                {
                    { 0, 20, 42, 35 }, 
                    { 20, 0, 30, 34 }, 
                    { 42, 30, 0, 12 }, 
                    { 35, 34, 12, 0 } 
                };

            var tsp = new Tsp(4, distances);

            var length = tsp.GetShortestRouteLength();
            length.Should().Be(97);
        }
    }
}
