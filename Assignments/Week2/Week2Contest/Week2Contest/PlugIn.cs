using System;
using System.Collections.Generic;

public class PlugIn
{
    public static void Main(string[] args)
    {
        string input = Console.ReadLine();
        Stack<char> repeatedPairs = new Stack<char>();

        for (int i = 0; i < input.Length; i++)
        {
            if (repeatedPairs.Count == 0)
            {
                repeatedPairs.Push(input[i]);
            }
            else if (repeatedPairs.Peek() == input[i])
            {
                repeatedPairs.Pop();
            }
            else
            {
                repeatedPairs.Push(input[i]);
            }
        }

        char[] remainingLetters = repeatedPairs.ToArray();
        Array.Reverse(remainingLetters);
        string output = new string(remainingLetters);
        Console.WriteLine(output);
    }
}