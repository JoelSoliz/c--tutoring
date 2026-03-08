using Classes.Class3;

namespace Classes.ExercisesWeek4
{
    public class Streaming
    {
        public int progress = 0;
        public int progressDivision = 3;

        public async Task BufferSongAsync(Song song, CancellationToken cancellationToken)
        {
            for (int i = 1; i <= 3; i++)
            {
                cancellationToken.ThrowIfCancellationRequested();
                await Task.Delay(200, cancellationToken);

                progress = i * 100 / progressDivision;
                Console.WriteLine($"Buffering: {song.Title}....{progress}%");
            }
        }
    }

    /*public class Program
    {
        public static async Task Main(string[] args)
        {
            var cts = new CancellationTokenSource(300);
            var token = cts.Token;
            var taylorStreaming = new Streaming();
            Stopwatch stopwatch = new Stopwatch();
            var songs = TestData.GetSongs();
            var taylorSongs = songs.Where(song => song.Artist == "Taylor Swift").ToList();

            stopwatch.Start();
            var task1 = taylorStreaming.BufferSongAsync(taylorSongs[0], token);
            var task2 = taylorStreaming.BufferSongAsync(taylorSongs[1], token);
            var task3 = taylorStreaming.BufferSongAsync(taylorSongs[2], token);
            var task4 = taylorStreaming.BufferSongAsync(taylorSongs[3], token);
            var tasks = new List<Task> { task1, task2, task3, task4 };
            int counter = 0;

            try
            {
                await Task.WhenAll(tasks);
                stopwatch.Stop();
            }
            catch (OperationCanceledException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                foreach (var task in tasks)
                {
                    if (task.IsCompletedSuccessfully)
                    {
                        counter++;
                    }
                }
                Console.WriteLine(counter.ToString());
            }
            Console.WriteLine($"Time: {stopwatch.ElapsedMilliseconds}ms");
        }
    }
    */
}