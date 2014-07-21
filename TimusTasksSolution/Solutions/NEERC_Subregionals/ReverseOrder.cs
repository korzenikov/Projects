using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;

namespace Solutions.NEERC_Subregionals
{
	class ReverseOrder
	{

		static TextReader GetFakeInput()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("This хреновый пример is an example of a simple test. If you did not");
			sb.AppendLine("understand the ciphering algorithm yet, then write the");
			sb.AppendLine("letters of each word in the reverse order. By the way,");
			sb.AppendLine("\"reversing\" the text twice restores the original text.");
			StringReader sr = new StringReader(sb.ToString());
			return sr;
		}

		private static Regex lattinLetterRegex = new Regex("[A-Z]|[a-z]", RegexOptions.Compiled);

		private static bool IsLatinLetter(char c)
		{
			return lattinLetterRegex.IsMatch(c.ToString());
		}

		private static string ReverseSentence(string sentence)
		{
			StringBuilder resultBuilder = new StringBuilder();
			StringBuilder wordBuilder = new StringBuilder();
			bool capturingWord = false;
			foreach (char c in sentence)
			{
				if (IsLatinLetter(c))
				{
					capturingWord = true;
					wordBuilder.Append(c);
				}
				else
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
			}

			if (capturingWord)
			{
				string capturedWord = wordBuilder.ToString();
				resultBuilder.Append(capturedWord.Reverse().ToArray());
			}

			return resultBuilder.ToString();
		}

		private void Run()
		{
			TextReader inputReader = Console.In;
			//TextReader inputReader = GetFakeInput();
			while (true)
			{
				string sentence = inputReader.ReadLine();
				if (sentence == null)
					break;
				string reversedSentence = ReverseSentence(sentence);
				Console.WriteLine(reversedSentence);
			}
			//Console.WriteLine("Press ENTER to exit");
			//Console.ReadLine();
		}
	}
}
