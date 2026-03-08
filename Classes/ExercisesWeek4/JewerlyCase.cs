namespace Classes.ExercisesWeek4
{
    public class JewerlyCase
    {
        public bool IsRotation(int[][] matrix, int N, bool rowsAscending, bool colsAscending)
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N - 1; j++)
                {
                    if (rowsAscending ? matrix[i][j] > matrix[i][j + 1] : matrix[i][j] < matrix[i][j + 1])
                        return false;
                }
            }

            for (int i = 0; i < N - 1; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (colsAscending ? matrix[i][j] > matrix[i + 1][j] : matrix[i][j] < matrix[i + 1][j])
                        return false;
                }
            }
            return true;
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
            if (jewerly.IsRotation(jewerlyCase, N, true, true)) Console.WriteLine(0);
            else if (jewerly.IsRotation(jewerlyCase, N, false, true)) Console.WriteLine(1);
            else if (jewerly.IsRotation(jewerlyCase, N, false, false)) Console.WriteLine(2);
            else Console.WriteLine(3);
        }
    }
}
