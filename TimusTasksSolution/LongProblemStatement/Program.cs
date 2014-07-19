using System;
using System.IO;
using System.Text;

namespace LongProblemStatement
{
	class Program
	{
		//static TextReader GetFakeInput()
		//{
		//	StringBuilder sb = new StringBuilder();
		//	sb.AppendLine("3 5 6");
		//	sb.AppendLine("To");
		//	sb.AppendLine("be");
		//	sb.AppendLine("or");
		//	sb.AppendLine("not");
		//	sb.AppendLine("to");
		//	sb.AppendLine("be");
		//	StringReader sr = new StringReader(sb.ToString());
		//	return sr;
		//}

		static void Main(string[] args)
		{
			//TextReader inputReader = GetFakeInput();
			TextReader inputReader = Console.In;
			string inputString = inputReader.ReadLine();
			string[] tokens = inputString.Split(' ');
			int h = int.Parse(tokens[0]);
			int w = int.Parse(tokens[1]);
			int n = int.Parse(tokens[2]);
			int pages = 1;
			int lines = 0;
			int symbols = 0;
			for (int i = 0; i < n; i++)
			{
				string word = inputReader.ReadLine();
				int wordLength = word.Length;
				if (symbols ==0 ||  symbols + wordLength + 1 > w)
				{
					symbols = wordLength;
					lines++;
					if (lines == h + 1)
					{
						lines = 1;
						pages++;
					}
				}
				else 
				{
					symbols += wordLength + 1;
				}
			}

			Console.WriteLine(pages);
			//Console.WriteLine("Press ENTER to exit");
			//Console.ReadLine();
		}
	}
}
