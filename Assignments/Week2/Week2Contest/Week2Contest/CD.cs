using System;
using System.Collections.Generic;

public class CD 
{
	public static void Main(string[] args) 
	{ 
		while (true) 
		{
			string input = Console.ReadLine();
			string[] parsedInput = input.Split(' ');
			int N = int.Parse(parsedInput[0]); // Jack's CDs
			int M = int.Parse(parsedInput[1]); // Jill's CDs

			if (N == 0 && M == 0)
			{
				break;
			}
			HashSet<int> jackCatalog = new HashSet<int>();
			for (int i = 0; i < N; i++) 
			{
				string catalogNumbersJack = Console.ReadLine();
				int parsedCatalogNumbersJack = int.Parse(catalogNumbersJack);

				jackCatalog.Add(parsedCatalogNumbersJack);
			}

			int counter = 0;
			for (int i = 0; i < M; i++) 
			{ 
				string catalogNumbersJill = Console.ReadLine();
				int parsedCatalogNumbersJill = int.Parse(catalogNumbersJill);
				if (jackCatalog.Contains(parsedCatalogNumbersJill)) // under Jack's catalog is there also in Jill's
				{ 
					counter++;
				}
			
			}

			Console.WriteLine(counter);

		}
	}

}