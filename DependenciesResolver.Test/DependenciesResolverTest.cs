using System;
using System.Linq;
using System.Reflection;
using Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DependenciesResolver.Test
{
    [TestClass]
    public class DependenciesResolverTest
    {
        [TestMethod]
        public void GetAllClassesTest()
        {
            Assembly entitiesAssembly = typeof (ClassA).Assembly;
            var resolver = new DependenciesResolver(entitiesAssembly);
            var classes = resolver.GetAllClasses().ToArray();
            Assert.AreEqual(5, classes.Length);

        }

        [TestMethod]
        public void GetClassesFromRootTypeTest()
        {
            Type classA = typeof(ClassA);
            Assembly entitiesAssembly = classA.Assembly;
            var resolver = new DependenciesResolver(entitiesAssembly);
            var classes = resolver.GetClassesFromRootType(classA).ToArray();
            Assert.AreEqual(5, classes.Length);
        }
    }
}
