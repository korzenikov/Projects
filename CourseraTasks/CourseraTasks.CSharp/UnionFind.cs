using System.Collections.Generic;
using System.Linq;

namespace CourseraTasks.CSharp
{
    public class UnionFind
    {
        private readonly int[] _parents;
        private readonly int[] _ranks;

        public UnionFind(int count)
        {
            _parents = Enumerable.Range(0, count).ToArray();
            _ranks = new int[count];
        }

        public int Find(int index)
        {
            var elementsToCompress = new List<int>();
            while (_parents[index] != index)
            {
                elementsToCompress.Add(index);
                index = _parents[index];
            }

            foreach (var element in elementsToCompress.Take(elementsToCompress.Count - 1))
            {
                _parents[element] = index;
            }
            
            return index;
        }
            
        public void Union(int group1, int group2)
        {
            if (_ranks[group1] > _ranks[group2])
            {
                _parents[group2] = group1;
            }
            else
            {
                _parents[group1] = group2;
                if (_ranks[group1] == _ranks[group2])
                {
                    _ranks[group2]++;
                }
            }
        }
    }
}
