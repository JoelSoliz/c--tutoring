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

        #region J
        int N = int.Parse(Console.ReadLine());
        long[] A = Console.ReadLine().Split(" ").Select(long.Parse).ToArray();

        bool[] current = new bool[200];
        bool[][] currentChoice = new bool[200][]; //if currentChoice[r][k] = true
        for (int residue = 0; residue < 200; residue++) currentChoice[residue] = new bool[N];

        for (int i = 0; i < N; i++)
        {
            int ai = (int)(A[i] % 200); //calculate residue of actual element
            bool[] next = new bool[200]; //copy of current
            bool[][] nextChoice = new bool[200][];
            for (int r = 0; r < 200; r++) nextChoice[r] = (bool[])currentChoice[r].Clone();
            Array.Copy(current, next, 200);

            int ri = ai;
            bool[] solo = new bool[N]; //the sub of one element
            solo[i] = true;

            if (!next[ri]) //someone has ri? the residue
            {
                next[ri] = true;
                nextChoice[ri] = solo; //first group achieveing residue
            }
            else if (!nextChoice[ri].SequenceEqual(solo))
            {
                PrintAnswer(nextChoice[ri], solo, N);
                return;
            }

            for (int residue = 0; residue < 200; residue++)
            {
                if (!current[residue]) continue; //If this residue wasn't reached before, skip it.
                int newResidue = (residue + ai) % 200;
                bool[] newChoice = (bool[])currentChoice[residue].Clone();
                newChoice[i] = true;

                if (!next[newResidue])
                {
                    next[newResidue] = true;
                    nextChoice[newResidue] = newChoice;
                }
                else if (!nextChoice[newResidue].SequenceEqual(newChoice))
                {
                    PrintAnswer(nextChoice[newResidue], newChoice, N);
                    return;
                }
            }

            current = next;
            currentChoice = nextChoice;
        }

        Console.WriteLine("No");
    }

    static void PrintAnswer(bool[] b1, bool[] b2, int N)
    {
        var indB = new List<int>();
        var indC = new List<int>();
        for (int k = 0; k < N; k++)
        {
            if (b1[k]) indB.Add(k + 1);
            if (b2[k]) indC.Add(k + 1);
        }
        Console.WriteLine("Yes");
        Console.WriteLine(indB.Count + " " + string.Join(" ", indB));
        Console.WriteLine(indC.Count + " " + string.Join(" ", indC));
    }
    #endregion
}


