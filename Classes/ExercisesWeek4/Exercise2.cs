using Classes.Class3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        string result = "Success";

        public void AddSongSafe(Song song)
        {
            try
            {
                if (song.Genre != _allowedGenre)
                {
                    throw new InvalidGenreException(song.Genre);
                }

                var songExists = _songs.Any(s => s.Title == song.Title);
                if (songExists)
                {
                    throw new DuplicateSongException(song.Title);
                }

                _songs.Add(song);
            }
            catch (InvalidGenreException ex)
            {
                result = $"Error: {ex.Message}";
            }
            catch (DuplicateSongException ex)
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
        public static void Main2(string[] args)
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
