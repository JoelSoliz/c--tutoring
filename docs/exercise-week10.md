# EJERCICIOS DE RAZONAMIENTO - SEMANA 10
## Ejercicio 1: Identificar el problema
Analiza este código:
```
[HttpGet("dashboard")]
public IActionResult GetDashboard()
{
var topAnimes = _animeService.GetTopRatedAsync().Result;
var trending = _playlistService.GetTrendingAsync().Result;
var newMovies = _movieService.GetNewReleasesAsync().Result;
return Ok(new { topAnimes, trending, newMovies });
}
```
### Preguntas:
**1. ¿Cuántos problemas de performance puedes identificar?**
Puedo identificar problemas de deadlock, ya que, estamos dentro de un método síncrono que llama métodos asíncronos.
Al utilizar .Result, tenemos los thread bloquedos, además, al ejecutarse los métodos secuencialmente, aumentamos la latencia.
Finalmente, no existe el uso de CancellationToken, si el usuario cierra el browser, se ejecutarían las tres queries innecesariamente.

**2. ¿Hay riesgo de deadlock? ¿En qué contexto?**
Si, existe riesgo de deadlock, porque estamos llamando al método Result, al ser ASP .NET Core, o existe Synchronization Context, sin embargo,
sigue existiendo riesgo de deadlock, con locks síncronos en código async.

**3. ¿Cómo lo reescribirías para máximo performance?**
```
[HttpGet("dashboard")]
public async Task<IActionResult> GetDashboard(CancellationToken ct)
{
    var topAnimesTask = _animeService.GetTopRatedAsync(ct);
    var trendingTask = _playlistService.GetTrendingAsync(ct);
    var newMoviesTask = _movieService.GetNewReleasesAsync(ct);

    await Task.WhenAll(topAnimesTask, trendingTask, newMoviesTask);

    return Ok(new {
        topAnimes = topAnimesTask.Result,
        trending = trendingTask.Result,
        newMovies = newMoviesTask.Result
    });
}
```
**4. ¿Qué pasa si una de las tres operaciones falla?**
Las operaciones continuan, especialmente si usamos Task.WhenAll(), espera a que todas se completen, 
esto quiere decir que a pesar que una se complete con un resultado de falla, de todas maneras las otras tareas se completan.

## Ejercicio 2: Diseñar el caching
**Tienes un endpoint GET /api/anime/top-rated que:
Se llama 10,000 veces por minuto.
Hace una query compleja que tarda 200ms.
Los datos cambian máximo una vez por hora.**

### Preguntas:

**1. ¿Qué estrategia de caching usarías?**

Usaría una estrategia de caching con Redis, ya que al llamarse múltiples veces, necesitamos guardar resultados que ya fueron consultados.
Esto corresponde a Cache-Aside (o lazy loading), primero buscamos en cache, si no está vamos a la DB y guardamos el resultado.

**2. ¿Cuánto tiempo de TTL configurarías?**

30 minutos, esto para maximizar los cache hits. Con 10,000 llamadas y 200ms de query, cada cache miss es costoso.

**3. ¿Cómo invalidarías el cache cuando los datos cambian?**

Podemos invalidarlo por el key del cache, al cambiar los datos, el key puede ser por ID, algo como anime:top-rated, una sola entrada de cache.

**4. ¿Qué pasa si tienes 5 instancias del servidor?**

Si usamos Cache Aside, entre instancias se perdería el estado, ya que con este approach no hay una manera de centralizar el estado entre instancias,
entonces, la solución sería utilizar caché distribuido con Redis para compartir estado entre instancias.

**5. ¿Usarías IMemoryCache o IDistributedCache? ¿Por qué?**

Si tuviera varias instancias de servidor, usaría IDistributed Cache porque de esta manera centralizamos el estado y las instancias compartirían la misma
información, haciéndolo más consistente, ya que, usando IMemoryCache podría tener 5 versiones distintas de top-rated al mismo tiempo, lo que genera inconsistencia.

## Ejercicio 3: I/O vs CPU
Clasifica cada operación y di cómo la implementarías:

**1. Consultar los últimos 100 episodios de un anime de la DB.**

Esto es una operación I/O bound, ya que, esperamos la respuesta de MySQL, entonces lo implementaría utilizando un
método asíncrono en el repositorio.

**2. Generar un PDF con el historial de reproducciones de un fan.**

Esto es una operación CPU bound, tendríamos que usar Task.Run().

**3. Verificar si un email ya existe en la DB.**

Esto es una operación I/O Bound, ya que, realizamos queries a la DB, entonces de la misma manera, tendríamos que implementarlo mediante un
método asíncrono en el repositorio que consulte si el email específico existe.

**4. Comprimir 500 imágenes de portadas de anime.**

Esto es una operación CPU Bound, porque necesitamos comprimir imáagenes, lo cual requiere procesamiento en memoria, de esta manera para implementarlo
usaría Parallel.ForEachAsync con MaxDegreeOfParallelism para procesar varias en paralelo sin saturar el CPU.

**5. Llamar a la API de Spotify para obtener metadata de canciones.**

Esto es una operación I/O bound, lo implementaría mediante async/await directo.

**6. Calcular recomendaciones con un modelo de ML en memoria.**

Al ser en memoria, realiza cómputo intensivo, siendo CPU bound, lo impelementaría mediante Task.Run.

**7. Leer un archivo de configuración del disco.**

Es I/O bound, ya que al leer de disco, estamos esperando un recurso externo, entonces se implementaría mediante async/await.

**8. Encriptar los datos de una playlist antes de exportarla.**
CPU bound, lo implementaría usando Task.Run.

## Ejercicio 4: Optimizar una query
```
public async Task<List<FanDto>> GetActiveFansAsync()
{
var fans = await _context.Fans
.Include(f => f.Playlists)
.ThenInclude(p => p.Songs)
.Include(f => f.WatchedAnimes)
.ToListAsync();
return fans
.Where(f => f.Playlists.Count > 0 || f.WatchedAnimes.Count > 0)
.Select(f => new FanDto
{
Id = f.Id,
Username = f.Username,
Email = f.Email
})
.ToList();
}
```

### Preguntas:
**1. ¿Cuántos problemas de performance tiene este código?**

Existe varios problemas de performance, como varias queries para obtener los fans activos.
Empezando por el Where que se ejecuta directamente en memoria, se estan trayendo campos innesarios, 
hay tracking innecesario, al ser de solo lectura no necesitamos tracking y no existe un límite de fans a retornar
retornar todos.

**2. ¿Qué datos está trayendo de la DB que no necesita?**

Esta traendo playlists, songs y watchedanimes, además esta trauendo todos los campos de fan,
cuando solo necesita el id, username e email.

**3. ¿Cómo reescribirías la query para que sea eficiente?**

Primero, se tiene que mover el .Where antes del ToListAsync, luego reemplazar los Include con .Any() dentro del .Where
Agregar .Select con solo los campos necesarios, agregar AsNoTracking, finalmente agregar Skip/Take.

**4. ¿Agregarías AsNoTracking? ¿Por qué?**

Sí, porque estamos realizando queries de lectura, no de escritura, por ende no necesitamos tracking para esta querie.


**5. ¿Cómo agregarías paginación?**

Lo agregaría mediante el método Take, el cual recibirá el valor mediante los atributos de entrada del método, que indicará cuántos elementos 
devolver o por default 10 y utilizar Skip.

