using System;
using System.Collections.Generic;

public class MusicalPuzzle 
{
	public static void Main(string[] args) 
	{
		string t = Console.ReadLine(); // test cases number
		int parsedT = int.Parse(t); 

		for (int i = 0; i < parsedT; i++)  
		{
			string n = Console.ReadLine();
			int parsedN = int.Parse(n);

			string s = Console.ReadLine();

			HashSet<string> minimumSounds = new HashSet<string>(); // to store all pairs

			for (int j = 0; j < parsedN - 1; j++) {
				string pair = s.Substring(j, 2); // extract part of our string
				minimumSounds.Add(pair);
			
			}
			Console.WriteLine(minimumSounds.Count());
			minimumSounds.Count();
		}
	}
}