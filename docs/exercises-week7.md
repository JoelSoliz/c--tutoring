# EJERCICIOS DE RAZONAMIENTO - SEMANA 7
## Ejercicio 1: Identificar responsabilidades mal ubicadas
Tienes el siguiente código en un Controller de una API de Anime:
```c#
[HttpPost("rate")]
public async Task<IActionResult> RateAnime(RateAnimeDto dto)
{
if (dto.Score < 1 || dto.Score > 10)
return BadRequest("Invalid score");
var anime = await _context.Animes.FindAsync(dto.AnimeId);

if (anime == null)
return NotFound("Anime not found");
var alreadyRated = await _context.Ratings
.AnyAsync(r => r.FanId == dto.FanId && r.AnimeId == dto.AnimeId);
if (alreadyRated)
return Conflict("Already rated");
var rating = new Rating
{
FanId = dto.FanId,
AnimeId = dto.AnimeId,
Score = dto.Score,
CreatedAt = DateTime.UtcNow
};
_context.Ratings.Add(rating);
await _context.SaveChangesAsync();
return Ok(rating);
}
```
### Preguntas:
**1. ¿Qué responsabilidades están mal ubicadas?**

En este controlador, las responsabilidades mal ubicadas son: La validación de reglas de negocio
(score < 1 || score > 10, verificar duplicados) pertenece al Service.
El acceso directo a _context y la construcción del objeto Rating pertenecen al Repository.
El Controller está haciendo todo a la vez, sin aplicar el principio de Separation of Concerns,
manejo de errores y acciones relacionadas con el manejo de datos y lógica de negocio.

**2. ¿Cómo redistribuirías este código entre Controller, Service y Repository?**

Controller:** Recibe el DTO, llama a _ratingService.RateAsync(dto) y captura excepciones
traduciéndolas a HTTP responses.
**Service:** Valida que el score esté entre 1-10, que el anime exista y que no haya un rating
duplicado. Lanza excepciones como NotFoundException o ConflictException en caso contrario.
**Repository:** Expone métodos como para obtener el anime por ID, buscar si existe rating y crear
rating. Nunca construye lógica, solo ejecuta queries.

**3. ¿Qué parte de este código es lógica de negocio y qué parte es lógica HTTP?**

Lógica de negocio son las condiciones como verificar que el score sea válido, que el anime exista,
que el fan no haya calificado antes.

Lógica HTTP es la traducción de esos resultados: return Ok(), return BadRequest(), return
NotFound().

**4. Si mañana necesitas reutilizar la lógica de rating desde un job en background, ¿qué**
    **problema tendrías con este código?**

La lógica de rating está acoplada a la capa HTTP. Un job en background no maneja IActionResult
ni HttpContext, por lo que no podría llamar al Controller directamente. Se tendría que duplicar
toda esa lógica en el job. Si esa lógica estuviera en un Service inyectable, el job podría llamar al
Service a través de su interfaz, sin depender de nada HTTP.

## Ejercicio 2: Elegir arquitectura
Tienes tres proyectos distintos:

- Una API para gestionar Playlists de Bad Bunny con 4 endpoints CRUD.
- Una plataforma de streaming de anime con 50+ endpoints, reglas de negocio complejas,
    múltiples equipos y alta necesidad de testing.
- Una API interna para un equipo de 2 personas que gestiona RomanticMovies con ratings
    y reviews.

### Preguntas:
**1. ¿Qué arquitectura elegirías para cada proyecto?**

API de Playlists de Bad Bunny:** Monolitica Simple
**API de Streaming:** Clean Architecture
**API Romantic Movies:** Layered Architecture

**2. ¿Qué criterios usaste para decidir?**

Para cada una pensé en el tamaño del equipo, complejidad del proyecto, necesidad de testeo,
cantidad de endpoints y lógica de negocio a implementar.
Por ejemplo, para la API de Bad Bunny, decidí elegir una Monolítica Simple debido a la
complejidad del proyecto, al ser únicamente 4 endpoints CRUD, quiere decir que no tiene una
complejidad que justifique el uso de capas.

Para la API de Streaming, decidí una Clean Arquitecture debido a que el proyecto tendrá reglas
de negocio complejas, manejo de varios endpoints y tiene múltiples equipos, las características
de Clean Architecture serán un beneficio para este escenario, ya que, utilizar este tipo de
arquitectura tiene sentido cuando se trabaja con un equipo grande, se requiere una alta
testabilidad y maneja lógica compleja.

Para Romantic Movies, al ser un equipo de 2 personas, una Layered Architecture es más que
suficiente, ya que, solo necesita gestionar un CRUD con lógica adicional como ratings y reviews,
siendo necesario que se pueda separar mejor las responsabilidades para obtener los resultados
de rating y reviews.


**3. ¿En cuál de los tres proyectos Clean Architecture sería sobreingeniería?**

