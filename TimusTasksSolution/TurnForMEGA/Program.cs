using System;
using System.IO;

namespace TurnForMEGA
{
	class Program
	{
		static void Main(string[] args)
		{
			TextReader inputReader = Console.In;
			string inputString = inputReader.ReadLine();
			string[] parts = inputString.Split(' ');
			int k = int.Parse(parts[0]);
			int n = int.Parse(parts[1]);
			string numbersOfCars = inputReader.ReadLine();
			string[] numbersOfCarsParts = numbersOfCars.Split(' ');
			int carsInJam = 0;
			foreach (string numberOfCarsString in numbersOfCarsParts)
			{
				int numberOfCar = int.Parse(numberOfCarsString);
				carsInJam = Math.Max(0, carsInJam + numberOfCar - k);
			}
			Console.WriteLine(carsInJam);
		}
	}
}
