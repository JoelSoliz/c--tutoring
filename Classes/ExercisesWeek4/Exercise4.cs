using Classes.Class3;
using System.Collections.Concurrent;

namespace Classes.ExercisesWeek4
{
    public class RankingProcessor
    {
        // Here we have the same time as async becase
        //  here the calculation occurs in main thread
        // Ideal for: Main flow to be executed (such as treat a critical task)
        public List<Song> CalculateRankingSync(List<Song> songs)
        {
            var calculation = songs.OrderByDescending(song => song.PlayCount * 0.6m + song.Rating * 0.4m)
                .Take(10);
            return calculation.ToList();
        }

        // In this case, task.run executes con a SEPARATE THREAD from the main one (no blocking operation)
        // It runs the calculation of top 10 ranking on background
        // That's why we have the same time with async
        // The difference here is the way we manage the thread where calculation occurs
        // Ideal for: when we don't want to block the main flow with heavy tasks
        public async Task<List<Song>> CalculateRankingAsync(List<Song> songs)
        {
            var calculation = await Task.Run(() =>
            {
                var formula = songs.OrderByDescending(song => song.PlayCount * 0.6m + song.Rating * 0.4m)
                .Take(10);
                return formula.ToList();
            });
            return calculation;
        }

        // Behaves slower becase each iteration performs only a smmall ammount
        // of work (it's practically a linear calculation)
        // So we have overhead herem coordeinating cores and chunks is a lot for that 
        // king of light calcuation. Here this parallel implementation can be used when
        // we need to do heave calcuations such as ai processing, audio compression in paralle

        public List<Song> CalculateRankingParallel(List<Song> songs)
        {
            ConcurrentBag<Song> bag = new ConcurrentBag<Song>();
            var parallel = Parallel.ForEach(songs, song =>
            {
                bag.Add(song);
            });

            var formula = bag.OrderByDescending(song => song.PlayCount * 0.6m + song.Rating * 0.4m)
                .Take(10);

            return formula.ToList();

        }
    }

    /*
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Random random = new Random();
            var ranking = new RankingProcessor();
            List<Song> songs = new List<Song>();

            for (int i = 1; i <= 500_000; i++)
            {
                int id = random.Next(1, i);
                int playCount = random.Next(10, 10000);
                decimal rating = random.Next(0, 10);
                string title = $"Song {i}";

                songs.Add(new Song { Id = id, Title = title, PlayCount = playCount, Rating = rating });
            }

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            ranking.CalculateRankingSync(songs);
            stopwatch.Stop();
            Console.WriteLine($"Sync time: {stopwatch.ElapsedMilliseconds}ms");

            stopwatch.Restart();
            await ranking.CalculateRankingAsync(songs);
            stopwatch.Stop();
            Console.WriteLine($"Async time: {stopwatch.ElapsedMilliseconds}ms");

            stopwatch.Restart();
            ranking.CalculateRankingParallel(songs);
            stopwatch.Stop();
            Console.WriteLine($"Parallel time: {stopwatch.ElapsedMilliseconds}ms");
        }
    }
    */
}

