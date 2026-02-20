using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class EliminatingBalloons
{
    public static void findMinimumArrows()
    {
        string balloonsNumber = Console.ReadLine();
        int N = int.Parse(balloonsNumber);

        string input = Console.ReadLine();
        string[] balloonsHeight = input.Split(' ');

        Dictionary<int, int> arrowsHeight = new Dictionary<int, int>(); //5:2 (same height)
        int arrows = 0;

        for (int i = 0; i < N; i++) // balloons assign
        {
            int height = int.Parse(balloonsHeight[i]);
            if (arrowsHeight.ContainsKey(height) && arrowsHeight[height] > 0)
            {
                arrowsHeight[height]--; // h -1
            }
            else
            {
                arrows++;
            }

            if (!arrowsHeight.ContainsKey(height - 1))
            {
                arrowsHeight[height - 1] = 0;

            }
            arrowsHeight[height - 1]++; // arrow left in the last position

        }

        Console.WriteLine(arrows);
    }
}
