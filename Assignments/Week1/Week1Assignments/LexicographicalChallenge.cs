using System;
using System.Collections.Generic;
using System.Text;

namespace Week1Assignments;

public class LexicographicalChallenge
{
    public static void Main(string[] args)
    {
        string input = Console.ReadLine();
        string K = Console.ReadLine();
        int parsedDistance = int.Parse(K);

        List<char>[] groups = new List<char>[parsedDistance];
        int[] groupIndexes = new int[parsedDistance];
        StringBuilder result = new StringBuilder();

        for (int i = 0; i < parsedDistance; i++)
        {
            groups[i] = new List<char>();
        }

        // characters distributed to it's groups
        for (int i = 0; i < input.Length; i++)
        {
            int index = i % parsedDistance;
            groups[index].Add(input[i]);

        }

        for (int i = 0; i < parsedDistance; i++)
        {
            groups[i].Sort();
        }

        for (int i = 0; i < input.Length; i++)
        {
            int index = i % parsedDistance;
            char consecutiveChar = groups[index][groupIndexes[index]];
            result.Append(consecutiveChar);
            groupIndexes[index]++;
        }

        Console.WriteLine(result.ToString());
    }
}