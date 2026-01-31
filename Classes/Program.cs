using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Clase2_CSharp_Ejemplos
{
    // ============================================================
    // SECCION 1: COLLECTIONS
    // ============================================================

    #region Collections Examples

    public class CollectionsExamples
    {
        // List<T> - Lista dinámica
        public static void ListExample()
        {
            Console.WriteLine("=== LIST<T> EXAMPLE ===");

            var animeEpisodes = new List<Episode>
            {
                new Episode { Number = 1, Title = "El comienzo", Duration = 24 },
                new Episode { Number = 2, Title = "Nuevo aliado", Duration = 24 },
                new Episode { Number = 3, Title = "Primera batalla", Duration = 24 }
            };

            // Agregar
            animeEpisodes.Add(new Episode { Number = 4, Title = "Entrenamiento", Duration = 24 });

            // Buscar
            var episode = animeEpisodes.Find(e => e.Number == 2);
            Console.WriteLine($"Encontrado: {episode?.Title}");

            // Filtrar
            var longEpisodes = animeEpisodes.Where(e => e.Duration > 20).ToList();
            Console.WriteLine($"Episodios largos: {longEpisodes.Count}");

            // Ordenar
            animeEpisodes.Sort((a, b) => a.Number.CompareTo(b.Number));

            // Recorrer
            foreach (var ep in animeEpisodes)
            {
                Console.WriteLine($"Episodio {ep.Number}: {ep.Title}");
            }

            Console.WriteLine();
        }

        // Dictionary<TKey, TValue> - Hash map
        public static void DictionaryExample()
        {
            Console.WriteLine("=== DICTIONARY<TKey, TValue> EXAMPLE ===");

            var characterPowerLevels = new Dictionary<string, int>
            {
                { "Goku", 9000 },
                { "Vegeta", 8500 },
                { "Gohan", 7000 },
                { "Piccolo", 6500 }
            };

            // Agregar
            characterPowerLevels["Krillin"] = 5000;

            // Actualizar
            characterPowerLevels["Goku"] = 9500;

            // Acceso seguro con TryGetValue
            if (characterPowerLevels.TryGetValue("Goku", out int gokuPower))
            {
                Console.WriteLine($"Poder de Goku: {gokuPower}");
            }

            // Verificar existencia
            if (characterPowerLevels.ContainsKey("Yamcha"))
            {
                Console.WriteLine("Yamcha está registrado");
            }
            else
            {
                Console.WriteLine("Yamcha no está registrado");
            }

            // Recorrer
            foreach (var kvp in characterPowerLevels)
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value}");
            }

            // Obtener solo keys o values
            var characters = characterPowerLevels.Keys.ToList();
            var powers = characterPowerLevels.Values.ToList();

            Console.WriteLine($"Total personajes: {characters.Count}");
            Console.WriteLine($"Poder promedio: {powers.Average()}");

            Console.WriteLine();
        }

        // HashSet<T> - Colección de elementos únicos
        public static void HashSetExample()
        {
            Console.WriteLine("=== HASHSET<T> EXAMPLE ===");

            var concertFans = new HashSet<string>();

            // Agregar fans
            concertFans.Add("Ana");
            concertFans.Add("Luis");
            concertFans.Add("Maria");
            concertFans.Add("Ana"); // Duplicado, se ignora

            Console.WriteLine($"Fans únicos registrados: {concertFans.Count}"); // 3

            // Verificar pertenencia (O(1) promedio)
            if (concertFans.Contains("Luis"))
            {
                Console.WriteLine("Luis está registrado");
            }

            // Operaciones de conjuntos
            var vipFans = new HashSet<string> { "Ana", "Carlos", "Pedro" };

            // Unión
            var allFans = new HashSet<string>(concertFans);
            allFans.UnionWith(vipFans);
            Console.WriteLine($"Total fans (unión): {allFans.Count}");

            // Intersección
            var commonFans = new HashSet<string>(concertFans);
            commonFans.IntersectWith(vipFans);
            Console.WriteLine($"Fans en común: {commonFans.Count}");

            // Diferencia
            var regularFans = new HashSet<string>(concertFans);
            regularFans.ExceptWith(vipFans);
            Console.WriteLine($"Fans regulares (no VIP): {regularFans.Count}");

            Console.WriteLine();
        }

        // Queue<T> - FIFO (First In, First Out)
        public static void QueueExample()
        {
            Console.WriteLine("=== QUEUE<T> EXAMPLE ===");

            var watchQueue = new Queue<string>();

            // Encolar episodios
            watchQueue.Enqueue("Attack on Titan - Ep 1");
            watchQueue.Enqueue("Attack on Titan - Ep 2");
            watchQueue.Enqueue("Attack on Titan - Ep 3");
            watchQueue.Enqueue("Attack on Titan - Ep 4");

            Console.WriteLine($"Episodios en cola: {watchQueue.Count}");

            // Ver el siguiente sin removerlo
            string next = watchQueue.Peek();
            Console.WriteLine($"Siguiente a ver: {next}");

            // Procesar la cola
            while (watchQueue.Count > 0)
            {
                string episode = watchQueue.Dequeue();
                Console.WriteLine($"Viendo: {episode}");
            }

            Console.WriteLine($"Cola vacía: {watchQueue.Count == 0}");
            Console.WriteLine();
        }

        // Stack<T> - LIFO (Last In, First Out)
        public static void StackExample()
        {
            Console.WriteLine("=== STACK<T> EXAMPLE ===");

            var songHistory = new Stack<string>();

            // Apilar canciones reproducidas
            songHistory.Push("Opening - Naruto");
            songHistory.Push("Ending - Attack on Titan");
            songHistory.Push("OST - Demon Slayer");
            songHistory.Push("Opening - Jujutsu Kaisen");

            Console.WriteLine($"Canciones en historial: {songHistory.Count}");

            // Ver la última sin removerla
            string lastPlayed = songHistory.Peek();
            Console.WriteLine($"Última reproducida: {lastPlayed}");

            // Retroceder en el historial
            Console.WriteLine("\nRetrocediendo en el historial:");
            while (songHistory.Count > 0)
            {
                string song = songHistory.Pop();
                Console.WriteLine($"Volviendo a: {song}");
            }

            Console.WriteLine();
        }
    }

    // Clase auxiliar para ejemplos
    public class Episode
    {
        public int Number { get; set; }
        public string Title { get; set; }
        public int Duration { get; set; }
    }

    #endregion

    // ============================================================
    // SECCION 2: TIPOS DE CLASSES
    // ============================================================

    #region Class Types

    // CONCRETE CLASS - Clase normal, instanciable
    public class AnimeCharacter
    {
        public string Name { get; set; }
        public string Anime { get; set; }
        public int PowerLevel { get; set; }

        public AnimeCharacter(string name, string anime, int powerLevel)
        {
            Name = name;
            Anime = anime;
            PowerLevel = powerLevel;
        }

        public virtual void Attack()
        {
            Console.WriteLine($"{Name} ataca con poder {PowerLevel}!");
        }

        public void Train(int hours)
        {
            PowerLevel += hours * 100;
            Console.WriteLine($"{Name} entrenó {hours} horas. Nuevo poder: {PowerLevel}");
        }

        public override string ToString()
        {
            return $"{Name} ({Anime}) - Power: {PowerLevel}";
        }
    }

    // ABSTRACT CLASS - No instanciable, sirve como base
    public abstract class MediaContent
    {
        public string Title { get; set; }
        public int DurationMinutes { get; set; }
        public DateTime ReleaseDate { get; set; }

        // Método abstracto - debe ser implementado por clases derivadas
        public abstract void Play();

        // Método abstracto con retorno
        public abstract string GetContentType();

        // Método concreto - compartido por todas las derivadas
        public void ShowInfo()
        {
            Console.WriteLine($"Título: {Title}");
            Console.WriteLine($"Duración: {DurationMinutes} minutos");
            Console.WriteLine($"Tipo: {GetContentType()}");
            Console.WriteLine($"Lanzamiento: {ReleaseDate:dd/MM/yyyy}");
        }

        // Método virtual - puede ser sobreescrito opcionalmente
        public virtual void AddToFavorites()
        {
            Console.WriteLine($"{Title} agregado a favoritos");
        }
    }

    public class AnimeEpisode : MediaContent
    {
        public int EpisodeNumber { get; set; }
        public int Season { get; set; }
        public string AnimeSeries { get; set; }

        public override void Play()
        {
            Console.WriteLine($"▶ Reproduciendo: {AnimeSeries} - T{Season}E{EpisodeNumber}: {Title}");
            Console.WriteLine($"Duración: {DurationMinutes} minutos");
        }

        public override string GetContentType()
        {
            return "Episodio de Anime";
        }

        public override void AddToFavorites()
        {
            Console.WriteLine($"Episodio {EpisodeNumber} de {AnimeSeries} agregado a favoritos");
        }
    }

    public class Song : MediaContent
    {
        public string Artist { get; set; }
        public string Album { get; set; }
        public string Genre { get; set; }

        public override void Play()
        {
            Console.WriteLine($"♪ Reproduciendo: {Title} - {Artist}");
            Console.WriteLine($"Álbum: {Album} | Género: {Genre}");
        }

        public override string GetContentType()
        {
            return "Canción";
        }
    }

    public class SeriesEpisode : MediaContent
    {
        public string SeriesName { get; set; }
        public int EpisodeNumber { get; set; }
        public int Season { get; set; }
        public double Rating { get; set; }

        public override void Play()
        {
            Console.WriteLine($"▶ Reproduciendo: {SeriesName} - {Season}x{EpisodeNumber:D2}");
            Console.WriteLine($"Título: {Title}");
            Console.WriteLine($"Rating: {Rating}/10");
        }

        public override string GetContentType()
        {
            return "Episodio de Serie";
        }
    }

    // STATIC CLASS - No instanciable, solo miembros estáticos
    public static class AnimeUtils
    {
        // Constantes
        public const int StandardEpisodeDuration = 24;
        public const int MaxPowerLevel = 999999;

        // Método estático para calcular incremento de poder
        public static int CalculatePowerIncrease(int currentPower, int trainingHours)
        {
            return currentPower + (trainingHours * 100);
        }

        // Formatear título de episodio
        public static string FormatEpisodeTitle(string series, int season, int episode)
        {
            return $"{series} - T{season}E{episode:D2}";
        }

        // Calcular rating promedio
        public static double CalculateAverageRating(List<double> ratings)
        {
            if (ratings == null || ratings.Count == 0)
                return 0;

            return ratings.Average();
        }

        // Determinar nivel de poder
        public static string GetPowerLevelCategory(int powerLevel)
        {
            return powerLevel switch
            {
                < 1000 => "Novato",
                < 5000 => "Intermedio",
                < 10000 => "Avanzado",
                < 50000 => "Élite",
                _ => "Legendario"
            };
        }

        // Validar nombre de personaje
        public static bool IsValidCharacterName(string name)
        {
            return !string.IsNullOrWhiteSpace(name) && name.Length >= 2 && name.Length <= 50;
        }
    }

    // SEALED CLASS - No puede ser heredada
    public class Enemy
    {
        public string Name { get; set; }
        public int Health { get; set; }

        public virtual void Attack()
        {
            Console.WriteLine($"{Name} realiza un ataque básico");
        }
    }

    public sealed class FinalBoss : Enemy
    {
        public int Phase { get; set; }
        public List<string> SpecialAbilities { get; set; }

        public FinalBoss()
        {
            SpecialAbilities = new List<string>();
        }

        public override void Attack()
        {
            Console.WriteLine($"💀 {Name} (Fase {Phase}) ejecuta un ataque devastador!");

            if (SpecialAbilities.Count > 0)
            {
                var ability = SpecialAbilities[new Random().Next(SpecialAbilities.Count)];
                Console.WriteLine($"   Habilidad especial: {ability}");
            }
        }

        public void TransformToNextPhase()
        {
            Phase++;
            Health = Health * 2;
            Console.WriteLine($"⚡ {Name} se transforma a la Fase {Phase}!");
            Console.WriteLine($"   Salud aumentada a: {Health}");
        }
    }

    // Esto NO compilaría:
    //public class SuperFinalBoss : FinalBoss { }

    // PARTIAL CLASS - Definición dividida en múltiples partes
    // Parte 1: Propiedades y datos
    public partial class Character
    {
        public string Name { get; set; }
        public string Anime { get; set; }
        public int PowerLevel { get; set; }
        public int Health { get; set; }
        public int Stamina { get; set; }
        public List<string> Abilities { get; set; }

        public Character()
        {
            Abilities = new List<string>();
            Health = 100;
            Stamina = 100;
        }
    }

    // Parte 2: Comportamiento y métodos
    public partial class Character
    {
        public void Train()
        {
            PowerLevel += 100;
            Stamina -= 20;
            Console.WriteLine($"{Name} entrenó! Poder: {PowerLevel}, Stamina: {Stamina}");
        }

        public void Rest()
        {
            Health = 100;
            Stamina = 100;
            Console.WriteLine($"{Name} descansó y recuperó toda su energía");
        }

        public void LearnAbility(string ability)
        {
            if (!Abilities.Contains(ability))
            {
                Abilities.Add(ability);
                Console.WriteLine($"{Name} aprendió: {ability}");
            }
        }

        public void UseAbility(string ability)
        {
            if (Abilities.Contains(ability))
            {
                Stamina -= 30;
                Console.WriteLine($"{Name} usa {ability}! (Stamina: {Stamina})");
            }
            else
            {
                Console.WriteLine($"{Name} no conoce la habilidad: {ability}");
            }
        }

        public void DisplayStatus()
        {
            Console.WriteLine($"\n=== {Name} ({Anime}) ===");
            Console.WriteLine($"Poder: {PowerLevel}");
            Console.WriteLine($"Salud: {Health}");
            Console.WriteLine($"Stamina: {Stamina}");
            Console.WriteLine($"Habilidades: {string.Join(", ", Abilities)}");
        }
    }

    // NESTED CLASS - Clase dentro de otra clase
    public class AnimeSeries
    {
        private string seriesName;
        private List<Episode> episodes;
        private int currentSeason;

        public AnimeSeries(string name, int season)
        {
            seriesName = name;
            currentSeason = season;
            episodes = new List<Episode>();
        }

        public void AddEpisode(int number, string title, int duration)
        {
            var episode = new Episode(this)
            {
                Number = number,
                Title = title,
                Duration = duration,
                Season = currentSeason
            };
            episodes.Add(episode);
        }

        public void DisplayAllEpisodes()
        {
            Console.WriteLine($"\n=== {seriesName} - Temporada {currentSeason} ===");
            foreach (var ep in episodes)
            {
                ep.Display();
            }
        }

        // NESTED CLASS - Tiene acceso a miembros privados de la clase contenedora
        public class Episode
        {
            private AnimeSeries parentSeries;

            public int Number { get; set; }
            public string Title { get; set; }
            public int Duration { get; set; }
            public int Season { get; set; }
            public bool Watched { get; set; }

            public Episode(AnimeSeries series)
            {
                parentSeries = series;
            }

            public void Display()
            {
                // Puede acceder a miembros privados de AnimeSeries
                string status = Watched ? "✓" : "○";
                Console.WriteLine($"{status} {parentSeries.seriesName} - T{Season}E{Number:D2}: {Title} ({Duration} min)");
            }

            public void MarkAsWatched()
            {
                Watched = true;
                Console.WriteLine($"Episodio {Number} marcado como visto");
            }
        }
    }

    #endregion

    // ============================================================
    // SECCION 3: ENCAPSULATION
    // ============================================================

    #region Encapsulation

    public class MusicPlaylist
    {
        // Campo privado
        private List<string> songs;
        private string owner;

        // Property con validación
        public string Name { get; set; }

        // Property de solo lectura (solo get público)
        public int SongCount => songs.Count;

        // Property con get público y set privado
        public DateTime CreatedDate { get; private set; }

        // Property con lógica en el setter
        private int maxSongs;
        public int MaxSongs
        {
            get => maxSongs;
            set
            {
                if (value > 0 && value <= 1000)
                    maxSongs = value;
                else
                    throw new ArgumentException("MaxSongs debe estar entre 1 y 1000");
            }
        }

        // Constructor
        public MusicPlaylist(string name, string owner)
        {
            Name = name;
            this.owner = owner;
            songs = new List<string>();
            CreatedDate = DateTime.Now;
            maxSongs = 100;
        }

        // Método público que controla el acceso a la lista privada
        public bool AddSong(string song)
        {
            if (songs.Count >= maxSongs)
            {
                Console.WriteLine($"Playlist llena. Máximo: {maxSongs} canciones");
                return false;
            }

            if (string.IsNullOrWhiteSpace(song))
            {
                Console.WriteLine("Nombre de canción inválido");
                return false;
            }

            songs.Add(song);
            Console.WriteLine($"'{song}' agregada a '{Name}'");
            return true;
        }

        public bool RemoveSong(string song)
        {
            bool removed = songs.Remove(song);
            if (removed)
                Console.WriteLine($"'{song}' removida de '{Name}'");
            else
                Console.WriteLine($"'{song}' no encontrada en '{Name}'");

            return removed;
        }

        // Retornar copia de solo lectura
        public IReadOnlyList<string> GetSongs()
        {
            return songs.AsReadOnly();
        }

        public void DisplayPlaylist()
        {
            Console.WriteLine($"\n=== Playlist: {Name} ===");
            Console.WriteLine($"Propietario: {owner}");
            Console.WriteLine($"Creada: {CreatedDate:dd/MM/yyyy}");
            Console.WriteLine($"Canciones: {songs.Count}/{maxSongs}");

            for (int i = 0; i < songs.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {songs[i]}");
            }
        }
    }

    // Ejemplo de encapsulation con backing field
    public class Fan
    {
        private string name;
        private int age;

        public string Name
        {
            get => name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("El nombre no puede estar vacío");
                name = value;
            }
        }

        public int Age
        {
            get => age;
            set
            {
                if (value < 0 || value > 120)
                    throw new ArgumentException("Edad inválida");
                age = value;
            }
        }

        public string FavoriteAnime { get; set; }

        // Property calculada
        public bool IsAdult => age >= 18;

        public Fan(string name, int age, string favoriteAnime)
        {
            Name = name; // Usa el setter con validación
            Age = age;   // Usa el setter con validación
            FavoriteAnime = favoriteAnime;
        }

        public override string ToString()
        {
            return $"{Name} ({Age} años) - Fan de {FavoriteAnime}";
        }

        // Sobrescribir Equals y GetHashCode para usar en HashSet
        public override bool Equals(object obj)
        {
            if (obj is Fan other)
                return Name.Equals(other.Name, StringComparison.OrdinalIgnoreCase);
            return false;
        }

        public override int GetHashCode()
        {
            return Name.ToLower().GetHashCode();
        }
    }

    #endregion

    // ============================================================
    // SECCION 4: INTERFACES
    // ============================================================

    #region Interfaces

    // Interface básica
    public interface IStreamingService
    {
        void PlayContent(string title);
        void PauseContent();
        void StopContent();
        int GetCurrentPosition();
    }

    // Implementación 1
    public class AnimeStreamingService : IStreamingService
    {
        private string currentContent;
        private int position;
        private bool isPlaying;

        public void PlayContent(string title)
        {
            currentContent = title;
            isPlaying = true;
            Console.WriteLine($"▶ Reproduciendo anime: {title}");
        }

        public void PauseContent()
        {
            if (isPlaying)
            {
                isPlaying = false;
                Console.WriteLine($"⏸ Anime pausado en {position} segundos");
            }
        }

        public void StopContent()
        {
            isPlaying = false;
            position = 0;
            Console.WriteLine($"⏹ Reproducción detenida");
        }

        public int GetCurrentPosition()
        {
            return position;
        }
    }

    // Implementación 2
    public class MusicStreamingService : IStreamingService
    {
        private string currentSong;
        private int position;
        private bool isPlaying;

        public void PlayContent(string title)
        {
            currentSong = title;
            isPlaying = true;
            Console.WriteLine($"♪ Reproduciendo canción: {title}");
        }

        public void PauseContent()
        {
            if (isPlaying)
            {
                isPlaying = false;
                Console.WriteLine($"⏸ Canción pausada");
            }
        }

        public void StopContent()
        {
            isPlaying = false;
            position = 0;
            Console.WriteLine($"⏹ Música detenida");
        }

        public int GetCurrentPosition()
        {
            return position;
        }
    }

    // Clase que usa la interface (polymorphism)
    public class ContentPlayer
    {
        private readonly IStreamingService service;

        public ContentPlayer(IStreamingService service)
        {
            this.service = service;
        }

        public void Play(string title)
        {
            service.PlayContent(title);
        }

        public void Pause()
        {
            service.PauseContent();
        }

        public void Stop()
        {
            service.StopContent();
        }
    }

    // Interfaces múltiples
    public interface IPlayable
    {
        void Play();
    }

    public interface IDownloadable
    {
        void Download(string path);
        long GetFileSize();
    }

    public interface IRateable
    {
        void Rate(int stars);
        double GetAverageRating();
    }

    // Una clase puede implementar múltiples interfaces
    public class DownloadableEpisode : IPlayable, IDownloadable, IRateable
    {
        public string Title { get; set; }
        public long FileSizeBytes { get; set; }
        private List<int> ratings = new List<int>();

        public void Play()
        {
            Console.WriteLine($"Reproduciendo: {Title}");
        }

        public void Download(string path)
        {
            Console.WriteLine($"Descargando '{Title}' a {path}...");
            Console.WriteLine($"Tamaño: {FileSizeBytes / 1024 / 1024} MB");
        }

        public long GetFileSize()
        {
            return FileSizeBytes;
        }

        public void Rate(int stars)
        {
            if (stars >= 1 && stars <= 5)
            {
                ratings.Add(stars);
                Console.WriteLine($"Calificación agregada: {stars} estrellas");
            }
        }

        public double GetAverageRating()
        {
            return ratings.Count > 0 ? ratings.Average() : 0;
        }
    }

    #endregion

    // ============================================================
    // SECCION 5: POLYMORPHISM
    // ============================================================

    #region Polymorphism

    // Ejemplo completo de polymorphism con abstract class
    public abstract class Fighter
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int AttackPower { get; set; }

        protected Fighter(string name, int health, int attackPower)
        {
            Name = name;
            Health = health;
            AttackPower = attackPower;
        }

        // Método abstracto - cada tipo de luchador ataca diferente
        public abstract void SpecialAttack();

        // Método virtual - puede ser sobreescrito
        public virtual void BasicAttack()
        {
            Console.WriteLine($"{Name} realiza un ataque básico ({AttackPower} de daño)");
        }

        // Método concreto
        public void TakeDamage(int damage)
        {
            Health -= damage;
            Console.WriteLine($"{Name} recibe {damage} de daño. Salud restante: {Health}");
        }

        public bool IsAlive()
        {
            return Health > 0;
        }
    }

    public class Saiyan : Fighter
    {
        public int TransformationLevel { get; set; }

        public Saiyan(string name, int health, int attackPower)
            : base(name, health, attackPower)
        {
            TransformationLevel = 1;
        }

        public override void SpecialAttack()
        {
            int damage = AttackPower * TransformationLevel * 2;
            Console.WriteLine($"💥 {Name} usa Kamehameha! ({damage} de daño)");
        }

        public override void BasicAttack()
        {
            Console.WriteLine($"👊 {Name} lanza un combo de golpes rápidos");
        }

        public void Transform()
        {
            TransformationLevel++;
            AttackPower = (int)(AttackPower * 1.5);
            Console.WriteLine($"⚡ {Name} se transforma a Super Saiyan {TransformationLevel}!");
            Console.WriteLine($"   Poder de ataque aumentado a: {AttackPower}");
        }
    }

    public class Ninja : Fighter
    {
        public int ChakraPoints { get; set; }

        public Ninja(string name, int health, int attackPower)
            : base(name, health, attackPower)
        {
            ChakraPoints = 100;
        }

        public override void SpecialAttack()
        {
            if (ChakraPoints >= 30)
            {
                ChakraPoints -= 30;
                int damage = AttackPower * 3;
                Console.WriteLine($"🌀 {Name} usa Rasengan! ({damage} de daño)");
                Console.WriteLine($"   Chakra restante: {ChakraPoints}");
            }
            else
            {
                Console.WriteLine($"{Name} no tiene suficiente chakra");
            }
        }

        public override void BasicAttack()
        {
            Console.WriteLine($"🗡 {Name} lanza kunais");
        }

        public void RestoreChakra()
        {
            RestoreChakra(100);
        }

        public void RestoreChakra(int chakraPoints)
        {
            ChakraPoints = chakraPoints;
            Console.WriteLine($"{Name} restauró su chakra");
        }
    }

    public class Shinigami : Fighter
    {
        public string ZanpakutoName { get; set; }
        public bool BankaiReleased { get; set; }

        public Shinigami(string name, int health, int attackPower, string zanpakuto)
            : base(name, health, attackPower)
        {
            ZanpakutoName = zanpakuto;
            BankaiReleased = false;
        }

        public override void SpecialAttack()
        {
            int multiplier = BankaiReleased ? 5 : 2;
            int damage = AttackPower * multiplier;
            string attackName = BankaiReleased ? "Bankai" : "Shikai";
            Console.WriteLine($"⚔ {Name} libera {attackName}: {ZanpakutoName}! ({damage} de daño)");
        }

        public void ReleaseBankai()
        {
            if (!BankaiReleased)
            {
                BankaiReleased = true;
                AttackPower *= 2;
                Console.WriteLine($"💀 {Name} libera Bankai: {ZanpakutoName}!");
                Console.WriteLine($"   Poder de ataque duplicado: {AttackPower}");
            }
        }
    }

    // Uso polimórfico
    public class Arena
    {
        private List<Fighter> fighters;

        public Arena()
        {
            fighters = new List<Fighter>();
        }

        public void AddFighter(Fighter fighter)
        {
            fighters.Add(fighter);
            Console.WriteLine($"{fighter.Name} entra a la arena");
        }

        public void StartBattle()
        {
            Console.WriteLine("\n=== ¡COMIENZA LA BATALLA! ===\n");

            foreach (var fighter in fighters)
            {
                if (fighter.IsAlive())
                {
                    fighter.BasicAttack();
                    fighter.SpecialAttack(); // Polymorphism: cada uno ejecuta su versión
                    Console.WriteLine();
                }
            }
        }

        public void DisplayFighters()
        {
            Console.WriteLine("\n=== Luchadores en la Arena ===");
            foreach (var fighter in fighters)
            {
                string status = fighter.IsAlive() ? "Vivo" : "Derrotado";
                Console.WriteLine($"- {fighter.Name}: {fighter.Health} HP ({status})");
            }
        }
    }

    #endregion

    // ============================================================
    // SECCION 6: SOLID PRINCIPLES
    // ============================================================

    #region SOLID Principles

    // ===== S - SINGLE RESPONSIBILITY PRINCIPLE =====

    // MAL: Clase con múltiples responsabilidades
    public class BadPlaylistManager
    {
        private List<string> songs = new List<string>();

        public void AddSong(string song) => songs.Add(song);
        public void RemoveSong(string song) => songs.Remove(song);

        // Responsabilidad extra: persistencia
        public void SaveToFile(string path) { /* ... */ }

        // Responsabilidad extra: notificaciones
        public void SendEmailNotification(string email) { /* ... */ }

        // Responsabilidad extra: logging
        public void LogActivity(string message) { /* ... */ }
    }

    // BIEN: Separando responsabilidades
    public class Playlist
    {
        private List<string> songs = new List<string>();

        public void AddSong(string song) => songs.Add(song);
        public void RemoveSong(string song) => songs.Remove(song);
        public IReadOnlyList<string> GetSongs() => songs.AsReadOnly();
    }

    public class PlaylistStorage
    {
        public void SaveToFile(Playlist playlist, string path)
        {
            Console.WriteLine($"Guardando playlist en {path}");
            // Lógica de escritura
        }

        public Playlist LoadFromFile(string path)
        {
            Console.WriteLine($"Cargando playlist desde {path}");
            // Lógica de lectura
            return new Playlist();
        }
    }

    public class PlaylistNotificationService
    {
        public void SendEmail(string email, string message)
        {
            Console.WriteLine($"Enviando email a {email}: {message}");
        }

        public void SendPushNotification(string userId, string message)
        {
            Console.WriteLine($"Enviando push a {userId}: {message}");
        }
    }

    // ===== O - OPEN/CLOSED PRINCIPLE =====

    // Abierto a extensión, cerrado a modificación
    public abstract class ContentFilter
    {
        public abstract bool Matches(MediaContent content);
    }

    public class GenreFilter : ContentFilter
    {
        private string genre;

        public GenreFilter(string genre)
        {
            this.genre = genre;
        }

        public override bool Matches(MediaContent content)
        {
            if (content is Song song)
                return song.Genre == genre;
            return false;
        }
    }

    public class DurationFilter : ContentFilter
    {
        private int minDuration;
        private int maxDuration;

        public DurationFilter(int min, int max)
        {
            minDuration = min;
            maxDuration = max;
        }

        public override bool Matches(MediaContent content)
        {
            return content.DurationMinutes >= minDuration &&
                   content.DurationMinutes <= maxDuration;
        }
    }

    public class ContentCatalog
    {
        private List<MediaContent> contents = new List<MediaContent>();

        public void AddContent(MediaContent content)
        {
            contents.Add(content);
        }

        // Método abierto a extensión mediante filtros
        public List<MediaContent> Search(ContentFilter filter)
        {
            var results = new List<MediaContent>();
            foreach (var content in contents)
            {
                if (filter.Matches(content))
                    results.Add(content);
            }
            return results;
        }
    }

    // ===== L - LISKOV SUBSTITUTION PRINCIPLE =====

    // Las subclases deben poder sustituir a su clase base
    public abstract class Vehicle
    {
        public abstract void Start();
        public abstract void Stop();
        public abstract int GetSpeed();
    }

    public class Car : Vehicle
    {
        private int speed;

        public override void Start()
        {
            Console.WriteLine("Auto encendido");
            speed = 0;
        }

        public override void Stop()
        {
            Console.WriteLine("Auto detenido");
            speed = 0;
        }

        public override int GetSpeed()
        {
            return speed;
        }

        public void Accelerate()
        {
            speed += 10;
        }
    }

    // ===== I - INTERFACE SEGREGATION PRINCIPLE =====

    // Interfaces pequeñas y específicas

    // MAL: Interface muy grande
    public interface IBadMediaPlayer
    {
        void PlayVideo();
        void PlayAudio();
        void ShowSubtitles();
        void AdjustVideoQuality();
        void ShowLyrics();
        void EnableSurroundSound();
    }

    // BIEN: Interfaces segregadas
    public interface IVideoPlayer
    {
        void PlayVideo();
        void AdjustVideoQuality();
    }

    public interface IAudioPlayer
    {
        void PlayAudio();
    }

    public interface ISubtitleSupport
    {
        void ShowSubtitles();
        void HideSubtitles();
    }

    public interface ILyricsSupport
    {
        void ShowLyrics();
    }

    // Implementar solo lo necesario
    public class SimpleAudioPlayer : IAudioPlayer
    {
        public void PlayAudio()
        {
            Console.WriteLine("Reproduciendo audio");
        }
    }

    public class FullVideoPlayer : IVideoPlayer, ISubtitleSupport
    {
        public void PlayVideo()
        {
            Console.WriteLine("Reproduciendo video");
        }

        public void AdjustVideoQuality()
        {
            Console.WriteLine("Ajustando calidad de video");
        }

        public void ShowSubtitles()
        {
            Console.WriteLine("Mostrando subtítulos");
        }

        public void HideSubtitles()
        {
            Console.WriteLine("Ocultando subtítulos");
        }
    }

    // ===== D - DEPENDENCY INVERSION PRINCIPLE =====

    // Depender de abstracciones, no de concreciones

    // Abstracción
    public interface INotificationService
    {
        void Notify(string destination, string message);
    }

    // Implementaciones concretas
    public class EmailNotificationService : INotificationService
    {
        public void Notify(string destination, string message)
        {
            Console.WriteLine($"📧 Email a {destination}: {message}");
        }
    }

    public class PushNotificationService : INotificationService
    {
        public void Notify(string destination, string message)
        {
            Console.WriteLine($"📱 Push a {destination}: {message}");
        }
    }

    public class SMSNotificationService : INotificationService
    {
        public void Notify(string destination, string message)
        {
            Console.WriteLine($"💬 SMS a {destination}: {message}");
        }
    }

    // Clase de alto nivel que depende de la abstracción
    public class EpisodeReleaseNotifier
    {
        private readonly INotificationService notificationService;

        // Dependency Injection via constructor
        public EpisodeReleaseNotifier(INotificationService notificationService)
        {
            this.notificationService = notificationService;
        }

        public void NotifyNewEpisode(string seriesName, int episodeNumber, string user)
        {
            string message = $"¡Nuevo episodio de {seriesName}! Episodio {episodeNumber} ya disponible";
            notificationService.Notify(user, message);
        }
    }

    #endregion

    // ============================================================
    // PROGRAMA PRINCIPAL - EJEMPLOS DE USO
    // ============================================================
    struct Person
    {
        public string Name;
        public int Age;
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("╔════════════════════════════════════════════════════════╗");
            Console.WriteLine("║     CLASE 2 - C# EJEMPLOS COMPLETOS                    ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════╝\n");

            // Descomentar la sección que quieras probar

            // TestCollections();
            // TestClassTypes();
            // TestEncapsulation();
            // TestInterfaces();
            // TestPolymorphism();
            TestSOLID();


        var person = new Person()
        {
            Name = "Luciana",
            Age = 23
        };
        Console.WriteLine("\n\nPresiona cualquier tecla para salir...");
            Console.ReadKey();

        }

        static void TestCollections()
        {
            Console.WriteLine("\n╔════════════════════════════════════════════════════════╗");
            Console.WriteLine("║  SECCION 1: COLLECTIONS                                ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════╝\n");

            CollectionsExamples.ListExample();
            CollectionsExamples.DictionaryExample();
            CollectionsExamples.HashSetExample();
            CollectionsExamples.QueueExample();
            CollectionsExamples.StackExample();
        }

        static void TestClassTypes()
        {
            Console.WriteLine("\n╔════════════════════════════════════════════════════════╗");
            Console.WriteLine("║  SECCION 2: TIPOS DE CLASSES                           ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════╝\n");

            // Concrete Class
            Console.WriteLine("=== CONCRETE CLASS ===");
            var goku = new AnimeCharacter("Goku", "Dragon Ball Z", 9000);
            goku.Attack();
            goku.Train(5);
            Console.WriteLine(goku);
            Console.WriteLine();

            // Abstract Class
            Console.WriteLine("=== ABSTRACT CLASS ===");
            MediaContent anime = new AnimeEpisode
            {
                Title = "El comienzo",
                AnimeSeries = "Attack on Titan",
                Season = 1,
                EpisodeNumber = 1,
                DurationMinutes = 24,
                ReleaseDate = new DateTime(2013, 4, 7)
            };
            anime.ShowInfo();
            anime.Play();
            Console.WriteLine();

            MediaContent song = new Song
            {
                Title = "Gurenge",
                Artist = "LiSA",
                Album = "Demon Slayer OST",
                Genre = "J-Pop",
                DurationMinutes = 4,
                ReleaseDate = new DateTime(2019, 7, 3)
            };
            song.ShowInfo();
            song.Play();
            Console.WriteLine();

            // Static Class
            Console.WriteLine("=== STATIC CLASS ===");
            int newPower = AnimeUtils.CalculatePowerIncrease(5000, 10);
            Console.WriteLine($"Nuevo poder después de entrenar: {newPower}");

            string episodeTitle = AnimeUtils.FormatEpisodeTitle("One Piece", 1, 1000);
            Console.WriteLine($"Título formateado: {episodeTitle}");

            string category = AnimeUtils.GetPowerLevelCategory(15000);
            Console.WriteLine($"Categoría de poder: {category}");
            Console.WriteLine();

            // Sealed Class
            Console.WriteLine("=== SEALED CLASS ===");
            var boss = new FinalBoss
            {
                Name = "Muzan Kibutsuji",
                Health = 10000,
                Phase = 1,
                SpecialAbilities = new List<string>
                {
                    "Regeneración instantánea",
                    "Tentáculos de sangre",
                    "Transformación celular"
                }
            };
            boss.Attack();
            boss.TransformToNextPhase();
            boss.Attack();
            Console.WriteLine();

            // Partial Class
            Console.WriteLine("=== PARTIAL CLASS ===");
            var naruto = new Character
            {
                Name = "Naruto Uzumaki",
                Anime = "Naruto",
                PowerLevel = 5000
            };
            naruto.LearnAbility("Rasengan");
            naruto.LearnAbility("Kage Bunshin");
            naruto.Train();
            naruto.UseAbility("Rasengan");
            naruto.DisplayStatus();
            Console.WriteLine();

            // Nested Class
            Console.WriteLine("=== NESTED CLASS ===");
            var series = new AnimeSeries("Jujutsu Kaisen", 1);
            series.AddEpisode(1, "Ryomen Sukuna", 24);
            series.AddEpisode(2, "Para mí", 24);
            series.AddEpisode(3, "Chica de acero", 24);
            series.DisplayAllEpisodes();
        }

        static void TestEncapsulation()
        {
            Console.WriteLine("\n╔════════════════════════════════════════════════════════╗");
            Console.WriteLine("║  SECCION 3: ENCAPSULATION                              ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════╝\n");

            var playlist = new MusicPlaylist("Anime Openings", "Juan");
            playlist.MaxSongs = 50;

            playlist.AddSong("Unravel - Tokyo Ghoul");
            playlist.AddSong("Gurenge - Demon Slayer");
            playlist.AddSong("My War - Attack on Titan");
            playlist.AddSong("SPECIALZ - Jujutsu Kaisen");

            playlist.DisplayPlaylist();

            Console.WriteLine($"\nTotal de canciones: {playlist.SongCount}");
            Console.WriteLine($"Fecha de creación: {playlist.CreatedDate}");

            Console.WriteLine("\n=== FAN CON VALIDACION ===");
            try
            {
                var fan1 = new Fan("Carlos", 25, "One Piece");
                Console.WriteLine(fan1);
                Console.WriteLine($"Es adulto: {fan1.IsAdult}");

                // Esto lanzará excepción
                // var fan2 = new Fan("", 25, "Naruto");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            // HashSet con Fans
            Console.WriteLine("\n=== HASHSET CON EQUALS/GETHASHCODE ===");
            var fans = new HashSet<Fan>
            {
                new Fan("Ana", 22, "Attack on Titan"),
                new Fan("Luis", 28, "Death Note"),
                new Fan("Ana", 30, "Fullmetal Alchemist") // Duplicado por nombre
            };

            Console.WriteLine($"Fans únicos: {fans.Count}"); // 2
            foreach (var fan in fans)
            {
                Console.WriteLine($"  - {fan}");
            }
        }

        static void TestInterfaces()
        {
            Console.WriteLine("\n╔════════════════════════════════════════════════════════╗");
            Console.WriteLine("║  SECCION 4: INTERFACES                                 ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════╝\n");

            // Polymorphism con interfaces
            IStreamingService animeService = new AnimeStreamingService();
            IStreamingService musicService = new MusicStreamingService();

            var animePlayer = new ContentPlayer(animeService);
            animePlayer.Play("Attack on Titan - Episodio 1");
            animePlayer.Pause();
            animePlayer.Stop();

            Console.WriteLine();

            var musicPlayer = new ContentPlayer(musicService);
            musicPlayer.Play("Unravel - Tokyo Ghoul Opening");
            musicPlayer.Pause();
            musicPlayer.Stop();

            Console.WriteLine("\n=== MULTIPLES INTERFACES ===");
            var episode = new DownloadableEpisode
            {
                Title = "Demon Slayer - Episodio 19",
                FileSizeBytes = 500 * 1024 * 1024 // 500 MB
            };

            episode.Play();
            episode.Download("/downloads");
            episode.Rate(5);
            episode.Rate(4);
            episode.Rate(5);
            Console.WriteLine($"Rating promedio: {episode.GetAverageRating():F2} estrellas");
        }

        static void TestPolymorphism()
        {
            Console.WriteLine("\n╔════════════════════════════════════════════════════════╗");
            Console.WriteLine("║  SECCION 5: POLYMORPHISM                               ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════╝\n");

            var arena = new Arena();

            // Crear diferentes tipos de luchadores
            var goku = new Saiyan("Goku", 1000, 500);
            var naruto = new Ninja("Naruto", 900, 400);
            var ichigo = new Shinigami("Ichigo", 950, 450, "Zangetsu");

            // Agregar a la arena
            arena.AddFighter(goku);
            arena.AddFighter(naruto);
            arena.AddFighter(ichigo);

            arena.DisplayFighters();

            // Transformaciones específicas
            Console.WriteLine("\n=== TRANSFORMACIONES ===");
            goku.Transform();
            ichigo.ReleaseBankai();
            naruto.RestoreChakra();

            // Batalla usando polymorphism
            arena.StartBattle();

            // Lista polimórfica
            Console.WriteLine("\n=== LISTA POLIMORFICA ===");
            List<Fighter> fighters = new List<Fighter>
            {
                new Saiyan("Vegeta", 1000, 480),
                new Ninja("Sasuke", 880, 420),
                new Shinigami("Byakuya", 920, 440, "Senbonzakura")
            };

            foreach (var fighter in fighters)
            {
                Console.WriteLine($"\n{fighter.Name}:");
                fighter.BasicAttack();
                fighter.SpecialAttack();
            }
        }

        static void TestSOLID()
        {
            Console.WriteLine("\n╔════════════════════════════════════════════════════════╗");
            Console.WriteLine("║  SECCION 6: SOLID PRINCIPLES                           ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════╝\n");

            // Single Responsibility
            Console.WriteLine("=== SINGLE RESPONSIBILITY ===");
            var playlist = new Playlist();
            playlist.AddSong("Opening 1");
            playlist.AddSong("Opening 2");

            var storage = new PlaylistStorage();
            storage.SaveToFile(playlist, "playlist.json");

            var notifier = new PlaylistNotificationService();
            notifier.SendEmail("user@example.com", "Playlist guardada");
            Console.WriteLine();

            // Open/Closed
            Console.WriteLine("=== OPEN/CLOSED ===");
            var catalog = new ContentCatalog();
            catalog.AddContent(new Song
            {
                Title = "Gurenge",
                Artist = "LiSA",
                Genre = "J-Pop",
                DurationMinutes = 4
            });
            catalog.AddContent(new Song
            {
                Title = "Unravel",
                Artist = "TK",
                Genre = "Rock",
                DurationMinutes = 4
            });

            var genreFilter = new GenreFilter("J-Pop");
            var results = catalog.Search(genreFilter);
            Console.WriteLine($"Canciones J-Pop encontradas: {results.Count}");
            Console.WriteLine();

            // Interface Segregation
            Console.WriteLine("=== INTERFACE SEGREGATION ===");
            IAudioPlayer audioPlayer = new SimpleAudioPlayer();
            audioPlayer.PlayAudio();

            IVideoPlayer videoPlayer = new FullVideoPlayer();
            videoPlayer.PlayVideo();
            videoPlayer.AdjustVideoQuality();

            if (videoPlayer is ISubtitleSupport subtitlePlayer)
            {
                subtitlePlayer.ShowSubtitles();
            }
            Console.WriteLine();

            // Dependency Inversion
            Console.WriteLine("=== DEPENDENCY INVERSION ===");

            // Podemos cambiar fácilmente la implementación
            INotificationService emailService = new EmailNotificationService();
            var notifier1 = new EpisodeReleaseNotifier(emailService);
            notifier1.NotifyNewEpisode("Attack on Titan", 75, "user@example.com");

            INotificationService pushService = new PushNotificationService();
            var notifier2 = new EpisodeReleaseNotifier(pushService);
            notifier2.NotifyNewEpisode("Demon Slayer", 26, "user123");

            INotificationService smsService = new SMSNotificationService();
            var notifier3 = new EpisodeReleaseNotifier(smsService);
            notifier3.NotifyNewEpisode("Jujutsu Kaisen", 24, "+1234567890");
        }
    }
}

