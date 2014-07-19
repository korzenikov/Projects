using System;
using System.IO;
using System.Text;

namespace Solutions
{
	class Palindrome
	{
		private void Run()
		{
			TextReader inputReader = Console.In;
			string input = inputReader.ReadLine();
			string output = GeneratePalindrome(input);
			Console.WriteLine(output);
		}

		private string GeneratePalindrome(string str)
		{
			int length = str.Length;

			do
			{
				length++;
			}
			while (!IsSymmetric(str, length));
			
			return AppendSymbols(str, length);
		}

		private bool IsSymmetric(string str, int length)
		{
			int t = length / 2;
			while (length - t < str.Length)
			{
				if (str[t - 1] != str[length - t])
					return false;
				t--;
			}

			return true;
		}

		private string AppendSymbols(string str, int length)
		{
			int symbolsToAppend = length - str.Length;
			StringBuilder sb = new StringBuilder(str);
			for (int i = symbolsToAppend - 1; i >= 0; i--)
			{
				sb.Append(str[i]);
			}

			return sb.ToString();
		}
	}
}
