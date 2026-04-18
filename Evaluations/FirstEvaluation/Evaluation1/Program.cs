public class Program
{
    public static void Main(string[] args)
    {
        #region A
        /*
        string[] tradeData = Console.ReadLine().Split(' ');
        int c = int.Parse(tradeData[0]); //number of BAPC coind in OldMacDona;d's cup
        int n = int.Parse(tradeData[1]); // the money that we have
        int coinsToBet = 0;

        if (n < c)
        {
            Console.WriteLine(coinsToBet);
        }
        else if (c < n)
        {
            int newAmount = c + 1;
            coinsToBet = newAmount;
            Console.WriteLine(coinsToBet);
        }
        else
        {
            coinsToBet = c;
            Console.WriteLine(coinsToBet);
        }
        */
        #endregion

        #region B
        /*
        string wordToCheck = Console.ReadLine();
        var frequency = wordToCheck
                        .ToLower()
                        .GroupBy(character => character)
                        .Count(group => group.Count() % 2 != 0); //determine odd
        if (frequency <= 1)
        {
            Console.WriteLine("yes");
        }
        else
        {
            Console.WriteLine("no");
        }
        */
        #endregion

        #region C
        /*
        string[] seatsInfo = Console.ReadLine().Split(' ');
        int n = int.Parse(seatsInfo[0]);  //10

        string r1_c1 = seatsInfo[1]; //B6
        char r1 = r1_c1[0]; // B
        int r1Value = (int)r1 - (int)'A' + 1; // A is 65 in ASCII so a - a +1 = 0 +1 =1
        int c1 = int.Parse(r1_c1.Substring(1));

        string r2_c2 = seatsInfo[2]; //D3
        char r2 = r2_c2[0]; //D
        int r2Value = (int)r2 - (int)'A' + 1;
        int c2 = int.Parse(r2_c2.Substring(1)); //3

        int rowDifference = Math.Abs(r1Value - r2Value);

        int leftFormula = c1 + rowDifference + c2;
        int rightFormula = (n + 1 - c1) + rowDifference + (n + 1 - c2);

        int minMoves = Math.Min(leftFormula, rightFormula);

        if (r1Value == r2Value)
        {
            int sameRowFormula = Math.Abs(c1 - c2);
            minMoves = Math.Min(minMoves, sameRowFormula);
        }

        Console.WriteLine(minMoves);
        */
        #endregion

        #region D
        /*
        string numberOfPeople = Console.ReadLine();
        int N = int.Parse(numberOfPeople);
        int[,] secretList = new int[N, N];
        int totalUniqueCouples = 0;

        for (int i = 0; i < N; i++)
        {
            string[] data = Console.ReadLine().Split(' ');
            for (int j = 0; j < N; j++)
            {
                secretList[i, j] = int.Parse(data[j]);
            }
        }

        for (int i = 0; i < N; ++i) //i = 0, j = 1, i = 1, j =2, etc
        {
            for (int j = i + 1; j < N; j++)
            {
                if (secretList[i, j] == 1 && secretList[j, i] == 1)
                {
                    totalUniqueCouples++;
                }
            }
        }
        Console.WriteLine(totalUniqueCouples);
        */
        #endregion D

        #region E
        /*
        string permutationSize = Console.ReadLine();
        int N = int.Parse(permutationSize);

        int result = N / 2;
        Console.WriteLine(result);
        */
        #endregion

        #region F
        /*
        string[] dollsData = Console.ReadLine().Split(' ');
        int S = int.Parse(dollsData[0]);
        int X = int.Parse(dollsData[1]);
        int counter = 0;

        while (S >= 1)
        {
            S = S / X;
            counter++;
        }
        Console.WriteLine(counter);
        */
        #endregion

        #region G
        /*
        string[] data = Console.ReadLine().Split(" ");
        int n = int.Parse(data[0]); //applicants
        int m = int.Parse(data[1]); //free departments
        int k = int.Parse(data[2]); // difference

        int[] desirableSizes = Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
        int[] availableSizes = Console.ReadLine().Split(" ").Select(int.Parse).ToArray();

        Array.Sort(desirableSizes);
        Array.Sort(availableSizes);

        int i = 0;
        int j = 0;
        int counter = 0;

        while (i < n && j < m)
        {
            if (availableSizes[j] < desirableSizes[i] - k) //30 < 40
            {
                j++; // cointune iterating departments
            }
            else if (availableSizes[j] > desirableSizes[i] + k) //60 > [40, 50]
            {
                i++;
            }
            else
            {
                i++; j++; counter++;
            }
        }
        Console.WriteLine(counter);
        */

        #endregion

        #region H
        /*
        string[] data = Console.ReadLine().Split(" ");
        int n = int.Parse(data[0]); //children
        int x = int.Parse(data[1]); // maximum weight

        int[] weights = Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
        Array.Sort(weights);

        int left = 0; //most light
        int right = n - 1; // most heavy
        int gondolas = 0;

        while (left <= right)
        {
            if (weights[left] + weights[right] <= x)
            {
                left++; right--; gondolas++;//we don't need the most heavy anymore
            }
            else
            {
                right--; gondolas++;
            }
        }
        Console.WriteLine(gondolas);
        */
        #endregion

        #region I
        /*
        int N = int.Parse(Console.ReadLine());
        int[] numbers = Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
        var distinct = new HashSet<int>(numbers);
        Console.WriteLine(distinct.Count);
        */
        #endregion

        int n = int.Parse(Console.ReadLine());
        long[] numbers = Console.ReadLine().Split(" ").Select(long.Parse).ToArray();
        long currentSum = numbers[0];
        long maxSum = numbers[0];
        for (int i = 1; i < n; i++)
        {
            currentSum = Math.Max(numbers[i], currentSum + numbers[i]);
            maxSum = Math.Max(maxSum, currentSum);
        }
        Console.WriteLine(maxSum);

    }
}