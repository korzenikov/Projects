using System;
using System.IO;

namespace Solutions.NEERC_Subregionals
{
    public class Cards
    {
        public void Run()
        {
            TextReader inputReader = Console.In;
            string input = inputReader.ReadLine();
            string[] parts = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int n = int.Parse(parts[0]);
            int m = int.Parse(parts[1]);
            string input2 = inputReader.ReadToEnd();
            string[] parts2 = input2.Split(new char[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            int[] numbers = new int[m];
            for (int i = 0; i < m; i++)
            {
                var number = int.Parse(parts2[i]);
                numbers[i] = number;
            }

            bool isPossibleSequence = CheckSequence(n, numbers);
            Console.WriteLine(isPossibleSequence ? "YES" : "NO");
        }

        private bool CheckSequence(int n, int[] numbers)
        {
            Array.Sort(numbers);
            var length = numbers.Length;
            var leastPossibleCard = -1;
            if (numbers[0] < 0 || numbers[length - 1] > n)
                return false;
            for (int i = 0; i < length; i++)
            {
                var number = numbers[i];
                var card1 = number - 1;
                var card2 = number;
                if (card1 > leastPossibleCard)
                    leastPossibleCard = card1;
                else if (card2 > leastPossibleCard && card2 < n)
                    leastPossibleCard = card2;
                else
                    return false;
            }

            return true;
        }
    }
}