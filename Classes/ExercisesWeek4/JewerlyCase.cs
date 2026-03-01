namespace Classes.ExercisesWeek4
{
    public class JewerlyCase
    {
        public bool IsOrdered(int[][] jewerlyBoxes)
        {
            for (int i = 0; i < jewerlyBoxes.Length - 1; i++)
            {
                for (int j = 0; j < jewerlyBoxes.Length - 1; j++)
                {
                    if (jewerlyBoxes[i][j] > jewerlyBoxes[i][j + 1]) //rows
                    {
                        return false;
                    }
                    if (jewerlyBoxes[i][j] > jewerlyBoxes[i + 1][j]) //columns
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public int[][] RotateClockwise(int[][] jewerlyBoxes, int N)
        {
            int[][] result = new int[N][];
            // prepares empty matrix
            for (int i = 0; i < jewerlyBoxes.Length; i++)
            {
                result[i] = new int[N];
            }

            //fills all the values
            for (int i = 0; i < jewerlyBoxes.Length; i++)
            {
                for (int j = 0; j < jewerlyBoxes.Length; j++)
                {
                    result[N - 1 - j][i] = jewerlyBoxes[i][j]; //it represents the rotation: [0][0] [3-1-0][0] = [2][0]
                    // j = row, i = column 
                }
            }
            return result;
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            string jewerlySize = Console.ReadLine();
            int N = int.Parse(jewerlySize);
            int[][] jewerlyCase = new int[N][];
            JewerlyCase jewerly = new JewerlyCase();

            for (int i = 0; i < N; i++)
            {
                string data = Console.ReadLine();
                string[] boxes = data.Split(" ");

                jewerlyCase[i] = boxes.Select(int.Parse).ToArray();
            }

            var current = jewerlyCase;
            for (int i = 0; i <= 3; i++)
            {
                if (jewerly.IsOrdered(current))
                {
                    Console.WriteLine(i);
                    break;
                }
                current = jewerly.RotateClockwise(current, N);
            }
        }
    }
}
