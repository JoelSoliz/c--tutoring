# EJERCICIOS DE RAZONAMIENTO

## **Ejercicio 1: Diseñar Policies**

**Tienes una API de películas románticas con estas reglas de negocio:**

- **Un Critic puede publicar reseñas solo si tiene más de 50 reseñas previas.**
- **Un Fan puede ver contenido premium si tiene suscripción activa.**
- **Solo el autor de una reseña o un Admin puede eliminarla.**
- **Un Moderator puede editar cualquier reseña, pero no eliminarla.**

### Preguntas:

**1. ¿Cuáles de estas reglas usarían Roles simples?**

Las regla número cuatro usaría un Rol simple, porque solamente necesitamos verificar si el usuario que está realizando el request es Moderator, de esta manera restringimos el endpoint de DELETE para este rol.

**2. ¿Cuáles necesitan Policies con Requirements personalizados?**

Las reglas 1, 2 y 3, porque son condiciones muy específicas. Por ejemplo, además del rol Critic, necesitamos saber la cantidad de resenias de este Rol, tenemos casos similares en Fan premium y el autor de una resenia, ya que estamos hablando de ownership.

**3. ¿Qué claims incluirías en el JWT para soportar estas reglas?**
Incluiría los siguientes claims:
isPremiumUser, rol, userId, reviewCount, username

**4. Diseña los Requirements y Handlers necesarios.**
A) **Requirements**

- PremiumSubscriptionRequirement(Si el usuario es Premium)

- MinimumReviewsRequirement(50) (Un Critic debe tener > a 50 reviews)

- ReviewOwnershipRequirement (Determinar Admin o duenio de la review esta realizando el request)

B) **Hadlers**

- **PremiumSubscriptionHandler:** Del token recibido en la request obetener el claim "isPremium", si es verdadero permite realizar la request.

- **MinimumReviewsHandler** Obtener el rol y cantidad de reviews del token recibido y poder validar que reviewCount >= 50.

- ReviewOwnershipHandler: Obtener el rol y userId del Token, si el userId del request coincide con el duenio del review permite la request o bypass si es Admin.

**5. ¿Cómo registrarías todo en Program.cs?**
Primero registraría los Handlers como Scoped, seguidamente registraría Policies para cada requirements incluyendo el manejo de rol de la regla 4.

## Ejercicio 2: Analizar un Middleware

Analiza el siguiente Middleware:

```c#
public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;
    private const string ApiKey = "super-secret-key-123";
    public ApiKeyMiddleware(RequestDelegate next) => _next = next;
    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.Headers.TryGetValue("X-Api-Key", out var key))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("API Key missing");
            return;
        }

        if (key != ApiKey)
        {
            context.Response.StatusCode = 403;
            await context.Response.WriteAsync("Invalid API Key");
            return;
        }
        await _next(context);
    }
}
```

### Preguntas:

1. **¿Cuántos problemas de seguridad puedes identificar?**

Puedo identificar varios problemas de seguridad como la exposición del valor de API Key que está hardcodeado. Se imprime en consola qué valor falta explícitamente en caso de error, siendo una exposición de información importante para posibles atacantes.
Finalmente, no hay registro de intentos fallidos, siendo imposible detectar ataques de fuerza bruta y no existe un rate limiting configurado, esto hace que un atacante pueda probar millones de API Keys sin límite.

2. ¿**Qué problema tiene el código HTTP 401 vs 403 en este contexto?**

Ambos están siendo usados incorrectamente, 401 es para indicar que el endpoint necesita autenticación y 403 que el usuario no tiene permiso de acceder a un recurso. En el código ambos son utilizados para reflejar mensajes de error frente a la ausencia de APi key en headers o en API, lo cual no es afine al contexto verdadero del código 403. En este caso, solo debería de retornar 401, porque el problema es de autenticación, no de autorización.

3. **¿Cómo mejorarías el manejo de errores?**

