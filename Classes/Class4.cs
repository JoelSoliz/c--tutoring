using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Classes.Class4
{
    internal class Class4
    {
        static async Task Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Banner("Clase 4 Demos: Generics, Exceptions, async/await, Thread vs Task");

            //GenericsDemo.Run();
            //ExceptionsDemo.Run();

            //// async demos
            //await AsyncAwaitDemo.RunAsync();

            //// Concurrency vs Parallelism + CPU-bound vs I/O-bound
            await ConcurrencyParallelismDemo.RunAsync();

            Banner("FIN");
        }

        static void Banner(string title)
        {
            Console.WriteLine();
            Console.WriteLine(new string('=', title.Length));
            Console.WriteLine(title);
            Console.WriteLine(new string('=', title.Length));
        }
    }

    // ============================================================
    // 1) GENERICS
    // ============================================================
    static class GenericsDemo
    {
        public static void Run()
        {
            Console.WriteLine("\n--- GENERICS DEMO ---");

            // Por qué: type-safety + performance + expresividad.
            // Comparación rápida: List<object> vs List<T>
            var bag = new List<object> { "Bad Bunny", 2026, 4.8 };
            Console.WriteLine($"List<object> permite mezclar: {string.Join(", ", bag)}");
            // Problema: al leer, necesitas cast y puedes fallar runtime.
            var songs = new List<Song>
            {
                new Song("DtMF", "Bad Bunny", 210, 1_000_000),
                new Song("As It Was", "Harry Styles", 167, 1_900_000),
            };

            // Sin casts, el compiler sabe el type.
            foreach (var song in songs)
            {
                Console.WriteLine($"Song: {song.Title} - {song.Artist} ({song.Seconds}s)");
            }

            // Generic method: reutilizable + type inference
            var top = MaxBy(songs, song => song.Seconds);
            Console.WriteLine($"MaxBy duration => {top.Title} ({top.Seconds}s)");

            // Generic constraint: where T : class / new() / IComparable<T>, etc.
            var cache = new SimpleCache<string, Song>();
            cache.Set("bb:dtmf", songs[0]);
            Console.WriteLine($"Cache hit => {cache.Get("bb:dtmf").Title}");

            // Covariance/Contravariance (concepto clave en interfaces/delegates)
            // IEnumerable<Derived> es asignable a IEnumerable<Base> (covariance).
            IEnumerable<RomanticMovie> romanticMovies = new List<RomanticMovie>
            {
                new RomanticMovie("Before Sunrise", 1995),
                new RomanticMovie("La La Land", 2016),
            };
            IEnumerable<Movie> movies = romanticMovies; // OK: covariance
            var countRM = Count(romanticMovies);
            var countM = Count(movies);
            Console.WriteLine($"Covariance IEnumerable<Movie> count => {Count(movies)}");
        }

        // Generic method con selector
        static T MaxBy<T, TKey>(IEnumerable<T> items, Func<T, TKey> keySelector)
          where TKey : IComparable<TKey>
        {
            using var it = items.GetEnumerator();
            if (!it.MoveNext()) throw new InvalidOperationException("Sequence empty.");

            T best = it.Current;
            TKey bestKey = keySelector(best);

            while (it.MoveNext())
            {
                var candidate = it.Current;
                var candidateKey = keySelector(candidate);
                if (candidateKey.CompareTo(bestKey) > 0)
                {
                    best = candidate;
                    bestKey = candidateKey;
                }
            }
            return best;
        }

        static int Count<T>(IEnumerable<T> items)
            where T : Movie
        {
            int count = 0;
            foreach (var _ in items) count++;
            return count;
        }
    }

    record Song(string Title, string Artist, int Seconds, int Listeners);
    record Movie(string Title, int Year, string Genre);
    record RomanticMovie(string Title, int Year) : Movie(Title, Year, "Romance");

    // Generic type: cache simple
    sealed class SimpleCache<TKey, TValue> where TKey : notnull
    {
        private readonly Dictionary<TKey, TValue> _dict = new();

        public void Set(TKey key, TValue value) => _dict[key] = value;

        public TValue Get(TKey key)
        {
            if (_dict.TryGetValue(key, out var value)) return value;
            throw new KeyNotFoundException($"Key not found: {key}");
        }
    }

    // ============================================================
    // 2) EXCEPTIONS + try/catch/finally
    // ============================================================
    static class ExceptionsDemo
    {
        public static void Run()
        {
            Console.WriteLine("\n--- EXCEPTIONS DEMO ---");

            // Idea clave: Exceptions son para exceptional flow, no para lógica normal.
            // Also: throw pierde stack trace si haces "throw ex;" (mal).
            try
            {
                Console.WriteLine("Parseando rating...");
                var rating = ParseRating("11"); // inválido
                Console.WriteLine($"Rating => {rating}");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"[catch ArgumentOutOfRangeException] {ex.Message}");
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"[catch FormatException] {ex.Message}");
            }
            finally
            {
                // finally: limpieza garantizada (dispose, release locks, logs, etc.)
                Console.WriteLine("[finally] Cleanup garantizado.");
            }

            // Exception filters (útil en backend: distinguir casos sin duplicar catch)
            try
            {
                ThrowHttpLike(430);
            }
            catch (HttpRequestException ex) when ((int)ex.StatusCode! == 429)
            {
                Console.WriteLine("[filter] Rate limited: manejar retry/backoff.");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"[filter] Otro error HTTP: {ex.StatusCode}");
            }

            // using => deterministic disposal (equivalente a try/finally con Dispose)
            using var timer = new DemoDisposable("RomComTimer");
            
            //DemoDisposable timer2 = new DemoDisposable("RomComTimer");

            //try
            //{
            //    var name = timer2.ToString();
            //}
            //finally
            //{
            //    timer2.Dispose();
            //}

            Console.WriteLine("Dentro del using...");
        }

        static int ParseRating(string text)
        {
            // Parse + validar
            if (!int.TryParse(text, out var rating))
                throw new FormatException("Rating no es un número.");

            if (rating < 0 || rating > 10)
                throw new ArgumentOutOfRangeException(nameof(text), "Rating debe estar entre 0 y 10.");

            return rating;
        }

        static void ThrowHttpLike(int statusCode)
        {
            throw new HttpRequestException("HTTP error", null, (System.Net.HttpStatusCode)statusCode);
        }
    }

    sealed class DemoDisposable : IDisposable
    {
        private readonly string _name;
        public DemoDisposable(string name)
        {
            _name = name;
            Console.WriteLine($"[DisposeDemo] {_name} acquired");
        }

        public void Dispose()
        {
            Console.WriteLine($"[DisposeDemo] {_name} disposed");
        }
    }

    // ============================================================
    // 3) async/await (non-blocking flow)
    // ============================================================
    static class AsyncAwaitDemo
    {
        // Reutiliza HttpClient: best practice
        private static readonly HttpClient Http = new HttpClient();

        public static async Task RunAsync()
        {
            Console.WriteLine("\n--- ASYNC/AWAIT DEMO ---");

            // (A) Non-blocking: mientras esperas I/O, el thread se libera.
            Console.WriteLine("Simulando I/O (Task.Delay)...");
            var sw = Stopwatch.StartNew();
            await Task.Delay(400); // I/O-like wait
            sw.Stop();
            Console.WriteLine($"Delay awaited in {sw.ElapsedMilliseconds}ms (sin bloquear CPU).");

            // (B) Composición: Task.WhenAll para concurrency (I/O-bound)
            Console.WriteLine("Concurrency I/O-like con WhenAll...");
            var t1 = FakeDownloadAsync("Episode 1", 350);
            var t2 = FakeDownloadAsync("Episode 2", 250);
            var t3 = FakeDownloadAsync("Episode 3", 450);

            var results = await Task.WhenAll(t1, t2, t3);
            Console.WriteLine($"Descargas completadas: {string.Join(" | ", results)}");

            // (C) Errores en async: se propagan al await
            try
            {
                await FailsAsync();
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Error async capturado en await: {ex.Message}");
            }

            // (D) CancellationToken (base para timeouts/cancelación)
            using var cts = new CancellationTokenSource(300);
            try
            {
                await CancellableWorkAsync(cts.Token);
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Operación cancelada por timeout.");
            }
        }

        static async Task<string> FakeDownloadAsync(string name, int ms)
        {
            await Task.Delay(ms);
            return $"{name} ok({ms}ms)";
        }

        static async Task FailsAsync()
        {
            await Task.Delay(100);
            throw new InvalidOperationException("Algo falló mientras cargabas la playlist.");
        }

        static async Task CancellableWorkAsync(CancellationToken ct)
        {
            // Nota: pasa el token a APIs que lo acepten.
            for (int i = 1; i <= 10; i++)
            {
                ct.ThrowIfCancellationRequested();
                await Task.Delay(80, ct);
                Console.WriteLine($"Trabajo cancelable tick {i}/10");
            }
        }
    }

    // ============================================================
    // 4) CPU-bound vs I/O-bound + Concurrency vs Parallelism
    //    Thread vs Task (práctico)
    // ============================================================
    static class ConcurrencyParallelismDemo
    {
        public static async Task RunAsync()
        {
            Console.WriteLine("\n--- CPU-bound vs I/O-bound + Concurrency vs Parallelism ---");

            // I/O-bound: usar async/await para no bloquear threads
            var ioSw = Stopwatch.StartNew();
            await SimulateIoBoundAsync();
            ioSw.Stop();
            Console.WriteLine($"I/O-bound sim => {ioSw.ElapsedMilliseconds}ms");

            // CPU-bound: async no ayuda por sí solo.
            // Para no bloquear el main thread (UI/server context), offload con Task.Run.
            var cpuSw = Stopwatch.StartNew();
            var cpuResult = await Task.Run(() => CpuBoundWork(35)); // ejemplo: Fibonacci
            cpuSw.Stop();
            Console.WriteLine($"CPU-bound (Task.Run) => result {cpuResult}, {cpuSw.ElapsedMilliseconds}ms");

            // Parallelism: ejecutar CPU work en paralelo (ojo: overhead)
            Console.WriteLine("Parallel CPU work (WhenAll + Task.Run)...");
            var pSw = Stopwatch.StartNew();
            var tasks = new[]
            {
                Task.Run(() => CpuBoundWork(35)),
                Task.Run(() => CpuBoundWork(35)),
                Task.Run(() => CpuBoundWork(35)),
            };
            var values = await Task.WhenAll(tasks);
            pSw.Stop();
            Console.WriteLine($"Parallel results sum={Sum(values)}, {pSw.ElapsedMilliseconds}ms");

            // Thread vs Task: Thread es low-level, Task es high-level (scheduler, continuations)
            Console.WriteLine("\nThread vs Task quick demo:");
            var thread = new Thread(() =>
            {
                Console.WriteLine($"[Thread] id={Environment.CurrentManagedThreadId} trabajando...");
                Thread.Sleep(150);
                Console.WriteLine("[Thread] done");
            });
            thread.Start();
            thread.Join();

            await Task.Run(() =>
            {
                Console.WriteLine($"[Task] id={Environment.CurrentManagedThreadId} trabajando...");
                Thread.Sleep(150);
                Console.WriteLine("[Task] done");
            });
        }

        static async Task SimulateIoBoundAsync()
        {
            // Concurrency: se solapan waits (no “más CPU”, sino mejor utilización del tiempo)
            var a = Task.Delay(250);
            var b = Task.Delay(250);
            var c = Task.Delay(250);
            await Task.WhenAll(a, b, c);
        }

        static long CpuBoundWork(int n)
        {
            // Fibonacci recursivo a propósito: CPU-heavy (demo)
            if (n <= 1) return n;
            return CpuBoundWork(n - 1) + CpuBoundWork(n - 2);
        }

        static long Sum(long[] xs)
        {
            long s = 0;
            foreach (var x in xs) s += x;
            return s;
        }
    }
}