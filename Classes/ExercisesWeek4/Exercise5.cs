using Classes.Class3;

namespace Classes.ExercisesWeek4
{
    public class DataAggregator
    {
        public async Task<List<Song>> GetFromDatabaseAsync()
        {
            await Task.Delay(400);
            var songs = TestData.GetSongs();

            var databaseSongs = songs.Where(song => song.PlayCount >= 450_000_000).ToList();
            return databaseSongs;
        }

        public async Task<List<Song>> GetFromCacheAsync()
        {
            await Task.Delay(50);
            var songs = new List<Song>();
            return songs;
        }

        public async Task<List<Song>> GetFromApiAsync()
        {
            await Task.Delay(800);
            var songs = TestData.GetSongs();
            return songs;
        }

        public async Task<List<Song>> GetFastestAsync()
        {
            List<Task<List<Song>>> tasks = new List<Task<List<Song>>> {
                GetFromDatabaseAsync(),
                GetFromCacheAsync(),
                GetFromApiAsync()
            };

            while (tasks.Count > 0)
            {
                var fastestProcess = await Task.WhenAny(tasks);
                var result = await fastestProcess;
                if (result.Count > 0)
                {
                    return result;
                }
                tasks.Remove(fastestProcess);
            }
            return new List<Song>(); // if all are empty
        }

        public async Task<List<Song>> DelayProcess(int time)
        {
            await Task.Delay(time);
            return new List<Song>();
        }
        public async Task<List<Song>> GetFastestProcessTimeout()
        {
            var delay = DelayProcess(600);
            List<Task<List<Song>>> tasks = new List<Task<List<Song>>> {
                GetFromDatabaseAsync(),
                GetFromCacheAsync(),
                GetFromApiAsync(),
                delay
            };

            while (tasks.Count > 0)
            {
                var fastestProcess = await Task.WhenAny(tasks);
                if (fastestProcess == delay)
                {
                    throw new TimeoutException();
                }
                var dataTask = (Task<List<Song>>)fastestProcess;
                var result = await dataTask;

                if (result.Count > 0) return result;
                tasks.Remove(dataTask);
            }
            return new List<Song>();
        }
    }

    /*
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var dataAggregator = new DataAggregator();
            var result = await dataAggregator.GetFastestAsync();
            foreach (var song in result)
            {
                Console.WriteLine(song);
            }

            try
            {
                var fastestProcess = await dataAggregator.GetFastestProcessTimeout();
                foreach (var song in fastestProcess)
                {
                    Console.WriteLine(song);
                }
            }
            catch (TimeoutException)
            {
                Console.WriteLine("Any process has respond on an adequate time");
            }
        }
    }
    */
}