En el proyecto de API de Bad Bunny, ya que, la misma naturaleza de este proyecto es manejar
un CRUD simple, sería sobre ingeniería utilizar Clean Architecture, no tiene lógica compleja y
tampoco un equipo grande.
También lo sería en Romantic Movies, donde Layered Architecture ya es suficiente y agregar
Clean Architecture añadiría complejidad innecesaria para un equipo de 2 personas.

### Ejercicio 3: Detectar Captive Dependency
```c#
// Singleton
public class AnimeRecommendationEngine
{
private readonly IAnimeRepository _repository; // Scoped
public AnimeRecommendationEngine(IAnimeRepository repository)
{
_repository = repository;
}
}
```
### **Preguntas:**

**1. ¿Qué problema tiene este código?**

El problema de Captive Dependency, significa una inyección de un servicio Scoped
dentro de un Singleton. En este caso, la clase AnimeRecommendationEngine es
Singleton, el cual está inyectando un repositorio scoped.

**2. ¿Qué podría pasar en producción con múltiples requests simultáneos?**

En producción podría suceder que la aplicación colapse por la cantidad de requests,
degrade el rendimiento y provocando cuellos de botella innecesarios. Como el Singleton
vive toda la vida de la aplicación, ese repositorio nunca se libera ni se renueva entre
requests. Esto puede causar que múltiples requests simultáneos compartan el mismo
DbContext, provocando errores de tracking de EF Core o datos incorrectos.

**3. ¿Cómo lo resolverías?**

Dependiendo del contexto, si el Engine no necesita ser Singleton, lo más adecuado sería
cambiar su lifetime a Scoped.

## Ejercicio 4: Diseñar capas
Diseña la estructura de capas para una API que gestiona:
- Songs de Bad Bunny con Artists y Albums.
- Playlists que contienen Songs.
- Fans que pueden seguir Artists y crear Playlists.

### Preguntas:
**1. ¿Qué Controllers necesitas?**

Necesitamos Controllers para: Artists, Songs, Albums, Playlists, Fans.

**2. ¿Qué Services necesitas?**

NecesitamosServicios para: Artists, Songs, Albums, Playlists, Fans. Ademas de otro que
sea FansArtirts, FansPlaylists, PlaylistsSongs.

**3. ¿Qué Repositories necesitas?**

Necesitamos Repositories para:
Artists, Songs, Albums, Playlists, Fans. Ademas de otro que sea FansArtirts,
FansPlaylists, PlaylistsSongs.

**4. ¿Qué reglas de negocio irían en el Service?**

Reglas de negocio que deberian ir en el service: Validar que los artists, albums, songs,
etc que desean encontrarse por ID existan. Validar que para crear una playlists se
necesita minimamente una cancion. Validar que la duración de las canciones sea más
de 30 segundos y no crear canciones que no tengan duración.

Otras validaciones podrían ser: Un fan no puede seguir al mismo Artist dos veces, no
pueden existir canciones repetidas en una Playlist, un album debe pertenecer a un solo
Artist.

**5. ¿Qué validaciones irían en el Controller?**

Validaciones como formato de entrada de los datos, si todos los campos se están
mandando y con el formato correcto. Por ejemplo, que los ID tengan un GUID valido, que
los campos obligatorios del DTO esten presentes, entre otros.

## Ejercicio 5: Evaluar sobreingeniería
Un desarrollador propone la siguiente estructura para una API con 3 entidades:
```
MyApi.Domain/
MyApi.Application/
MyApi.Infrastructure/
MyApi.API/
MyApi.Shared/
MyApi.Tests.Unit/
MyApi.Tests.Integration/
```

Con:
- Repositorios genéricos y específicos.
- CQRS con MediatR.
- Domain Events.
- Unit of Work explícito.

### **Preguntas:**

**1. ¿Es esto sobreingeniería? ¿Por qué?**

Si, en algunos aspectos lo es, ya que, para manejar 3 entidades utilizar CQRS o Domain
Events es sobreingeniería, para 3 entidades sin lógica compleja, estos aspectos no son
necesarios.

**2. ¿Qué partes conservarías y cuáles eliminarías?**

Conservaría la separación por capas, repositorios específicos, tests unitarios y de
integración, ya que, al manejar varias entidades podría ayudarnos a mejorar la calidad
del código y tener una API más organizada.

Eliminaría CQRS, Domain Events, Unit of Work explícito, ya que, EF Core lo tiene
integrado, no tenemos un flujo complejo. Respecto a los repositorios genéricos, podría
ser discutible, dependiendo que tanta lógica CRUD se repite entre entidades. Sin
embargo, al ser tres entidades podríamos prescindir de este aspecto.

**3. ¿Cuándo esta estructura sí tendría sentido?**

Cuando tenemos un sistema con múltiples entidades, flujo de negocio complejo, lógica
de negocio compleja, manejo de eventos, dependencia con muchos sistemas externos.
Cuando tenemos un proyecto mediano a grande.

Por ejemplo, una plataforma de e-commerce con inventario, pagos, notificaciones y
múltiples equipos trabajando en paralelo, donde los Domain Events permiten desacoplar
los efectos secundarios y CQRS permite escalar lecturas y escrituras por separado.


