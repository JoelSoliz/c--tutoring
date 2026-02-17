using System;

public class TeaQueue
{
    public static void Main(string[] args)
    {
        string testCases = Console.ReadLine();
        int t = int.Parse(testCases);

        for (int i = 0; i < t; i++)
        {
            string studentsNumber = Console.ReadLine();
            int N = int.Parse(studentsNumber);

            int nextAvailableSecond = 0;
            for (int j = 0; j < N; j++)
            {
                string teaMoments = Console.ReadLine();

                string[] parsedTeaMoments = teaMoments.Split(" ");
                int l = int.Parse(parsedTeaMoments[0]);// seg that take tea
                int r = int.Parse(parsedTeaMoments[1]); // waiting Time

                int secondToTakeTea = Math.Max(l, nextAvailableSecond);

                if (secondToTakeTea <= r)
                {
                    Console.Write(secondToTakeTea + " ");
                    nextAvailableSecond = secondToTakeTea + 1;
                }
                else
                {
                    Console.Write("0 ");
                }
            }
            Console.WriteLine();

        }
    }
}