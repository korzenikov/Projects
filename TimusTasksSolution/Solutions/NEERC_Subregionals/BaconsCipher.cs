using System;
using System.IO;

namespace Solutions.NEERC_Subregionals
{
    public class BaconsCipher
    {
        private void Run()
        {
            TextReader inputReader = Console.In;
            string input = inputReader.ReadLine();
            var numberOfSubstrings = CalculateNumberOfSubstrings(input);
            Console.WriteLine(numberOfSubstrings);
        }

        private int CalculateNumberOfSubstrings(string input)
        {
            int length = input.Length;
            int sum = 0;
            int[] prefix = new int[length];
            for (int offset = 0; offset < length; offset++)
            {
                int substrLength = length - offset;
                int maxMatchedSymbols = 0;
                BuildPrefixFunction(input, prefix, offset, substrLength);
                int m = 0;
                int i = 0;
                while (m < offset)
                {
                    if (input[m + i] == input[offset + i])
                    {
                        i++;
                        if (i == substrLength)
                        {
                            maxMatchedSymbols = substrLength;
                            break;
                        }
                    }
                    else
                    {
                        if (i > maxMatchedSymbols)
                        {
                            maxMatchedSymbols = i;
                        }
                        m = m + i - prefix[offset + i];
                        if (prefix[i] > -1)
                        {
                            i = prefix[offset + i];
                        }
                        else
                        {
                            i = 0;
                        }
                    }
                }

                if (i > maxMatchedSymbols)
                {
                    maxMatchedSymbols = i;
                }

                if (maxMatchedSymbols == substrLength)
                    break;
                sum += substrLength - maxMatchedSymbols;
            }

            return sum;
        }

        private int[] BuildPrefixFunction(string s, int[] prefix, int start, int length)
        {
            prefix[start + 0] = -1;
            if (length == 1)
            {
                return prefix;
            }

            prefix[start + 1] = 0;
            int i = 2;
            int cnd = 0;
            while (i < length)
            {
                if (s[start + i - 1] == s[start + cnd])
                {
                    cnd++;
                    prefix[start + i] = cnd;
                    i++;
                }
                else if (cnd > 0)
                {
                    cnd = prefix[start + cnd];
                }
                else
                {
                    prefix[start + i] = 0;
                    i++;
                }
            }

            return prefix;
        }
    }
}