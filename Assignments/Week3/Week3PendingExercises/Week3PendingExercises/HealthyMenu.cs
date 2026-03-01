using System;
using System.Linq;

public class HealthyMenu
{
    public static void CalculateStudents()
    {
        string input = Console.ReadLine();
        string[] data = input.Split(' ');
        int N = int.Parse(data[0]); // fruit types
        int M = int.Parse(data[1]); // classes number

        int[] maxClassNumber = new int[M];

        for (int i = 0; i < N; i++) //fruits
        {
            string students = Console.ReadLine();
            string[] parsedStudents = students.Split(' ');
            for (int j = 0; j < M; j++)
            {
                int studentsQuantity = int.Parse(parsedStudents[j]);
                if (studentsQuantity > maxClassNumber[j])
                {
                    maxClassNumber[j] = studentsQuantity;
                }
            }
        }

        int result = maxClassNumber.Sum();
        Console.WriteLine(result);
    }
}