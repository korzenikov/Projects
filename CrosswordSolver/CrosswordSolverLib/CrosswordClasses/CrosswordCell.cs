﻿namespace CrosswordSolverLib.CrosswordClasses
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

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

            return Equals((CrosswordCell)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (RowIndex * 397) ^ ColumnIndex;
            }
        }

        protected bool Equals(CrosswordCell other)
        {
            return RowIndex == other.RowIndex && ColumnIndex == other.ColumnIndex;
        }
    }
}
