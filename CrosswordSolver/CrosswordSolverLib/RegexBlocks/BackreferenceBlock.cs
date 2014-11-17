using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrosswordSolverLib.RegexBlocks
{
    public class BackreferenceBlock : RegexBlock
    {
        public BackreferenceBlock(int groupIndex)
        {
            GroupIndex = groupIndex;
        }
        
        public int GroupIndex { get; private set; }
     
    }
}
