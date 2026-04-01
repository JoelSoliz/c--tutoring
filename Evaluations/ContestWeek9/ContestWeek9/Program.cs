public class Program
{
    public static void Main(string[] args)
    {
        #region B
        /*
        string S = Console.ReadLine();
        char firstCharacter = S[0];
        Console.WriteLine(S.Substring(1) + firstCharacter);
        */
        #endregion

        #region C
        /*
        string sweets = Console.ReadLine();
        int N = int.Parse(sweets);
        int counter = N - 1;
        Console.WriteLine(counter);
        */
        #endregion

        #region F
        /*
        int N = int.Parse(Console.ReadLine());
        double century = Math.Ceiling((double)N / 100);
        Console.WriteLine((int)century);
        */
        #endregion

        #region E
        /*
        string N = Console.ReadLine();
        int lastNumber = N.Length - 1;
        while (lastNumber >= 0 && N[lastNumber] == '0')
        {
            lastNumber--; //drop 0s. moving the index 121
        }

        int leftSide = 0;
        int rightSide = lastNumber; //pointer without ceros

        while (leftSide < rightSide)
        {
            if (N[leftSide] != N[rightSide])
            {
                Console.WriteLine("No");
                return;
            }
            leftSide++; //to the center ->
            rightSide--; //<-
        }
        Console.WriteLine("Yes");
        */
        #endregion

        #region D
        /*
        string[] data = Console.ReadLine().Split(" ");
        int A = int.Parse(data[0]);
        int B = int.Parse(data[1]);
        int C = int.Parse(data[2]);

        int poweredA = A * A;
        int poweredB = B * B;
        int poweredC = C * C;

        if (poweredA + poweredB < poweredC)
        {
            Console.WriteLine("Yes");
        }
        else
        {
            Console.WriteLine("No");
        }
        */
        #endregion

        #region F
        /*
        int N = int.Parse(Console.ReadLine());
        int[] data = Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
        Dictionary<int, int> pairs = new Dictionary<int, int>();

        for (int i = 0; i < N; i++)
        {
            int remainder = data[i] % 200;

            if (pairs.ContainsKey(remainder))
            {
                pairs[remainder] = pairs[remainder] + 1; //increment counter
            }
            else
            {
                pairs[remainder] = 1;
            }
        }
        long counter = 0;
        foreach (var group in pairs)
        {
            long totalPairs = group.Value;
            long pairsInGroup = totalPairs * (totalPairs - 1) / 2;
            counter += pairsInGroup;
        }
        Console.WriteLine(counter);
        */
        #endregion

        #region G
        /*
        int N = int.Parse(Console.ReadLine());
        int[] A = Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
        int[] B = Console.ReadLine().Split(" ").Select(int.Parse).ToArray();

        int lowerBound = A.Max();
        int upperBound = B.Min();

        if (upperBound >= lowerBound)
        {
            int result = upperBound - lowerBound + 1;
            Console.WriteLine(result);
        }
        else
        {
            Console.WriteLine("0");
        }
        */
        #endregion

        #region H
        /*
        string[] data = Console.ReadLine().Split(" ");
        long N = long.Parse(data[0]);
        int K = int.Parse(data[1]);

        for (int i = 0; i < K; i++)
        {
            if (N % 200 == 0)
            {
                N = N / 200;
            }
            else
            {
                string numbers = N.ToString();
                numbers = numbers + "200";
                N = long.Parse(numbers);
            }
        }
        Console.WriteLine(N);
        */
        #endregion

        #region I
        /*
        int N = int.Parse(Console.ReadLine());
        char[] S = Console.ReadLine().ToCharArray();
        int Q = int.Parse(Console.ReadLine());
        bool flipped = false;

        for (int i = 0; i < Q; i++)
        {
            int[] data = Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
            int T = data[0];
            int A = data[1];
            int B = data[2];

            if (T == 2)
            {
                flipped = !flipped; //true
            }
            else if (T == 1)
            {
                if (flipped)
                {
                    if (A <= N)
                    {
                        A = A + N;
                    }
                    else
                    {
                        A = A - N;
                    }

                    if (B <= N)
                    {
                        B = B + N;
                    }
                    else
                    {
                        B = B - N;
                    }
                }
                char temp = S[A - 1];
                S[A - 1] = S[B - 1];
                S[B - 1] = temp;
            }
        }
        if (flipped)
        {
            Console.WriteLine(new string(S, N, N) + new string(S, 0, N));
        }
        else
        {
            Console.WriteLine(new string(S));
        }
        */
        #endregion

        int N = int.Parse(Console.ReadLine());
        long[] A = Console.ReadLine().Split(" ").Select(long.Parse).ToArray();
        Dictionary<int, int> seen = new Dictionary<int, int>();
        seen[0] = 0;
        long sum = 0;
        bool found = false;

        for (int i = 1; i <= N; i++)
        {
            sum += A[i - 1];
            int remainder = (int)(sum % 200);
            if (seen.ContainsKey(remainder))
            {
                int j = seen[remainder];
                if (j == 0 && i == 1) continue;

                Console.WriteLine("Yes");
                if (j == 0)
                {
                    Console.WriteLine("1 1");
                    Console.Write(i - 1);
                    for (int k = 2; k <= i; k++)
                        Console.Write(" " + k);
                    Console.WriteLine();
                }
                else
                {
                    Console.Write(j);
                    for (int k = 1; k <= j; k++)
                        Console.Write(" " + k);
                    Console.WriteLine();
                    Console.Write(i);
                    for (int k = 1; k <= i; k++)
                        Console.Write(" " + k);
                    Console.WriteLine();
                }
                found = true;
                break;
            }
            else
            {
                seen[remainder] = i;
            }
        }

        if (!found)
            Console.WriteLine("No");
    }
}


