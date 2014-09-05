using System;
using System.Linq;
using System.Text;
using RedPill.Contract;

namespace RedPill.Implementation
{
    public class RedPillService : IRedPill
    {
        public ContactDetails WhoAreYou()
        {
            return new ContactDetails
            {
                GivenName = "Andrei",
                FamilyName = "Korzenikov",
                EmailAddress = "korzenikoval@gmail.com",
                PhoneNumber = "+79214176320"
            };
        }

        public long FibonacciNumber(long n)
        {
            if (n > 92)
                throw new ArgumentOutOfRangeException("n", "Fib(>92) will cause a 64-bit integer overflow.");
            long prevPrev = 0;
            long prev = 1;
            if (n == 0)
            {
                return prevPrev;
            }
            if (n == 1)
            {
                return prev;
            }

            long current = 0;
            if (n > 0)
            {
                for (long i = 0; i < n - 1; i++)
                {
                    current = prev + prevPrev;
                    prevPrev = prev;
                    prev = current;
                }
            }
            else
            {
                for (long i = 0; i < -n; i++)
                {
                    current = prev - prevPrev;
                    prev = prevPrev;
                    prevPrev = current;
                }
            }

            return current;
        }

        public TriangleType WhatShapeIsThis(int a, int b, int c)
        {
            if (a >= b + c || b >= a + c || c >= a + b)
            {
                return TriangleType.Error;
            }


            if (a == b && b == c)
            {
                return TriangleType.Equilateral;
            }

            if (a == b || b == c || a == c)
            {
                return TriangleType.Isosceles;
            }

            return TriangleType.Scalene;
        }

        public string ReverseWords(string s)
        {
            if (s == null) 
                throw new ArgumentNullException("s");
            var resultBuilder = new StringBuilder();
            var wordBuilder = new StringBuilder();
            bool capturingWord = false;
            foreach (char c in s)
            {
                if (char.IsWhiteSpace(c))
                {
                    if (capturingWord)
                    {
                        string capturedWord = wordBuilder.ToString();
                        resultBuilder.Append(capturedWord.Reverse().ToArray());
                        wordBuilder.Clear();
                        capturingWord = false;
                    }
                    resultBuilder.Append(c);
                }
                else
                {
                    capturingWord = true;
                    wordBuilder.Append(c);
                }
            }

            if (capturingWord)
            {
                string capturedWord = wordBuilder.ToString();
                resultBuilder.Append(capturedWord.Reverse().ToArray());
            }

            return resultBuilder.ToString();
        }
    }
}
