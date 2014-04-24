using System;
using System.Linq;
using System.Reflection;
using Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

namespace DependenciesResolver.Test
{
    [TestClass]
    public class DependenciesResolverTest
    {
        [TestMethod]
        public void GetAllClassesTest()
        {
            Assembly entitiesAssembly = typeof(ClassA).Assembly;
            var resolver = new DependenciesResolver(entitiesAssembly);
            var classes = resolver.GetAllClasses().ToArray();

            classes.Length.Should().Be(5);

            classes.Should()
                .Contain(x => x.Type == typeof(ClassA))
                .And.Contain(x => x.Type == typeof(ClassB))
                .And.Contain(x => x.Type == typeof(ClassC))
                .And.Contain(x => x.Type == typeof(ClassD))
                .And.Contain(x => x.Type == typeof(ClassE));
        }

        [TestMethod]
        public void GetClassesFromRootTypeTest()
        {
            Type classB = typeof(ClassB);
            Assembly entitiesAssembly = classB.Assembly;
            var resolver = new DependenciesResolver(entitiesAssembly);
            var classesFromClassB = resolver.GetClassesFromRootType(classB).ToArray();
            classesFromClassB.Length.Should().Be(2);
            classesFromClassB.Should().Contain(x => x.Type == typeof(ClassB)).And.Contain(x => x.Type == typeof(ClassD));

            Type classC = typeof(ClassC);
            var classesFromClassC = resolver.GetClassesFromRootType(classC).ToArray();
            classesFromClassC.Length.Should().Be(3);
            classesFromClassC.Should().Contain(x => x.Type == typeof(ClassC))
                .And.Contain(x => x.Type == typeof(ClassD))
                .And.Contain(x => x.Type == typeof(ClassE));
        }
    }
}
