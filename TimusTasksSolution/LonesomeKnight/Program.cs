using System;
using System.IO;

namespace LonesomeKnight
{
	class Program
	{
		private struct PieceMove
		{
			private sbyte horizontal;
			
			private sbyte vertical;
			
			public sbyte Horizontal
			{
				get
				{
					return horizontal;
				}
			}

			public sbyte Vertical
			{
				get
				{
					return vertical;
				}
			}

			public PieceMove(sbyte horizontal, sbyte vertical)
			{
				this.horizontal = horizontal;
				this.vertical = vertical;
			}
		}

		static PieceMove GetKnightMove(int i)
		{
			sbyte offset1;
			sbyte offset2;
			if (i < 4)
			{
				offset1 = 2;
				offset2 = 1;
			}
			else
			{
				i = i % 4;
				offset1 = 1;
				offset2 = 2;
			}

			if (i % 2 == 0)
				offset1 *= (sbyte)-1;
			if (i / 2 % 2 == 0)
				offset2 *= (sbyte)-1;
			return new PieceMove (offset1, offset2);
		}

		private static int GetSquaresUnderKnightAttack(string position)
		{
			int horizontal = position[0] - 'a';
			int vertical = int.Parse(position[1].ToString()) - 1;
			int squaresUnderAttack = 0;
			for (int i = 0; i < 8; i++)
			{
				PieceMove move = GetKnightMove(i);
				int destHorizontal = horizontal + move.Horizontal;
				if (destHorizontal < 0 || destHorizontal > 7)
					continue;
				int destVertical = vertical + move.Vertical;
				if (destVertical < 0 || destVertical > 7)
					continue;
				squaresUnderAttack++;
			}

			return squaresUnderAttack;
		}

		static void Main(string[] args)
		{
			TextReader inputReader =  Console.In;
			string numberString = inputReader.ReadLine();
			int number = int.Parse(numberString);
			for (int i = 0; i < number; i++)
			{
				string position = inputReader.ReadLine();
				int squares = GetSquaresUnderKnightAttack(position);
				Console.WriteLine(squares);
			}
		}
	}
}
