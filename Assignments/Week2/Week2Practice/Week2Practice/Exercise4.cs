using System;
using System.Collections.Generic;

public static class MusicCalculator
{
	public static int calculateBPMPromedy(List<int> bpmValues) 
	{
		int sum = 0;
		foreach (int bpm in bpmValues)
		{
			sum += bpm;
		}
		int result = sum / bpmValues.Count;
		return result;
	}

}

public class User 
{
	public int Id { get; set; }
	public string Name { get; set; }

	public User(int id, string name)
	{
		Id = id;
		Name = name;
	}
}

public sealed class PremiumUser : User
{
	public bool HasPremiumSubscription { get; set; }

	public PremiumUser(int id, string name) : base(id, name)
	{
	}
}

public partial class Anime : PremiumUser 
{
	public string Title { get; set; }
	public int ChaptersAmount { get; set; }

	public Anime(string title, int chaptersAmount)
	{
		Title = title;
		ChaptersAmount = chaptersAmount;
	}
}

public partial class Anime
{
	public void Watch() 
	{
		Console.WriteLine($"Watching {Title} with {ChaptersAmount} chapters");
	}
}