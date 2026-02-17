using System;

namespace Week1Assignments;

public class Kathmandu
{
    static void Main(string[] args) 
    { 
        string input = Console.ReadLine();
        string[] processedInput = input.Split(' '); // string array

        int T = Int32.Parse(processedInput[0]); //time needed to rest
        int D = Int32.Parse(processedInput[1]); //flight time
        int M = Int32.Parse(processedInput[2]);

        int[] mealsTime = new int[M];

        for (int i=0; i < mealsTime.Length; i++) { // fill the array
            string timeOfMeals = Console.ReadLine();
            int parsedTimeOfMeals = int.Parse(timeOfMeals);

            mealsTime[i] = parsedTimeOfMeals;

        }
        bool result = determineAdequateFlight(T, D, M, mealsTime);
        Console.WriteLine(result ? "Y" : "N"); //CONDITION ? VALUE IS IT'S TRUE: VALUE IF IT'S FALSE
    }

    // 1 [2] 3 [4] 5 6 [7] 8 9 10
    static bool determineAdequateFlight(int T, int D, int M, int[] mealsTime) {
        if (M == 0)
        {
            return D >= T;
        }
        int firstTimeFood = mealsTime[0];
        int lastTimeFood = D - mealsTime[M - 1];
        if (firstTimeFood >= T) { return true;}

        for (int i = 0; i<mealsTime.Length - 1; i++) {
            int secondTimeFood = mealsTime[i + 1] - mealsTime[i];
            if (secondTimeFood >= T)
            {
                return true;
            }
        }

        if (lastTimeFood >= T)
        {
            return true;
        }
        else { return false;}
    }



}