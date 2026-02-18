using System;

namespace Week1Assignment;

public class AmusementPark {

    public static void Main(string[] args) {
        string input = Console.ReadLine();
        string[] parsedInput = input.Split(' ');

        int N = int.Parse(parsedInput[0]);
        int H = int.Parse(parsedInput[1]);

        string minimumHeights = Console.ReadLine();
        string[] parsedMinimumHeights = minimumHeights.Split(' ');

        int[] requiredMinimumHeights = new int[N];

        for (int i = 0; i < parsedMinimumHeights.Length; i++) {

            requiredMinimumHeights[i] = int.Parse(parsedMinimumHeights[i]);
        }

        int result = determineAtractionsToVisit(N, H, requiredMinimumHeights);
        Console.WriteLine(result);
    
    }

    static int determineAtractionsToVisit(int N, int H, int[] minimumHeights)
    {
        int totalActivities = 0;
        for (int i = 0; i < minimumHeights.Length; i++)
        {
            if (H >= minimumHeights[i])
            {
                totalActivities++;
            }
        }

        return totalActivities;
    }


}