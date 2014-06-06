using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace CourseraTasks.Test
{
    [TestClass]
    public class SCCCalculatorTest
    {
        [TestMethod]
        public void DepthFirstSeachTest()
        {
            var adjacencyList = new[] { 
                new[] { 3 }, 
                new[] { 7 }, 
                new[] { 5 }, 
                new[] { 6 },
                new[] { 1 }, 
                new[] { 8 }, 
                new[] { 0}, 
                new[] { 4, 5 },
                new[] { 2, 6 }
            };
            IEnumerable<int> vertices1 = SCCCalculator.DepthFirstSeach(adjacencyList, new HashSet<int>(), 0, true);
            vertices1.Should().HaveCount(9);

            IEnumerable<int> vertices2 = SCCCalculator.DepthFirstSeach(adjacencyList, new HashSet<int>(), 1, true);
            vertices2.Should().HaveCount(3);
        }

        [TestMethod]
        public void DepthFirstSeachLoopTest()
        {
            var adjacencyList = new[] { 
                new[] { 3 }, 
                new[] { 7 }, 
                new[] { 5 }, 
                new[] { 6 },
                new[] { 1 }, 
                new[] { 8 }, 
                new[] { 0}, 
                new[] { 4, 5 },
                new[] { 2, 6 }
            };
            var result1 = SCCCalculator.DepthFirstSeachLoop(adjacencyList, Enumerable.Range(0, adjacencyList.Length), true).ToArray();
            result1.Length.Should().BeLessOrEqualTo(3);

            var result2 = SCCCalculator.DepthFirstSeachLoop(adjacencyList, Enumerable.Range(0, adjacencyList.Length).Reverse(), true).ToArray();
            result2.Length.Should().BeLessOrEqualTo(3);
        }

        [TestMethod]
        public void GetSCCsTest()
        {
            var adjacencyList = new[] { 
                new[] { 3 }, 
                new[] { 7 }, 
                new[] { 5 }, 
                new[] { 6 },
                new[] { 1 }, 
                new[] { 8 }, 
                new[] { 0}, 
                new[] { 4, 5 },
                new[] { 2, 6 }
            };
            var components = SCCCalculator.GetSCCs(adjacencyList).ToArray();
            components.Should().HaveCount(3);
            components[0].Should().BeEquivalentTo(new[] { 0, 6, 3 });
            components[1].Should().BeEquivalentTo(new[] { 8, 5, 2 });
            components[2].Should().BeEquivalentTo(new[] { 7, 1, 4 });
        }
    }
}
