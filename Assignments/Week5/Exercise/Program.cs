public class Program
{
    public static void Main(string[] args)
    {
        #region Jigsaw of Shadows
        /*
        string[] flatlandersInfo = Console.ReadLine().Split(' ');

        int degree = int.Parse(flatlandersInfo[0]);
        int N = int.Parse(flatlandersInfo[1]);

        double radians = degree * (Math.PI / 180.0); // convert to radians
        var intervals = new List<(double start, double end)>(); //touple

        for (int i = 0; i < N; i++)
        {
            string[] data = Console.ReadLine().Split(' ');

            int X = int.Parse(data[0]);
            int H = int.Parse(data[1]);

            intervals.Add((X, X + H / Math.Tan(radians)));
        }

        var orderedIntervals = intervals.OrderBy(interval => interval.start).ToList();
        double currentStart = orderedIntervals[0].start;
        double currentEnd = orderedIntervals[0].end;
        double totalLength = 0;

        for (int i = 1; i < orderedIntervals.Count; i++)
        {
            if (orderedIntervals[i].start <= currentEnd) //it gets overlaped? 50 <= 100
            {
                currentEnd = Math.Max(currentEnd, orderedIntervals[i].end); // the new fusioned interval 0 and 200
            }
            else
            {
                totalLength += currentEnd - currentStart; //300 - 0, the length of interval
                currentStart = orderedIntervals[i].start; // if it doesn't overlaped
                currentEnd = orderedIntervals[i].end;
            }
        }
        totalLength += currentEnd - currentStart; // the pending interval
        Console.WriteLine(totalLength.ToString("F5")); //5 decimals
        */
        #endregion

        #region Collatz Polynomial
        /*
        string degree = Console.ReadLine();
        int N = int.Parse(degree);
        string[] polinomial = Console.ReadLine().Split(' ');
        int polinom = 0;

        //build the binary
        foreach (string coef in polinomial)
        {
            polinom = (polinom << 1) | int.Parse(coef); //using left shift for convert to bytes and adds new bit to right
        }

        int steps = 0;
        while (polinom != 1)
        {
            if (polinom % 2 == 1) //verifies last bit is 1
            {
                polinom = polinom ^ (polinom << 1) ^ 1;
                // p * x = p << 1
                //  ^ polinom adds the original pxx + p = p(x+1)
                //coef = 0 or 1
                // ^ 1 adds 1
                // We can quit the 2 result if we sum 1 
                // 1 0 0 1
                // 1 0 0 1 0
                // XOR = 1 1 0 1 1
                // XOR 1 = 11010
            }
            else // if it's 0
            {
                polinom = polinom >> 1; // divide by x
            }
            steps++;
        }

        Console.WriteLine(steps);
        */
        #endregion

        #region Finding Privacy

        string[] data = Console.ReadLine().Split(' ');
        int K = int.Parse(data[0]);
        int N = int.Parse(data[1]);

        int y = N - 2 * K; //B: X-
        int x = 3 * K - N; //A: X--

        if (K < (N - 1) / 2 || K > (N + 1) / 2)
        {
            Console.WriteLine("*");
            return;
        }

        if (K == (N - 1) / 2) Console.Write("-"); //if it's minimum

        for (int i = 0; i < K; i++)
        {
            bool isLastIteration = (i == K - 1);
            if (y > 0)
            {
                Console.Write(isLastIteration ? "X-" : "X--");
                y--;
            }
            else
            {
                Console.Write(isLastIteration ? "X" : "X-");
                x--;
            }
        }
        Console.WriteLine();

        #endregion

        #region Kool Strings
        /*
        string[] data = Console.ReadLine().Split(" ");
        int K = int.Parse(data[0]);
        string S = data[1];
        char[] characters = S.ToCharArray(); //here we're going to do the flips

        int operations = 0;
        int count = 1;

        for (int i = 1; i < S.Length; i++)
        {
            if (characters[i] == characters[i - 1])
            {
                count++;
            }
            else // if they're different
            {
                count = 1;
            }

            if (count == K)
            {
                if (characters[i] == '1')
                {
                    characters[i] = '0';
                }
                else
                {
                    characters[i] = '1';
                }

                operations++;
                count = 1;
            }
        }
        Console.WriteLine(operations + " " + new string(characters));
        */
        #endregion
    }
}
