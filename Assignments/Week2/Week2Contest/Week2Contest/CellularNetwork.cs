using System;

public class CellularNetwork
{
    public static void Main(string[] args)
    {
        string[] input = Console.ReadLine().Split();
        int n = int.Parse(input[0]);
        int m = int.Parse(input[1]);

        string[] citiesInput = Console.ReadLine().Split();
        long[] cities = new long[n];
        for (int i = 0; i < n; i++)
        {
            cities[i] = long.Parse(citiesInput[i]);
        }

        string[] towersInput = Console.ReadLine().Split();
        long[] towers = new long[m];
        for (int i = 0; i < m; i++)
        {
            towers[i] = long.Parse(towersInput[i]);
        }

        long minRadius = 0; //the max distance
        for (int i = 0; i < n; i++)
        {
            long city = cities[i];
            long minDistance = long.MaxValue;

            for (int j = 0; j < m; j++)
            {
                
                long distance = Math.Abs(city - towers[j]); // Math.Abs ensures for positive distance
                if (distance < minDistance)
                {
                    minDistance = distance;
                }
            }

            if (minDistance > minRadius)
            {
                minRadius = minDistance;
            }
        }

        Console.WriteLine(minRadius);
    }
}