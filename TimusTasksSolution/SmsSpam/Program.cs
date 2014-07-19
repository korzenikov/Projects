using System;
using System.IO;
using System.Linq;
using System.Text;

namespace SmsSpam
{
	class Program
	{
		static string[] phoneButtons = new string[] { "abc", "def", "ghi", "jkl", "mno", "pqr", "stu", "vwx", "yz", ".,!", " "};

		private static int GetCountOfTaps(char symbol)
		{
			return phoneButtons.Aggregate(0, (count, item) => count + item.IndexOf(symbol) + 1);
		}

		//static TextReader GetFakeInput()
		//{
		//	StringBuilder sb = new StringBuilder();
		//	sb.AppendLine("pokupaite gvozdi tolko v kompanii gvozdederov i tovarischi!");
		//	StringReader sr = new StringReader(sb.ToString());
		//	return sr;
		//}

		static void Main(string[] args)
		{
			//TextReader inputReader = GetFakeInput();
			TextReader inputReader = Console.In;
			string inputString = inputReader.ReadLine();
			int total = inputString.Aggregate(0, (count, item) => count + GetCountOfTaps(item));
			Console.WriteLine(total);
			//Console.WriteLine("Press ENTER to exit");
			//Console.ReadLine();
		}
	}
}
