using System;
using System.Collections.Generic;

namespace CourseraTasks.CSharp
{
    public class MinPriorityQuery<T> : PriorityQuery<T>
        where T : IComparable<T>
    {
        public MinPriorityQuery(IEnumerable<T> values) : base(values)
        {
        }

        public MinPriorityQuery()
        {
        }

        public T Min
        {
            get
            {
                return HighestPriorityElement;
            }
        }

        public T ExtractMin()
        {
            return ExtractHighestPriorityElement();
        }

        protected override bool IsHigherPriority(T priority1, T priority2)
        {
            return priority1.CompareTo(priority2) < 0;
        }
    }
}