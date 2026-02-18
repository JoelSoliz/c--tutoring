using System;
namespace Week1Assignments;

public class Vacations
{
    public static void Main (string[] args)
    {
        string input = Console.ReadLine();
        string[] processedInput = input.Split(' ');

        int D = Int32.Parse(processedInput[0]);
        int C = Int32.Parse(processedInput[1]);
        int R = Int32.Parse(processedInput[2]);

        int[] tiringActivities = new int[C] ;

        for (int i = 0; i < tiringActivities.Length; i++) // is not <= because we're taking in to account from 0 position of array
        {
            string tiredActivities = Console.ReadLine();
            int parsedActivities = Int32.Parse(tiredActivities);

            tiringActivities[i] = parsedActivities;
        }

        int totalActivities = CalcualteActivites(tiringActivities, R , D);
        Console.WriteLine(totalActivities);

    }

    static int CalcualteActivites(int[] tiredActivities, int R, int disposition)
    {
        int actualTiredActivity = 0;
        int actualIvigoratingActivity = 0;
        int actualActivitiesDone = 0;

        while (actualTiredActivity < tiredActivities.Length || actualIvigoratingActivity < R)
        {
            if(actualTiredActivity < tiredActivities.Length && tiredActivities[actualTiredActivity] <= disposition)
            {
                actualActivitiesDone++;
                disposition -= tiredActivities[actualTiredActivity];
                actualTiredActivity++;
            } else if (actualIvigoratingActivity < R)
            {
                actualActivitiesDone++;
                string ivigoratingActivities = Console.ReadLine();
                int ivigoratingValues = int.Parse(ivigoratingActivities);
                disposition += ivigoratingValues;
                actualIvigoratingActivity++;

            } else
            {
                break;
            }

        }
        return actualActivitiesDone;
    }
}