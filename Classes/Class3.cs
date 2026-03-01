using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Classes.Class3
{
    public interface IMedia
    {
        int Id { get; }
        string Title { get; }
    }

    public class Song : IMedia
    {
        public int Id { get; init; }
        public string Title { get; init; } = "";
        public string Artist { get; init; } = "";
        public string Genre { get; init; } = "";
        public int DurationSeconds { get; init; }
        public int PlayCount { get; init; }
        public DateTime ReleaseDate { get; init; }

        public decimal Rating { get; init; }

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

    public class Character : IMedia
    {
        public int Id { get; init; }
        public string Title { get; init; } = "";
        public string Anime { get; init; } = "";
        public int PowerLevel { get; init; }
        public List<string> Tags { get; init; } = new();
    }

    public class Movie : IMedia
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
                new Character { Id = 1, Title = "Naruto Uzumaki", Anime = "Naruto", PowerLevel = 9500, Tags = new List<string> { "hero", "ninja", "hokage" } },
                new Character { Id = 2, Title = "Sasuke Uchiha", Anime = "Naruto", PowerLevel = 9300, Tags = new List<string> { "rival", "ninja", "avenger" } },
                new Character { Id = 3, Title = "Sakura Haruno", Anime = "Naruto", PowerLevel = 7800, Tags = new List<string> { "hero", "ninja", "medic" } },
                new Character { Id = 4, Title = "Monkey D. Luffy", Anime = "One Piece", PowerLevel = 10500, Tags = new List<string> { "hero", "pirate", "captain" } },
                new Character { Id = 5, Title = "Roronoa Zoro", Anime = "One Piece", PowerLevel = 9800, Tags = new List<string> { "hero", "pirate", "swordsman" } },
                new Character { Id = 6, Title = "Goku", Anime = "Dragon Ball Z", PowerLevel = 15000, Tags = new List<string> { "hero", "saiyan", "god" } },
                new Character { Id = 7, Title = "Vegeta", Anime = "Dragon Ball Z", PowerLevel = 14500, Tags = new List<string> { "rival", "saiyan", "prince" } },
                new Character { Id = 8, Title = "Eren Yeager", Anime = "Attack on Titan", PowerLevel = 11000, Tags = new List<string> { "hero", "titan", "freedom" } },
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

        // EXERCISE 8
        public static string ToLabel(this Song song)
        {
            return $"{song.Title} - {song.Artist}";
        }

        public static IEnumerable<Song> TopByPlays(this IEnumerable<Song> song, int n)
        {
            var topSongs = song.OrderByDescending(s => s.PlayCount).Take(n);
            return topSongs;
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

    class Class3
    {
        static List<Song> FilterSongs(IEnumerable<Song> songs, Func<Song, bool> rule)
        {
            var result = songs.Where(song => rule(song)).ToList();
            return result;
        }

        static void Main(string[] args)
        {
        //    Console.WriteLine("╔════════════════════════════════════════╗");
        //    Console.WriteLine("║  C# Tutoring - Clase 3: LINQ           ║");
        //    Console.WriteLine("║  Ejercicios Prácticos                  ║");
        //    Console.WriteLine("╚════════════════════════════════════════╝");

            // var genre = Console.ReadLine();
            var songs = TestData.GetSongs();
            var playlists = TestData.GetPlaylists();
            var movies = TestData.GetMovies();
            var directors = TestData.GetDirectors();
            var users = TestData.GetUsers();

            #region Class Example
            /* Class Example            var filteredSongs = songs.Where(song => song.Genre == genre && song.ReleaseDate.Year >= 2020)
                                    .OrderBy(song => song.ReleaseDate);
                                    //.Select(song => song.Title);
            foreach (var song in filteredSongs)
            {
                Console.WriteLine(song.GetTitleInitials());
            }
            */
            #endregion

            #region Exercise1
            /*
            string artist = "Bad Bunny";
            var filterBadBunnySongs = songs.Where(song => song.Artist == artist && song.DurationSeconds >= 150 &&
                                        song.DurationSeconds <= 260)
                                        .OrderByDescending(song => song.ReleaseDate)
                                        .ThenBy(song => song.Title)
                                        .Select(song => $"{song.Title} - {song.Artist} ({song.DurationSeconds / 60:D2}:{song.DurationSeconds % 60:D2})")
                                        .ToList(); // materialization

            foreach (var song in filterBadBunnySongs) // now is a string
            {
                Console.WriteLine(song);

            }
            */
            #endregion

            #region Exercise2
            /*
            var allPlaylists = playlists.SelectMany(playlist => playlist.Songs)
                   .Where(song => song != null) // SONG OBJECT
                   .Select(song => song.Artist).Distinct()
                   .OrderBy(song => song);


            foreach (var song in allPlaylists) // now is a string
            {
                Console.WriteLine(song);

            }
            */
            // query syntax
            var queryPlaylists =
                (from playlist in playlists
                 from song in playlist.Songs

                 where song != null
                 select song.Artist)
                .Distinct().OrderBy(song => song)

            foreach (var query in queryPlaylists)
            {
                Console.WriteLine(query);
            }
            #endregion

            #region Exercise3
            /*
            var groupByGenre = songs.GroupBy(song => song.Genre)
                .Select(group => new
                { // anonymous object
                    Genre = group.Key,
                    Count = group.Count(),
                    Average = group.Average(song => song.DurationSeconds),
                    TopSongTitle = group.OrderByDescending(song => song.PlayCount)
                    .First().Title
                })
                .ToList();

            foreach (var genreStats in groupByGenre) // now is a string
            {
                Console.WriteLine($"Genres: {genreStats.Genre}, Songs: {genreStats.Count}, Average: {genreStats.Average:0.00}, Top Song: {genreStats.TopSongTitle}");

            }
            // Blocking operations: Specially GroupBy, because we're bringing the entire dataset
            // For example if there are 1000 songs with 50 genres, it would take some time and block other operations
            // Also, the count, average and order because we're iterating the whole data
            */
            #endregion

            #region Exercise4
            //EXERCISE 4: Hacerlo con method syntax
            /*
            var joinRomanticMovies =
                from movie in movies
                join director in directors on movie.DirectorId equals director.Id
                where movie.Genre == "romance"
                select new { DirectorName = director.Name, MovieTitle = movie.Title, Rating = movie.Rating }; //anonymous object

            foreach (var romanticMovies in joinRomanticMovies) // now is a string
            {
                Console.WriteLine($"Director: {romanticMovies.DirectorName}, Movie: {romanticMovies.MovieTitle}, Rating: {romanticMovies.Rating}");

            }
            */

            // method syntax solution
            var methodSyntaxJoin = movies.Join(directors,
                movie => movie.DirectorId,
                director => director.Id,
                (movie, director) => new
                {
                    DirectorName = director.Name,
                    MovieTitle = movie.Title,
                    Rating = movie.Rating
                }
                )
                .Where(movie => movie.Genre == "romance");

            foreach (var romanticMethod in methodSyntaxJoin)
            {
                Console.WriteLine($"Director: {romanticMethod.DirectorName}, Movie: {romanticMethod.MovieTitle}, Rating: {romanticMethod.Rating}");
            }
            #endregion

            #region Exercise5
            /*
            // EXERCISE 5: Filtrar peliculas primero
            // Here a group join is necessary, to count the romantic movies 
            var allDirectors =
                from director in directors
                join movie in movies on director.Id equals movie.DirectorId into moviesGroup
                select new
                {
                    Name = director.Name,
                    RomanceMovieCount = moviesGroup.Where(movie => movie.Genre == "romance")
                    .Count()
                };

            foreach (var director in allDirectors)
            {
                Console.WriteLine($"Director: {director.Name}, RomanticMovies: {director.RomanceMovieCount}");
            }
            */

            // METHOD SYNTAX
            var allDirectorsMethodSyntax =
                directors.GroupJoin(movies, director => director.Id, movie => movie.DirectorId,
                (director, moviesGroup) => new
                {
                    Name = director.Name,
                    RomanceMovieCount = moviesGroup.Count(movie => movie.Genre == "romance")
                });
            foreach (var otherDirector in allDirectorsMethodSyntax)
            {
                Console.WriteLine($"Director: {otherDirector.Name}, RomanticMovies: {otherDirector.RomanceMovieCount}");

            }
            #endregion

            #region Exercise6
            /*
            var popularSongs = FilterSongs(songs, song => song.PlayCount > 1_000_000);
            Console.WriteLine("POPULAR SONGS:");
            foreach (var popularSong in popularSongs) 
            {
                Console.WriteLine(popularSong);
            }

            var taylorSongs = FilterSongs(songs, song => song.Artist == "Taylor Swift" && song.DurationSeconds < 210);
            Console.WriteLine("TAYLOR SWIFT:");
            foreach (var taylorSong in taylorSongs)
            {
                Console.WriteLine(taylorSong);
            }
            */

            // We can test only the function and the rule, instead of testing into harcoded data or 
            // method, the method never changes, only the rules
            #endregion

            #region Exercise7
            /* debug
            var thresholds = new[] { 100 _000, 1 _000_000, 10 _000_000 };
            var predicates = new List<Func<Song, bool>>();

            // Crea predicados en un loop
            for (int i = 0; i < thresholds.Length; i++)
            {
                predicates.Add(s = > s.PlayCount >= thresholds[i]); // if the loop ends: i = 3
            }

            // Ejecuta cada predicado sobre una cancion
            var testSong = new Song { PlayCount = 500 _000 };
            foreach (var pred in predicates) // all predicates captured i variable no it's value
            {
                Console.WriteLine(pred(testSong));
            } // error: index out of range 3 doesn't exists, the array is [0 1 2]
            */

            var thresholds = new[] { 100_000, 1_000_000, 10_000_000 };
            var predicates = new List<Func<Song, bool>>();

            // Crea predicados en un loop
            for (int i = 0; i < thresholds.Length; i++)
            {
                var actualTreshold = i;
                predicates.Add(s => s.PlayCount >= treshholds[actualTreshold]);
            }

            var testSong = new Song { PlayCount = 500_000 };
            foreach (var pred in predicates)
            {
                Console.WriteLine(pred(testSong));
            }
            #endregion

            #region Exercise8
            /*
            var topSongs = songs.TopByPlays(3);
            foreach (var song in topSongs)
            {
                Console.WriteLine(song.ToLabel());
            }
            */
            #endregion

            #region Exercise9
            /*
            IQueryable<Song> dbSongs =
                songs.AsQueryable();
            string selectedArtist = Console.ReadLine();
            // first version
            var filterSongs = dbSongs.Where(s => s.Artist == selectedArtist).Take(20)
                .Select(song => song.Title);

            // second version
            var filterOtherSongs = dbSongs.Where(song => song.Artist == selectedArtist)
                    .Select(song => song.Title)
                    .Take(20);

            foreach (var song in filterOtherSongs)
            { 
                Console.WriteLine(song);
            }

            // Question 2
            // Using C# methods that SQL doesn't recognize, such as custom methods.
            // Incorrect operator order using Take before Where brings more data into SQL. (always filter first)
            // Use external data or variables that manages app state.

            // Question 3: The effect is that one part is gone to be executed in sql and other in memory.
            */
            #endregion

            #region Exercise10
            var bossBattle = users.Where(user => user.Watched.Count(movie => movie.Genre == "romance") >= 3)
                .ToList() // in the case we're going to reuse this filter
                .SelectMany(user => user.Watched) // directly movie object
                .DistinctBy(movie => movie.Id)
                .OrderByDescending(movie => movie.Rating)
                .ThenBy(movie => movie.DurationMinutes)
                .Take(5)
                .Select(movie => movie.Title);

            // other case to materialize: at the final if we're going to use a foreach only like 1 execution

            foreach (var movie in bossBattle)
            {
                Console.WriteLine(movie);
            }
            #endregion

            Console.WriteLine("\n✅ Programa finalizado. Presiona cualquier tecla para salir.");
            Console.ReadKey();
        }
    }
}