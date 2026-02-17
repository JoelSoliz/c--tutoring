using System;
namespace Week1Assignments;

public class BattleShip 
{
    public static void Main(string[] args) 
    { 
        string numberOfShips = Console.ReadLine();
        int N = int.Parse(numberOfShips);
        bool[,] board = new bool[10, 10];

        for (int i = 0; i < N; i++) {
            string shipPositions = Console.ReadLine();
            string[] parsedShipPositions = shipPositions.Split(' ');
            int D = int.Parse(parsedShipPositions[0]);
            int L = int.Parse(parsedShipPositions[1]);
            int R = int.Parse(parsedShipPositions[2]);
            int C = int.Parse(parsedShipPositions[3]);

            // Horizontal Case: C -> C + L -1
            if (D == 0)
            {
                if (C + L - 1 > 10)
                {
                    Console.WriteLine("N");
                    return;
                }
                for (int j = 0; j < L; j++) //Length
                {
                    int rows = R - 1;
                    int columns = C - 1 + j; // need to move column by column
                    if (board[rows, columns])
                    {
                        Console.WriteLine("N");
                        return;
                    }
                    board[rows, columns] = true;

                }

            }
            else {
                if (R + L - 1 > 10)
                {
                    Console.WriteLine("N");
                    return;
                }
                for (int j = 0; j < L; j++)
                {
                    int rows = R - 1 + j; // need to move row by row
                    int columns = C - 1;
                    if (board[rows, columns])
                    {
                        Console.WriteLine("N");
                        return;
                    }
                    board[rows, columns] = true;
                }
            }
        }
        Console.WriteLine("Y");
    }
}