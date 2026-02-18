using System;
namespace Week1Assignments;

public class GameShow {

    public static void Main(string[] args)
    {
        string numberOfBoxes = Console.ReadLine();
        int parsedNumberOfBoxes = int.Parse(numberOfBoxes);
        int sbecs = 100;


        int result = determineLargestBalance(sbecs, parsedNumberOfBoxes);
        Console.WriteLine(result);
    }

    static int determineLargestBalance(int sbecs, int numberOfBoxes) {
        int bestBalance = sbecs;
        int currentBalance = sbecs;
        for (int i = 0; i < numberOfBoxes; i++) {
            string values = Console.ReadLine();
            int boxesNumbers = int.Parse(values);
            currentBalance = currentBalance + boxesNumbers;
            bestBalance = Math.Max(currentBalance, bestBalance);
        }
        return bestBalance;
    }
}