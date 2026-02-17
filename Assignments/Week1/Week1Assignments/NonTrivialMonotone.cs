using System;

namespace Week1Assignment;

public class NonTrivialMonotone
{
    public static void Main(string[] args)
    {
        string N = Console.ReadLine();
        int numberOfCharacters = int.Parse(N);
        string s = Console.ReadLine();

        int result = determineSequenceNumber(numberOfCharacters, s);
        Console.WriteLine(result);
    }

    static int determineSequenceNumber(int N, string s) {
        int consecutiveAs = 0;
        int validSequences = 0;
        foreach(char actualCharacter in s)
        {
            if (actualCharacter == 'a')
            {
                consecutiveAs++;
            }
            else
            {
                if (consecutiveAs >= 2)
                {
                    validSequences += consecutiveAs;
                }
                consecutiveAs = 0;
            }

        }
        if (consecutiveAs > 0) // if thre are other as in the final
        {
            if (consecutiveAs >= 2)
            {
                validSequences += consecutiveAs;
            }

        }
        return validSequences;
    }
        
}