Empezaría implementando un middleware de manejo de excepcion global, y reflejar en los mensajes únicamente el mensaje "Invalid API information" o indicar que algo está perdido pero no indicar el qué. Además de retornar el status code correcto que es el 401, junto a un JSON estandarizado como un dto para respuesta de errores.

4. **¿Dónde debería vivir la API Key en producción?**

Debería vivir en variables de entorno del sistema operativo o en un manager de secretos que ofrezca la nube.

5. **¿Cómo agregarías logging a este Middleware?**
   Utilizaría Serilog. Empezando por inyectar la interfáz correspondiente en el constructor y reflejar un log de Warning cuando falta la API Key o es inválida, incluyendo un LogInformation en caso que la autenticación sea exitosa.

## Ejercicio 3: Diseñar el sistema de logging

**Diseña la estrategia de logging para una API de playlists de Bad Bunny con estos requisitos:**

- **En desarrollo: todos los logs en consola, incluyendo queries SQL.**
- **En producción: solo Information+ en archivo, Warning+ en un servicio externo.**
- **Nunca loggear el contenido de los mensajes privados entre fans.**
- **Poder rastrear todas las acciones de un fan específico.**
- **Alertar cuando una playlist tenga más de 1000 reproducciones en 1 hora.**

## Preguntas:

**1. ¿Qué sinks de Serilog usarías?**

Usaría sinks de tipo: Consola, archivo y Seq o ElasticSearch, porque tenemos que guardar en desarrollo los logs en consola, luego en producción en un archivo y en un servicio externo. 

**2. ¿Cómo configurarías los niveles por ambiente?**

Configuraría Verbose/Debug e Information para desarrollo que incluya queries SQL.
Para Producción Information level para guardar en un archivo, Warning en Seq y silenciar las queries de SQL.


**3. ¿Qué claims del JWT incluirías en el contexto de cada log?**

Incluiría claims como userId, username, role.

**4. ¿Cómo implementarías la alerta de reproducciones?**

En el Service, al registrar una reproducción, consultaría cuántas reproducciones tuvo esa playlist en la última hora.
Si supera 1000, lanzar un log tipo Warning con el nombre, Id y conteo de la playlist.Además de considerar el uso de caché para no consultar en cada reproducción a la DB. 

**5. ¿Qué información incluirías en cada log de acción de usuario?**
Incluiría: userId, userName, action, traceId, resourceId, ip y timestamp.

## Ejercicio 4: Trazar el flujo de una request
**Una request llega a DELETE /api/playlists/abc-123 con un JWT válido de un Fan (no
Admin).**

**La playlist abc-123 pertenece a otro Fan.**

### Traza paso a paso:

**1. ¿Qué Middleware se ejecuta y en qué orden?**
Se ejecuta el middleware de logger(RequestLoggingMiddleware
), seguidamente revisa la seguridad de la request con SecurityHeadersMiddleware, luego si excedió el límite de request con RateLimitMiddleware
, luego cualquier error si ocurre de la request (en este caso, capturaría un 403, sería ExceptionMiddleware), identifica al usuario con UseAuthentication, verifica permisos con UseAuthorization y ejecuta el controller. 

**2. ¿Qué loggea cada Middleware?**

**RequestLoggingMiddleware:** loggea al final de todo, método, path, statusCode, tiempo, userId, IP.

**ExceptionMiddleware:** loggea el error con errorCode, path, mensaje y traceId.

Los demás no loguean nada por sí solos. 

**3. ¿Dónde falla la autorización?**

Falla en el controller, al llamar a su método correspondiente de autorización el controller lanza un ForbiddenException.

**4. ¿Qué código HTTP retorna y por qué?**

Retorna un 403 porque el Fan no es un Admin, por ende no puede borrar la playlist y además no es el dueño de esta playlist abc-123. 

**5. ¿Qué loggea el ExceptionMiddleware?**

Loggea el statusCode, el error, path, traceId. Este resultado llega al capturar la excepción y se loguea como Warning. 

**6. ¿Qué ve el cliente en la respuesta?**

El cliente ve el JSON definido en el DTO, en este caso el JSON con: statusCode, errorCorde, message, traceId y timestamp.