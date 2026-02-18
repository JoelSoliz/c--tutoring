using System;
public class LexicographicalAnalysis
{
    public static void AnalyzeBytes()
    {
        string number = Console.ReadLine();
        int N = int.Parse(number);

        string input = Console.ReadLine();
        string[] bytes = input.Split(' ');

        int[] numbers = new int[bytes.Length];
        int[] bitsQuantity = new int[30];
        int[] result = new int[N];
        for (int i = 0; i < N; i++)
        {
            int parsedBytes = int.Parse(bytes[i]);
            numbers[i] = parsedBytes;
            for (int k = 0; k < 30; k++)
            {
                if ((numbers[i] & (1 << k)) != 0) // the bit is on?
                {

                    bitsQuantity[k]++;
                }
            }



        }

        for (int i = 0; i < N; i++)
        {
            for (int k = 29; k >= 0; k--)
            {
                if (bitsQuantity[k] > 0)
                {
                    result[i] += (1 << k);
                    bitsQuantity[k]--;
                }
            }
        }

        Console.WriteLine(string.Join(" ", result));

    }
}