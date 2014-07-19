using System;
using System.IO;

namespace Solutions.NEERC_Subregionals
{
	class Flags
	{
		private void Run()
		{
			TextReader inputReader = Console.In;
			string inputString = inputReader.ReadLine();
			int n = int.Parse(inputString);
			long mPrevPrev = 2;
			long mPrev = 0;
			long mCurrent = 0;
			for (int i = 0; i < n; i++)
			{
				mCurrent = mPrev + mPrevPrev;
				mPrevPrev = mPrev;
				mPrev = mCurrent;
			}
			
			Console.WriteLine(mCurrent);
		}
	}
}
