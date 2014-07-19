using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PhonesNumbers
{
	class Program
	{
		static string[] digitsToLetters = new string[] { "oqz", "ij", "abc", "def", "gh", "kl", "mn", "prs", "tuv", "wxy" };

		public class Step
		{
			public string Word { get; set; }

			public byte Position { get; set; }

			public Step Previous { get; set; }
		}

		private static bool IsProducesDigit(char symbol, int digit)
		{
			return digitsToLetters[digit].IndexOf(symbol) != -1;
		}

		private static IEnumerable<string> GetWordsForPosition(byte[] phoneNumber, byte position, string[] words)
		{
			foreach (var word in words)
			{
				int length = word.Length;
				if (position + length <= phoneNumber.Length)
				{
					bool match = true;
					for (int j = 0; j < length; j++)
					{
						if (!IsProducesDigit(word[j], phoneNumber[position + j]))
						{
							match = false;
							break;
						}
					}
					if (match)
						yield return word;
				}
			}
		}

		static List<string> GetMnemonic(byte[] phoneNumber, string[] words)
		{
			int phoneNumberLength = phoneNumber.Length;
			Queue<Step> steps = new Queue<Step>();
			steps.Enqueue(new Step { Position = 0 });
			ISet<int> visitedPositions = new HashSet<int>();
			while (steps.Count != 0)
			{
				Step currentStep = steps.Dequeue();
				byte position = currentStep.Position;
				if (position == phoneNumberLength)
				{
					Stack<string> solutionSteps = new Stack<string>();
					while (currentStep.Previous != null)
					{
						solutionSteps.Push(currentStep.Word);
						currentStep = currentStep.Previous;
					}

					return solutionSteps.ToList();
				}
				else
				{
					foreach (var word in GetWordsForPosition(phoneNumber, position, words))
					{
						byte nextPosition = (byte)(position + word.Length);
						if (!visitedPositions.Contains(nextPosition))
						{
							visitedPositions.Add(nextPosition);
							Step nextStep = new Step { Position = nextPosition, Word = word, Previous = currentStep };
							steps.Enqueue(nextStep);
						}
					}
				}
			}

			return null;
		}

		static string GetShortestWordsCombination(TextReader inputReader, int numberOfWords, byte[] phoneNumber)
		{
			string[] words = new string[numberOfWords];
			for (int i = 0; i < numberOfWords; i++)
			{
				var word = inputReader.ReadLine();
				words[i] = word;
			}
			string[] orderedWords = words;
			var usedWordIndices = new Stack<int>();
			List<string> mnemonic = GetMnemonic(phoneNumber, orderedWords);
			if (mnemonic == null)
				return "No solution.";
			return string.Join(" ", mnemonic);
		}

		static byte[] GetPhoneNumber(string phoneNumberString)
		{
			int length = phoneNumberString.Length;
			byte[] phoneNumber = new byte[length];
			for (int i = 0; i < length; i++)
			{
				phoneNumber[i] = byte.Parse(phoneNumberString[i].ToString());
			}
			return phoneNumber;
		}

		static void Main(string[] args)
		{
			TextReader inputReader = Console.In;
			while (true)
			{
				string phoneNumberString = inputReader.ReadLine();
				if (phoneNumberString == "-1")
					break;
				byte[] phoneNumber = GetPhoneNumber(phoneNumberString);
				string numberOfWordsString = inputReader.ReadLine();
				int numberOfWords = int.Parse(numberOfWordsString);
				string wordsCombination = GetShortestWordsCombination(inputReader, numberOfWords, phoneNumber);
				Console.WriteLine(wordsCombination);
			}
		}
	}
}
