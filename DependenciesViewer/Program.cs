using System;
using System.Linq;
using System.Reflection;
using PriceMaster.Entities.ReferenceData;

namespace DependenciesViewer
{
    class Program
    {
        static void Main(string[] args)
        {
            Type rootType = typeof(Security);
            Assembly entitiesAssembly = rootType.Assembly;
            var resolver = new DependenciesResolver.DependenciesResolver(entitiesAssembly);
            var classes = resolver.GetClassesFromRootType(rootType).ToArray();
            //var classes = resolver.GetAllClasses().ToArray();
            foreach (var classInfo in classes)
            {
                Console.WriteLine(classInfo.Type.FullName);
            }

            Console.WriteLine("Total classes count: {0}", classes.Length);
        }
    }
}
