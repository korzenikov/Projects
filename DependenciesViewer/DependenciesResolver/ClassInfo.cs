using System;
using System.Collections.Generic;

namespace DependenciesResolver
{
    public class ClassInfo
    {
        public ClassInfo(Type type, IEnumerable<Type> referencedTypes)
        {
            Type = type;
            ReferencedTypes = referencedTypes;
        }

        public Type Type { get; private set; }

        public IEnumerable<Type> ReferencedTypes { get; private set; }
    }
}