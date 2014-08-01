using CourseraTasks.CSharp;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CourseraTasks.Test
{
    [TestClass]
    public class HeldKarpTest
    {
        [TestMethod]
        public void GetShortestRouteLengthTest()
        {
            var distances = new float[,]
                {
                    { 0, 2, 1, 4 }, 
                    { 2, 0, 3, 5 }, 
                    { 1, 3, 0, 6 }, 
                    { 4, 5, 6, 0 } 
                };

            var heldKarp = new HeldKarp(4, distances);

            var length = heldKarp.GetShortestRouteLength();

            length.Should().Be(13);
        }

        [TestMethod]
        public void GetShortestRouteLengthTest2()
        {
            var distances = new float[,]
                {
                    { 0, 20, 42, 35 }, 
                    { 20, 0, 30, 34 }, 
                    { 42, 30, 0, 12 }, 
                    { 35, 34, 12, 0 } 
                };

            var heldKarp = new HeldKarp(4, distances);
            
            var length = heldKarp.GetShortestRouteLength();

            length.Should().Be(97);
        }


        [TestMethod]
        public void GetShortestRouteLengthTest3()
        {
            var distances = new float[,]
                {
                    { 0, 20, 20 }, 
                    { 20, 0, 20 },
                    { 20, 20, 0 }
                };

            var heldKarp = new HeldKarp(3, distances);

            var length = heldKarp.GetShortestRouteLength();

            length.Should().Be(60);
        }
    }
}
