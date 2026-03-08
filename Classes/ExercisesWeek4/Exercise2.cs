using Classes.Class3;

namespace Classes.ExercisesWeek4
{
    public class PlaylistException : Exception
    {
        public PlaylistException(string message) : base(message) { }
        public PlaylistException(string message, Exception inner) : base(message, inner) { }
    }

    public class InvalidGenreException : PlaylistException
    {
        // Custom properties to carry business-specific data
        public string Genre { get; }
        public InvalidGenreException(string genre) : base($"Not valid song genre: {genre}") { Genre = genre; }
    }

    public class DuplicateSongException : PlaylistException
    {
        public string Title { get; }
        public DuplicateSongException(string title) : base($"The song {title} already exists in the data") { Title = title; }
    }

    public class BadBunnyValidator
    {
        private readonly List<Song> _songs = new List<Song>();
        private int _counter = 0;
        private readonly string _allowedGenre = "reggaeton";

        public void AddSongSafe(Song song)
        {
            string result = "Success";
            try
            {
                if (song.Genre != _allowedGenre)
                {
                    throw new InvalidGenreException(song.Genre);
                }

                var songExists = _songs.Any(s => s.Equals(song));
                if (songExists)
                {
                    throw new DuplicateSongException(song.Title);
                }
                _songs.Add(song);
            }
            catch (PlaylistException ex) //playlist exception
            {
                result = $"Error: {ex.Message}";
            }
            finally
            {
                _counter++;
                Console.WriteLine($"Registered errors: {_counter} : {result}");
            }
        }

        public void AddSongWithInnerException(Song song)
        {
            try
            {
                throw new Exception("Database error!");

            }
            catch (Exception ex)
            {
                throw new PlaylistException("System error: ", ex);
            }
        }
    }
    /*
    public class Program
    {
        public static void Main(string[] args)
        {
            var songs = TestData.GetSongs();
            var badBunnyValidator = new BadBunnyValidator();

            badBunnyValidator.AddSongSafe(songs[0]);
            badBunnyValidator.AddSongSafe(new Song { Id = 2, Title = "Tití Me Preguntó", Artist = "Bad Bunny", Genre = "reggaeton", DurationSeconds = 256, PlayCount = 450_000_000, ReleaseDate = new DateTime(2022, 5, 6) });
            try
            {
                badBunnyValidator.AddSongWithInnerException(songs[1]);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
    */
}
