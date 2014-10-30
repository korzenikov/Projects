using System;
using System.Text;

namespace CrosswordSolverClient
{
    public class TestHexagonCrossword 
    {
        private int[][] _field;
        private int _size;

        public TestHexagonCrossword(int size)
        {
            _size = size;
            int linesCount = 2 * _size + 1;
            _field = new int[linesCount][];
            int symbolsInLine = _size + 1;
            int id = 0;
            for (int i = 0; i < _size + 1; i++)
            {
                _field[i] = new int[symbolsInLine];
                for (int j = 0; j < symbolsInLine; j++)
                {
                    _field[i][j] = id++;
                }

                symbolsInLine++;
            }

            symbolsInLine -= 2;

            for (int i = _size + 1; i < linesCount; i++)
            {
                _field[i] = new int[symbolsInLine];
                for (int j = 0; j < symbolsInLine; j++)
                {
                    _field[i][j] = id++;
                }

                symbolsInLine--;
            }
        }
        
        public void Print()
        {
            int indentSize = 4;
            string indent = new string(' ', indentSize / 2);

            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size - i; j++)
                {
                    Console.Write(indent);
                }

                foreach (var c in _field[i])
                {
                    Console.Write("{0,4} ", c);
                }
                Console.WriteLine();
            }

            int lineNumber = _size;
            
            for (int i = 0; i < _size + 1; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    Console.Write(indent);
                }

                foreach (var c in _field[lineNumber + i])
                {
                    Console.Write("{0,4} ", c);
                }
                Console.WriteLine();
            }
        }


        public string GetTopBottomLine(int number)
        {
            StringBuilder sb = new StringBuilder();
            int lineLength;
            int i;
            if (number <= _size)
            {
                lineLength = _size + number + 1;
                i = 0;
            }
            else
            {
                lineLength = 3 * _size - number + 1;
                i = number - _size;
            }
            int j = number;
            for (int t = 0; t < lineLength; t++)
            {
                if (i > 6)
                    j--;
                sb.Append(_field[i][j]);
                i++;
                sb.Append(" ");
            }

            return sb.ToString();
        }

        public string GetBottomTopLine(int number)
        {
            StringBuilder sb = new StringBuilder();
            int lineLength;
            int i;
            if (number <= _size)
            {
                i = 2 * _size;
                lineLength = _size + number + 1;
            }
            else
            {
                lineLength = 3 *_size - number + 1;
                i = 3 * _size - number;
            }
            int j = number;
            for (int t = 0; t < lineLength; t++)
            {
                if (i < 6)
                    j--;
                sb.Append(_field[i][j]);
                i--;
                sb.Append(" ");
            }

            return sb.ToString();

        }

        public string GetLeftRightLine(int number)
        {
            StringBuilder sb = new StringBuilder();
            int lineLength;
            int i = number;
            int j = 0;
            if (number <= _size)
            {
                lineLength = _size + number + 1;
            }
            else
            {
                lineLength = 3 * _size - number + 1;
            }

            for (int t = 0; t < lineLength; t++)
            {
                sb.Append(_field[i][j]);
                j++;
                sb.Append(" ");
            }

            return sb.ToString();
        }

        public int GetLeftRightQuestionNumberForCell(int i, int j)
        {
            return i;
        }

        public int GetTopBottomQuestionNumberForCell(int i, int j)
        {
            return j;
        }

        public int GetBottomTopQuestionNumberForCell(int i, int j)
        {
            if (i < _size)
                return j + _size - i;
            return j;
        }
    }
}
