namespace CrosswordSolverLib
{
    public class MatrixHelper
    {
        public static string GetHorizontalLine(char[,] matrix, int rowIndex)
        {
            int size = matrix.GetLength(1);
            var line = new char[size];
            for (int j = 0; j < size; j++)
                line[j] = matrix[rowIndex, j];
            return new string(line);
        }

        public static string GetVerticalLine(char[,] matrix, int columnIndex)
        {
            int size = matrix.GetLength(0);
            var line = new char[size];
            for (int i = 0; i < size; i++)
                line[i] = matrix[i, columnIndex];
            return new string(line);
        }
    }
}
