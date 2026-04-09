public class Program
{
    public static void Main(string[] args)
    {
        #region B
        /*
        int X = int.Parse(Console.ReadLine());
        int steps = X / 5;
        if (X % 5 != 0)
        {
            steps++;
        }
        Console.WriteLine(steps);
        */
        #endregion

        #region E
        /*
        string path = Console.ReadLine();
        char[] chars = path.ToCharArray();
        var newPath = new StringBuilder("");
        newPath.Append(chars[0]);

        for (int i = 1; i < chars.Length; i++)
        {
            if (chars[i] == '/' && chars[i - 1] == '/')
            {

            }
            else
            {
                newPath.Append(chars[i]);
            }
        }

        if (newPath.Length > 1 && newPath[newPath.Length - 1] == '/')
        {
            newPath.Length--;
        }
        Console.WriteLine(newPath.ToString());
        */
        #endregion

        #region F
        /*
        int N = int.Parse(Console.ReadLine());
        var result = Fibonacci(N + 1);
        Console.WriteLine(result);
        #endregion
        */
        #endregion

        #region J
        /*
        int t = int.Parse(Console.ReadLine());
        for (int i = 0; i < t; i++)
        {
            var parts = Console.ReadLine().Split();
            long n = long.Parse(parts[0]);
            long k = long.Parse(parts[1]);

            long add = n;
            if (n % k != 0)
            {
                add = n + (k - (n % k));
            }

            long result = (long)Math.Ceiling((double)add / n);
            Console.WriteLine(result);
        }
        */
        #endregion

        #region C
        /*
        string[] firstLine = Console.ReadLine().Split(" ");
        int n = int.Parse(firstLine[0]);
        int t = int.Parse(firstLine[1]);

        int[] a = Console.ReadLine().Split().Select(int.Parse).ToArray();

        int left = 0;
        int addition = 0;
        int maxBooks = 0;

        for (int right = 0; right < n; right++)
        {
            addition += a[right];

            while (addition > t)
            {
                addition -= a[left]; //remove book to left if exceeds t
                left++;
            }

            maxBooks = Math.Max(maxBooks, right - left + 1); //3-161 
        }

        Console.WriteLine(maxBooks);
        */
        #endregion

        #region G
        /*
        int N = int.Parse(Console.ReadLine());
        long[] points = Console.ReadLine().Split().Select(long.Parse).ToArray();
        long actual = 1500;
        bool found = false;
        for (int i = 0; i < N; i++)
        {
            actual = actual + points[i];
            if (actual >= 4000)
            {
                found = true;
                Console.WriteLine(i + 1);
                break;
            }
        }
        if (!found)
        {
            Console.WriteLine(-1);
        }
        */
        #endregion

        #region A
        /*
        string[] data = Console.ReadLine().Split(" ");
        int n = int.Parse(data[0]);
        int m = int.Parse(data[1]);

        int[] a = Console.ReadLine().Split().Select(int.Parse).ToArray();
        Array.Sort(a);
        int[] b = Console.ReadLine().Split().Select(int.Parse).ToArray();

        for (int i = 0; i < b.Length; i++)
        {
            int left = 0, right = n;
            while (left < right)
            {
                int mid = (left + right) / 2;
                if (a[mid] <= b[i])
                    left = mid + 1;
                else
                    right = mid;
            }
            Console.Write(left + " ");
        }
        */
        #endregion

        #region D
        /*
        int t = int.Parse(Console.ReadLine());

        for (int i = 0; i < t; i++)
        {
            string[] ndh = Console.ReadLine().Split(" ");
            int n = int.Parse(ndh[0]);
            double d = long.Parse(ndh[1]);
            double h = long.Parse(ndh[2]);

            long[] y = Console.ReadLine().Split().Select(long.Parse).ToArray();

            double totalArea = n * (d * h) / 2;

            for (int j = 0; j < n - 1; j++)
            {
                if (y[j + 1] - y[j] < h)
                {
                    double s = h - (y[j + 1] - y[j]);
                    totalArea -= (d * s * s) / (2 * h);

                }
            }
            Console.WriteLine(totalArea);
        }
        */
        #endregion

        #region I
        /*
        int t = int.Parse(Console.ReadLine());

        for (int i = 0; i < t; i++)
        {
            int n = int.Parse(Console.ReadLine());
            int[] l = Console.ReadLine().Split().Select(int.Parse).ToArray();

            Dictionary<int, int> freq = new Dictionary<int, int>(); //key: value, value: robots
            foreach (int x in l)
                freq[x] = freq.GetValueOrDefault(x, 0) + 1;

            bool invalid = false;

            foreach (int k in freq.Keys)
            {
                if (k == 0) continue; //when there is one robot in a row, or starting a new row
                if (!freq.ContainsKey(k - 1) || freq[k] > freq[k - 1])
                {
                    invalid = true;
                    break;
                }
            }
            Console.WriteLine(invalid ? "NO" : "YES");
        }
        */
        #endregion

        #region H
        int t = int.Parse(Console.ReadLine());

        for (int testCase = 0; testCase < t; testCase++)
        {
            string[] nm = Console.ReadLine().Split();
            int n = int.Parse(nm[0]);
            int m = int.Parse(nm[1]);

            char[][] grid = new char[n][];
            for (int i = 0; i < n; i++)
                grid[i] = Console.ReadLine().ToCharArray();

            string target = "vika";
            int progress = 0; //how many letters of vika are found

            for (int col = 0; col < m && progress < 4; col++)
            {
                char needed = target[progress];// this column has the letter we need now?
                for (int row = 0; row < n; row++)
                {
                    if (grid[row][col] == needed)
                    {
                        progress++;
                        break;
                    }
                }
            }

            Console.WriteLine(progress == 4 ? "YES" : "NO");
        }
        #endregion
    }

    public static int Fibonacci(int n)
    {
        if (n <= 1)
        {
            return n;
        }
        return Fibonacci(n - 1) + Fibonacci(n - 2);
    }
}