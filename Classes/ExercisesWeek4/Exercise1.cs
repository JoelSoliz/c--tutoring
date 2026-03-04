using Classes.Class3;

namespace Classes.ExercisesWeek4
{
    public class CloudStorage<T> where T : IMedia
    {
        private readonly List<T> _files = new();

        public async Task UploadAsync(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("The item is null:", nameof(item));
            }

            var fileExists = _files.Any(f => f.Id == item.Id);
            if (fileExists)
            {
                throw new InvalidOperationException("The id already exists");
            }
            await Task.Delay((300));
            _files.Add(item);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var file = _files.FirstOrDefault(f => f.Id == id);
            if (file == null)
            {
                throw new InvalidOperationException("The ID doesn't exists");
            }
            return file;
        }

        public async Task DeleteAsync(int id)
        {
            var fileToDelete = await GetByIdAsync(id);
            var fileToRemove = _files.Remove(fileToDelete);
        }

        public static List<T> Merge(List<T> a, List<T> b)
        {
            var combinatedLists = a.Concat(b).DistinctBy(file => file.Id);
            return combinatedLists.ToList();
        }
    }

    /*
    public class Program
    {
        public static async Task Main(string[] args)
        {

            var cloudStorage = new CloudStorage<Song>();
            var songs = TestData.GetSongs();

            await cloudStorage.UploadAsync(songs[0]);
            var selectedSong = await cloudStorage.GetByIdAsync(1);
            Console.WriteLine(selectedSong);

            await cloudStorage.DeleteAsync(1);

            var badBunnySongs = songs.Where(song => song.Artist == "Bad Bunny").ToList();
            var taylorSongs = songs.Where(song => song.Artist == "Taylor Swift").ToList();

            var merge = CloudStorage<Song>.Merge(badBunnySongs, taylorSongs); // cannot be accesed from an instance reference as it's a static method
            foreach (var item in merge)
            {
                Console.WriteLine(item);
            }


        }
    }
    */
}
