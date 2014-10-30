using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrosswordSolverLib.CrosswordClasses
{
    public class CrosswordCell
    {
        public CrosswordCell(int rowIndex, int columnIndex)
        {
            RowIndex = rowIndex;
            ColumnIndex = columnIndex;
        }

        public int RowIndex { get; private set; }

        public int ColumnIndex { get; private set; }
    }
}
