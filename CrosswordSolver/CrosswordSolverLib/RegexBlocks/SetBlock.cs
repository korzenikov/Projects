using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrosswordSolverLib.RegexBlocks
{
    public abstract class SetBlock : RegexBlock
    {
        public SetBlock(string characters)
        {
            Characters = characters;
        }

        public string Characters { get; private set; }
    }
}
