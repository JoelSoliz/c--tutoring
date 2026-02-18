using System;

namespace Week1Assignments;

public class HandlingBlocks 
{
	public static void Main(string[] args)
	{
		string input = Console.ReadLine();
		string[] blocks = input.Split(' ');

		int N = int.Parse(blocks[0]);
		int K = int.Parse(blocks[1]);
		int[] numbers = new int[N];
		int[] colors = new int[N];

		for (int i = 0; i < N; i++)
		{
			string blocksData = Console.ReadLine();
			string[] parsedBlocksData = blocksData.Split(' ');
			int number = int.Parse(parsedBlocksData[0]);
			int color = int.Parse(parsedBlocksData[1]);

			numbers [i] = number;
			colors [i] = color;
		}

		for (int i = 0; i < numbers.Length; i++) {
			if (numbers[i] != i + 1) {

				int currentPosition = i;
				int targetPosition = numbers[i] - 1;

				int currentColor = colors[currentPosition];
				int targetColor = colors[targetPosition];

				if (currentColor != targetColor) {
					Console.WriteLine("N");
					return;

				}
			}
		}
		Console.WriteLine("Y");
	}
}