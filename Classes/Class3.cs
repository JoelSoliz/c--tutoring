using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Classes
{
    public class Song
    {
        public int Id { get; init; }
        public string Title { get; init; } = "";
        public string Artist { get; init; } = "";
        public string Genre { get; init; } = "";
        public int DurationSeconds { get; init; }
        public int PlayCount { get; init; }
        public DateTime ReleaseDate { get; init; }

        public override string ToString()
        {
            return $"{Title} - {Artist} ({ReleaseDate:dd-MM-yyyy})";
        }
    }

    public class Playlist
    {
        public int Id { get; init; }
        public string Name { get; init; } = "";
        public List<Song> Songs { get; init; } = new();
        public string Owner { get; init; } = "";
    }

    public class Character
    {
        public int Id { get; init; }
        public string Name { get; init; } = "";
        public string Anime { get; init; } = "";
        public int PowerLevel { get; init; }
        public List<string> Tags { get; init; } = new();
    }

    public class Movie
    {
        public int Id { get; init; }
        public string Title { get; init; } = "";
        public string Genre { get; init; } = "";
        public int DurationMinutes { get; init; }
        public decimal Rating { get; init; }
        public int DirectorId { get; init; }
    }

    public class Director
    {
        public int Id { get; init; }
        public string Name { get; init; } = "";
    }

    public class User
    {
        public int Id { get; init; }
        public string Name { get; init; } = "";
        public List<Movie> Watched { get; init; } = new();
    }

    public static class TestData
    {
        public static List<Song> GetSongs()
        {
            return new List<Song>
            {
                // Bad Bunny
                new Song { Id = 1, Title = "Tití Me Preguntó", Artist = "Bad Bunny", Genre = "reggaeton", DurationSeconds = 256, PlayCount = 450_000_000, ReleaseDate = new DateTime(2022, 5, 6) },
                new Song { Id = 2, Title = "Moscow Mule", Artist = "Bad Bunny", Genre = "reggaeton", DurationSeconds = 243, PlayCount = 380_000_000, ReleaseDate = new DateTime(2022, 5, 6) },
                new Song { Id = 3, Title = "Yo Perreo Sola", Artist = "Bad Bunny", Genre = "reggaeton", DurationSeconds = 172, PlayCount = 890_000_000, ReleaseDate = new DateTime(2020, 2, 29) },
                new Song { Id = 4, Title = "Dakiti", Artist = "Bad Bunny", Genre = "reggaeton", DurationSeconds = 205, PlayCount = 1_200_000_000, ReleaseDate = new DateTime(2020, 10, 30) },
                new Song { Id = 5, Title = "Callaita", Artist = "Bad Bunny", Genre = "reggaeton", DurationSeconds = 250, PlayCount = 920_000_000, ReleaseDate = new DateTime(2019, 5, 31) },
                
                // Taylor Swift
                new Song { Id = 6, Title = "Anti-Hero", Artist = "Taylor Swift", Genre = "pop", DurationSeconds = 200, PlayCount = 650_000_000, ReleaseDate = new DateTime(2022, 10, 21) },
                new Song { Id = 7, Title = "Cruel Summer", Artist = "Taylor Swift", Genre = "pop", DurationSeconds = 178, PlayCount = 890_000_000, ReleaseDate = new DateTime(2019, 8, 23) },
                new Song { Id = 8, Title = "Blank Space", Artist = "Taylor Swift", Genre = "pop", DurationSeconds = 231, PlayCount = 2_100_000_000, ReleaseDate = new DateTime(2014, 11, 10) },
                new Song { Id = 9, Title = "Shake It Off", Artist = "Taylor Swift", Genre = "pop", DurationSeconds = 219, PlayCount = 1_800_000_000, ReleaseDate = new DateTime(2014, 8, 18) },
                
                // Otros artistas
                new Song { Id = 10, Title = "Blinding Lights", Artist = "The Weeknd", Genre = "pop", DurationSeconds = 200, PlayCount = 1_500_000_000, ReleaseDate = new DateTime(2019, 11, 29) },
                new Song { Id = 11, Title = "Levitating", Artist = "Dua Lipa", Genre = "pop", DurationSeconds = 203, PlayCount = 1_400_000_000, ReleaseDate = new DateTime(2020, 10, 1) },
                new Song { Id = 12, Title = "Safaera", Artist = "Bad Bunny", Genre = "reggaeton", DurationSeconds = 295, PlayCount = 780_000_000, ReleaseDate = new DateTime(2020, 2, 29) },
            };
        }

        public static List<Playlist> GetPlaylists()
        {
            var songs = GetSongs();
            return new List<Playlist>
            {
                new Playlist
                {
                    Id = 1,
                    Name = "Reggaeton Hits",
                    Owner = "DJ Luian",
                    Songs = songs.Where(s => s.Genre == "reggaeton").Take(3).ToList()
                },
                new Playlist
                {
                    Id = 2,
                    Name = "Swiftie Essentials",
                    Owner = "Taylor Nation",
                    Songs = songs.Where(s => s.Artist == "Taylor Swift").ToList()
                },
                new Playlist
                {
                    Id = 3,
                    Name = "Pop Bangers",
                    Owner = "Spotify",
                    Songs = songs.Where(s => s.Genre == "pop").Take(4).ToList()
                },
            };
        }

        public static List<Character> GetCharacters()
        {
            return new List<Character>
            {
                new Character { Id = 1, Name = "Naruto Uzumaki", Anime = "Naruto", PowerLevel = 9500, Tags = new List<string> { "hero", "ninja", "hokage" } },
                new Character { Id = 2, Name = "Sasuke Uchiha", Anime = "Naruto", PowerLevel = 9300, Tags = new List<string> { "rival", "ninja", "avenger" } },
                new Character { Id = 3, Name = "Sakura Haruno", Anime = "Naruto", PowerLevel = 7800, Tags = new List<string> { "hero", "ninja", "medic" } },
                new Character { Id = 4, Name = "Monkey D. Luffy", Anime = "One Piece", PowerLevel = 10500, Tags = new List<string> { "hero", "pirate", "captain" } },
                new Character { Id = 5, Name = "Roronoa Zoro", Anime = "One Piece", PowerLevel = 9800, Tags = new List<string> { "hero", "pirate", "swordsman" } },
                new Character { Id = 6, Name = "Goku", Anime = "Dragon Ball Z", PowerLevel = 15000, Tags = new List<string> { "hero", "saiyan", "god" } },
                new Character { Id = 7, Name = "Vegeta", Anime = "Dragon Ball Z", PowerLevel = 14500, Tags = new List<string> { "rival", "saiyan", "prince" } },
                new Character { Id = 8, Name = "Eren Yeager", Anime = "Attack on Titan", PowerLevel = 11000, Tags = new List<string> { "hero", "titan", "freedom" } },
            };
        }

        public static List<Director> GetDirectors()
        {
            return new List<Director>
            {
                new Director { Id = 1, Name = "Richard Linklater" },
                new Director { Id = 2, Name = "Greta Gerwig" },
                new Director { Id = 3, Name = "Luca Guadagnino" },
                new Director { Id = 4, Name = "Wong Kar-wai" },
                new Director { Id = 5, Name = "Christopher Nolan" },
            };
        }

        public static List<Movie> GetMovies()
        {
            return new List<Movie>
            {
                new Movie { Id = 1, Title = "Before Sunrise", Genre = "romance", DurationMinutes = 101, Rating = 8.1m, DirectorId = 1 },
                new Movie { Id = 2, Title = "Before Sunset", Genre = "romance", DurationMinutes = 80, Rating = 8.1m, DirectorId = 1 },
                new Movie { Id = 3, Title = "Lady Bird", Genre = "drama", DurationMinutes = 94, Rating = 7.4m, DirectorId = 2 },
                new Movie { Id = 4, Title = "Little Women", Genre = "romance", DurationMinutes = 135, Rating = 7.8m, DirectorId = 2 },
                new Movie { Id = 5, Title = "Call Me by Your Name", Genre = "romance", DurationMinutes = 132, Rating = 7.9m, DirectorId = 3 },
                new Movie { Id = 6, Title = "In the Mood for Love", Genre = "romance", DurationMinutes = 98, Rating = 8.1m, DirectorId = 4 },
                new Movie { Id = 7, Title = "Inception", Genre = "sci-fi", DurationMinutes = 148, Rating = 8.8m, DirectorId = 5 },
            };
        }

        public static List<User> GetUsers()
        {
            var movies = GetMovies();
            return new List<User>
            {
                new User
                {
                    Id = 1,
                    Name = "Ana",
                    Watched = new List<Movie> { movies[0], movies[1], movies[3], movies[4] } // 3 romance
                },
                new User
                {
                    Id = 2,
                    Name = "Carlos",
                    Watched = new List<Movie> { movies[6] } // 0 romance
                },
                new User
                {
                    Id = 3,
                    Name = "María",
                    Watched = new List<Movie> { movies[0], movies[4], movies[5], movies[2] } // 3 romance
                },
            };
        }
    }

    public static class SongExtension
    {
        public static string GetTitleInitials(this Song song)
        {
            var titleWords = song.Title.Split(' ');
            var titleInitials = titleWords.Select(word => word.Length > 0 ? word.Substring(0, 1).ToUpper() : string.Empty);
            return titleInitials.JoinString();
        }

        public static string JoinString(this IEnumerable<string> data)
        {
            var sb = new StringBuilder();
            foreach (var d in data)
            {
                sb.Append(d);
            }

            return sb.ToString();
        }
    }

    // ============================================
    // PROGRAMA PRINCIPAL
    // ============================================

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║  C# Tutoring - Clase 3: LINQ           ║");
            Console.WriteLine("║  Ejercicios Prácticos                  ║");
            Console.WriteLine("╚════════════════════════════════════════╝");

            var genre = Console.ReadLine();
            var songs = TestData.GetSongs();
            var filteredSongs = songs.Where(song => song.Genre == genre && song.ReleaseDate.Year >= 2020)
                                    .OrderBy(song => song.ReleaseDate);
                                    //.Select(song => song.Title);
            foreach (var song in filteredSongs)
            {
                Console.WriteLine(song.GetTitleInitials());
            }

            Console.WriteLine("\n✅ Programa finalizado. Presiona cualquier tecla para salir.");
            Console.ReadKey();
        }
    }
}