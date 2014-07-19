using CourseraTasks.CSharp;
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
    public class UnionFindTest
    {
        [TestMethod]
        public void FindTest()
        {
            int n = 8;
            var unionFind = new UnionFind(n);
            for (int i = 0; i < n; i++)
            {
                unionFind.Find(i).Should().Be(i);
            }

        }
    }
}
