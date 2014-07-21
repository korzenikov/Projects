using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Solutions.NEERC_Subregionals
{
    public class BaconsCipher2
    {
        private void Run()
        {
            //TextReader inputReader = Console.In;
            //do
            //{
            //    string input = GenerateRandomString(50);
            //    var numberOfSubstrings = CalculateNumberOfSubstrings(input);
            //    var numberOfSubstrings2 = CalculateNumberOfSubstrings2(input);
            //    if (numberOfSubstrings != numberOfSubstrings2)
            //    {
            //        Console.WriteLine(input);
            //        break;
            //    }
            //} while (true);
            //string input = inputReader.ReadLine();
            string input = GenerateRandomString(5000);
            //string input = "savndmvkqpfrsikyozavdzlltlzpvzkjclgvuyogpxxrxxxyga";

            //string input = "zkjclgvuyogpxxrxxxyga";

            var sw = Stopwatch.StartNew();
            var numberOfSubstrings = CalculateNumberOfSubstrings(input);
            sw.Stop();
            Console.WriteLine("Unoptimized routine:");
            Console.WriteLine(numberOfSubstrings);
            Console.WriteLine("Elapsed milliseconds: {0}", sw.ElapsedMilliseconds);
            Console.WriteLine("Elapsed ticks: {0}", sw.ElapsedTicks);

            sw.Restart();
            var numberOfSubstrings2 = CalculateNumberOfSubstrings2(input);
            sw.Stop();
            Console.WriteLine("Optimized routine:");
            Console.WriteLine(numberOfSubstrings2);
            Console.WriteLine("Elapsed milliseconds: {0}", sw.ElapsedMilliseconds);
            Console.WriteLine("Elapsed ticks: {0}", sw.ElapsedTicks);
        }

        private int  KmpSearch(string s, string word)
        {
            var prefix = BuildPrefixFunction(word);
            int m = 0;
            int i = 0;
            while (m + i < s.Length)
            {
                if (s[m + i] == word[i])
                {
                    i++;
                    if (i == word.Length)
                        return m;
                }
                else
                {
                    m = m + i - prefix[i];
                    if (prefix[i] > -1)
                    {
                        i = prefix[i];
                    }
                    else
                    {
                        i = 0;
                    }
                }
            }
            return -1;
        }

        private int[] BuildPrefixFunction(string s)
        {
            int[] prefix = new int[s.Length];
            prefix[0] = -1;
            prefix[1] = 0;
            int i = 2;
            int cnd = 0;
            while (i < s.Length)
            {
                if (s[i - 1] == s[cnd])
                {
                    cnd++;
                    prefix[i] = cnd;
                    i++;
                }
                else if (cnd > 0)
                {
                    cnd = prefix[cnd];
                }
                else
                {
                    prefix[i] = 0;
                    i++;
                }
            }

            return prefix;
        }

        private string GenerateLongString(int length)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                char symbol = (char)('a' + (i % 26));
                sb.Append(symbol);
            }

            return sb.ToString();
        }

        private string GenerateRandomString(int length)
        {
            var r = new Random();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                char symbol = (char)('a' + r.Next(26));
                sb.Append(symbol);
            }

            return sb.ToString();
        }

        private int CalculateNumberOfSubstrings(string input)
        {
            int length = input.Length;
            int sum = 0;
            for (int i = 0; i < length; i++)
            {
                int maxMatchedSymbols = 0;
                for (int j = 0; j < i; j++)
                {
                    int matchedSymbols = 0;
                    for (int t = 0; t < length - i; t++)
                    {
                        if (input[i + t] != input[j + t])
                            break;
                        matchedSymbols++;
                    }
                    if (matchedSymbols > maxMatchedSymbols)
                    {
                        maxMatchedSymbols = matchedSymbols;
                    }
                    if (matchedSymbols == length - i)
                        break;
                }

                if (maxMatchedSymbols == length - i)
                    break;
                sum += length - i - maxMatchedSymbols;
            }

            return sum;
        }

        private int[] BuildPrefixFunction2(string s, int[] prefix, int start, int length)
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

        private int CalculateNumberOfSubstrings2(string input)
        {
            int length = input.Length;
            int sum = 0;
            int[] prefix = new int[length];
            for (int offset = 0; offset < length; offset++)
            {
                int substrLength = length - offset;
                int maxMatchedSymbols = 0;
                BuildPrefixFunction2(input,  prefix, offset, substrLength);
                int m = 0;
                int i = 0;
                while (m  < offset)
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
                        if (prefix[i] > - 1)
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
    }
}
