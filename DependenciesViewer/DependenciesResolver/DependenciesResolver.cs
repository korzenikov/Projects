using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DependenciesResolver
{
    public class DependenciesResolver
    {
        private readonly Assembly _assembly;

        public DependenciesResolver(Assembly assembly)
        {
            _assembly = assembly;
        }

        public IEnumerable<ClassInfo> GetAllClasses()
        {
            return _assembly.GetTypes().Select(type => new ClassInfo(type, GetPropertyTypes(type)));
        }

        public IEnumerable<ClassInfo> GetClassesFromRootType(Type rootType)
        {
            var discoveredTypes = new HashSet<Type>();
            var typesToVisit = new Queue<Type>();
            typesToVisit.Enqueue(rootType);
            discoveredTypes.Add(rootType);
            while (typesToVisit.Count != 0)
            {
                var currentType = typesToVisit.Dequeue();
                var types = GetPropertyTypes(currentType).ToArray();
                foreach (var propertyType in types.Where(propertyType => !discoveredTypes.Contains(propertyType)))
                {
                    typesToVisit.Enqueue(propertyType);
                    discoveredTypes.Add(propertyType);
                }

                yield return new ClassInfo(currentType, types);
            }
        }

        private IEnumerable<Type> GetPropertyTypes(Type entityType)
        {
            var propertyTypes = new List<Type>();

            foreach (var propertyType in entityType.GetProperties().Select(propertyInfo => propertyInfo.PropertyType))
            {
                Type referencedType;
                if (propertyType.IsArray)
                {
                    referencedType = propertyType.GetElementType();
                }
                else if (propertyType.IsGenericType &&
                    typeof(IEnumerable).IsAssignableFrom(propertyType.GetGenericTypeDefinition()))
                {
                    referencedType = propertyType.GenericTypeArguments.First();
                }
                else
                {
                    referencedType = propertyType;
                }

                if (_assembly.DefinedTypes.Contains(referencedType))
                {
                    propertyTypes.Add(referencedType);
                }
            }

            return propertyTypes.Distinct().Where(type => type != entityType);
        }
    }
}
