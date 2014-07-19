using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Solutions.NEERC_Subregionals
{
	class AirportAnnouncements
	{
		const string UnknownLanguage = "unknown";
		const string WrongData = "Igor is wrong.";

	    private void Run()
		{
			TextReader inputReader = Console.In;

			bool incorrectData = false;
			string input = inputReader.ReadLine();
			int n = int.Parse(input);
			int[] phrasesLanguages = new int[n];

			ISet<string> languages = new HashSet<string>();
			int lauguageId = 0;
			string currentLanguage = string.Empty;

			for (int i = 0; i < n; i++)
			{
				string language = inputReader.ReadLine();
				if (language != UnknownLanguage)
				{
					if (language != currentLanguage)
					{
						if (!languages.Contains(language))
						{
							lauguageId++;
							languages.Add(language);
							currentLanguage = language;
						}
						else
						{
							incorrectData = true;
							break;
						}
					}
					phrasesLanguages[i] = lauguageId;
				}
			}

			if (incorrectData)
				Console.WriteLine(WrongData);
			else
			{
				List<int> counts = GetPossibleLanguageCounts(phrasesLanguages).ToList();
				string answer;
				if (counts.Count == 0)
					answer = WrongData;
				else
					answer = string.Join(" ", counts);
				Console.WriteLine(answer);
			}
		}

		private IEnumerable<int> GetPossibleLanguageCounts(IList<int> phrasesLanguages)
		{
			int count = phrasesLanguages.Count;
			for (int numberOfLanguages = 1; numberOfLanguages <= count; numberOfLanguages++)
				if (CheckNumberfOfLanguages(phrasesLanguages, numberOfLanguages))
					yield return numberOfLanguages;
		}

		private bool CheckNumberfOfLanguages(IList<int> phrasesLanguages, int numberfOfLanguages)
		{
			int remainder;
			int phrasesPerLanguage = Math.DivRem(phrasesLanguages.Count, numberfOfLanguages, out remainder);
			if (remainder != 0)
				return false;
			int currentLanguage = 1;
			for (int i = 0; i < numberfOfLanguages; i++)
			{
				bool currentLanguageFound = false;
				for (int j = 0; j < phrasesPerLanguage; j++)
				{
					int phraseLanguage = phrasesLanguages[i * phrasesPerLanguage + j];
					if (phraseLanguage != 0)
					{
						currentLanguageFound = true;
						if (phraseLanguage != currentLanguage)
							return false;
					}
				}
				if (currentLanguageFound)
					currentLanguage++;
			}
			return true;
		}
	}
}
