using System;
using System.IO;
using System.Text;

namespace Teamwork
{
	class Program
	{
		static void Main(string[] args)
		{
			TextReader inputReader = Console.In;
			string numberString = inputReader.ReadLine();
			string inputString = inputReader.ReadLine();
			string[] tokens = inputString.Split(' ');
			int prevNumber = -1;
			int count = 0;
			foreach (var token in tokens)
			{
				int number = int.Parse(token);
				if (prevNumber == -1 || prevNumber == number)
					count++;
				else
				{
					Console.Write("{0} {1} ", count, prevNumber);
					count = 1;
				}

				prevNumber = number;
			}

			Console.Write("{0} {1} ", count, prevNumber);
		}
	}
}
