using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseraTasks.CSharp
{
    public class UnionFind
    {
        private readonly int[] _parents;
        private readonly int[] _ranks;

        public UnionFind(int count)
        {
            _parents = new int[count];
            _ranks = new int[count];
        }

        public int Find(int index)
        {
            return 0;
        }

        public void Union(int group1, int group2)
        {
        }
    }
}
