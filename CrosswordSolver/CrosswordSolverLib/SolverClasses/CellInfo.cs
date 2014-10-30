using System.Collections.Generic;

using CrosswordSolverLib.CrosswordClasses;

namespace CrosswordSolverLib.SolverClasses
{
    public class CellInfo
    {
        private readonly CrosswordCell _cell;

        public CellInfo(CrosswordCell cell)
        {
            _cell = cell;
        }

        public CrosswordCell Cell
        {
            get
            {
                return _cell;
            }
        }

        public List<char> AvailableCharacters { get; set; }
    }
}
