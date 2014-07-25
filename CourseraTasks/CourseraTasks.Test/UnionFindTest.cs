using System.Linq;

using CourseraTasks.CSharp;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CourseraTasks.Test
{
    [TestClass]
    public class UnionFindTest
    {
        [TestMethod]
        public void UnionTest()
        {
            int n = 8;
            var unionFind = new UnionFind(n);
            for (int i = 0; i < n; i++)
            {
                unionFind.Find(i).Should().Be(i);
            }

            unionFind.Union(0, 1);
            var group1 = unionFind.Find(0);
            unionFind.Find(1).Should().Be(group1);

            unionFind.Union(2, 3);
            var group2 = unionFind.Find(2);
            unionFind.Find(3).Should().Be(group2);

            unionFind.Union(group1, group2);

            var groups = new int[] 
            {
                unionFind.Find(0),
                unionFind.Find(1),
                unionFind.Find(2),
                unionFind.Find(3)
            };

            groups.Distinct().Should().HaveCount(1);

            unionFind.Union(5, 6);
            var largeGroup = unionFind.Find(0);
            var smallGroup = unionFind.Find(5);
            unionFind.Union(largeGroup, smallGroup);
            unionFind.Find(5).Should().Be(largeGroup);
            unionFind.Find(6).Should().Be(largeGroup);
        }
    }
}
