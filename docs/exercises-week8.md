# EJERCICIOS DE RAZONAMIENTO - SEMANA 8

## Ejercicio 1: Identificar vulnerabilidades

```c#
[HttpPost("login")]
public async Task<IActionResult> Login(LoginDto dto)
{
var fan = await _context.Fans
.FirstOrDefaultAsync(f => f.Email == dto.Email);
if (fan == null)
return NotFound("Email not found");
if (fan.Password == dto.Password)
return Ok(new { token = GenerateToken(fan) });
return BadRequest("Wrong password");
}
```

### Preguntas

**1. ¿Cuántas vulnerabilidades de seguridad puedes identificar?**
Puedo encontrar varias vulnerabilidades, la primera consiste en la exposición de información acerca de qué campo del login está incorrecto, el password no parece indicar que está siendo hasheado. Además, la generación de tokens se está realizando dentro del mismo controller.

**2. ¿Qué información está revelando innecesariamente?**
Esta revelando información sobre qué credenciales están siendo ingresadas incorrectamente. Si el email no existe devuelve NotFound, y si el password es incorrecto devuelve BadRequest. Eso le dice al atacante exactamente qué campo explotar.

**3. ¿Qué problema tiene el manejo del password?**
Tiene el problema que no esta hasheado y se está comparando directamente del request si coincide con el valor guardado en la base de datos, lo cual en autenticación no es una buena práctica debido a que estamos guardando los valores crudos en la DB, sin hasheo, que es una capa de seguridad requerida.

**4. ¿Qué capas arquitectónicas están siendo violadas?**
Las capas de repositorio porque se accede directamente a DB Context y la capa de servicios ya que se está manejando lógica de negocio dentro del controlador, como la validación del password y generación de Tokens.

**5. Reescribe el flujo correcto conceptualmente.**

1. El Controller recibe el LoginDto y llama a \_authService.Login(dto)
2. El AuthService busca al fan por email via repository
3. Si no existe, lanza UnauthorizedException, sin exponer el campo específico que está siendo mal ingresado.
4. Si existe, usa PasswordHasher.VerifyHashedPassword() para comparar.
5. Si es correcto, llama a \_jwtTokenService.GenerateToken(fan).
6. Retorna el token al Controller, que responde con Ok(result).

## Ejercicio 2: Diseñar autorización

Tienes una API de Anime con los siguientes roles:

Guest: no autenticado.

Fan: usuario registrado.

PremiumFan: fan con suscripción activa.

Moderator: puede gestionar contenido.

Admin: acceso total.

Y los siguientes endpoints:
GET /animes: lista pública.

GET /animes/{id}/episodes: requiere ser Fan.

GET /animes/{id}/episodes/{id}/stream: requiere PremiumFan.

POST /animes: requiere Moderator o Admin.

DELETE /animes/{id}: requiere Admin.

POST /playlists: requiere Fan.

DELETE /playlists/{id}: requiere ser el dueño (de la playlist) o Admin.

### Preguntas:

**1. ¿Cuáles endpoints usarían [AllowAnonymous]?**

Solo el endpoint de GET /animes, ya que es un endpoint público que no requiere autenticación.

**2. ¿Cuáles usarían [Authorize(Roles = ...)]?**

Los endpoints GET /animes/{id}/episodes, GET /animes/{id}/episodes/{id}/stream, DELETE /animes/{id}, POST /animes, POST /playlists, todos estos endpoints requieren de un rol específico, los cuales son: Fan, Admin y Moderator.

**3. ¿Cuáles necesitarían una Policy personalizada?**

Los endpoints que necesitan policies son: GET /animes/{id}/episodes/{id}/stream, porque isPremium es un claim que determina si un fan tiene una suscripción activa y DELETE /playlists/{id}, porque no existe una manera mediante roles que logre expresar dueño o Admin.

**4. ¿Cómo implementarías la regla “ser el dueño o Admin”?**

1. Obtener el fanId mediante el claim **sub** del token.
2. Verificar en el service si la playlist pertenece a este fan.
3. Si no le pertenece, verificar si el **role** claim pertenece a un Admin.
4. Si ninguna de estas condiciones se cumple, lanzamos un error 403.

**5. ¿Qué claims incluirías en el JWT para soportar todo esto?**

Claims que incluiría: sub, exp, isPremium, role.

## Ejercicio 3: Analizar un JWT

Dado el siguiente payload de un JWT:

```
{
"sub": "fan-xyz-789",
"email": "badbunny@music.com",
"role": "Fan",
"creditCard": "4111-1111-1111-1111",
"password": "mypassword123",
"isPremium": false,
"exp": 9999999999
}
```

### Preguntas:

**1. ¿Qué problemas de seguridad tiene este payload?**

Dentro del payload se expone el número de tarjeta de crédito y password del usuario, siendo inseguro porque un payload es fácil de decodificar, exponiendo información sensible de los usuarios ante cualquier atacante.

**2. ¿Qué claims son apropiados y cuáles no?**

Claims apropiados: sub, role, isPremium, exp.

Claims que no son necesarios: creditCard, password, email (este último dependería de la necesidad del sistema, pero en general no es necesario)

**3. ¿Qué implica un exp tan lejano en el tiempo?**

Implica que este token nunca vencería, convirtiéndolo en una vulnerabilidad, ya que, si el token fuera robado, el atacante tendría acceso prácticamente para siempre sin ninguna posibilidad de mitigarlo, ya que los JWTs no se pueden invalidar antes de su expiración

**4. ¿Cómo lo corregirías?**

1. Solamente tomaría en cuenta como claims: sub, role, isPremium, exp, jti, iat.

2. El exp tendría que estar dentro de un intervale de 15 a 30 minutos.

## Ejercicio 4: Ownership vs Role

Un Fan autenticado con id fan-abc-123 hace esta request:

```
DELETE /api/playlists/playlist-xyz-999
Authorization: Bearer <token valido con role Fan>
```

La playlist playlist-xyz-999 pertenece al Fan con id fan-def-456.

### Preguntas:

**1. ¿Debería permitirse esta operación?**

No debería permitirse porque la playlist no pertenece al usuario que está intentando realizar la request.

**2. ¿Qué código HTTP debería retornarse si se rechaza?**

Un código HTTP 403, ya que no está autorizado para realizar esa operación de DELETE de una playlist que no es suya.

**3. ¿Dónde implementarías la verificación de ownership: Controller, Service o Repository?**

En el service, porque accedemos al token y podemos verificar que el id del Fan es el del owner o no.

**4. ¿Cómo obtendrías el id del fan autenticado en el Service?**

El Controller extrae el claim **sub** del token y lo pasa como parámetro al Service, dentro del service solo recibimos como parámetro el fanId y realizamos la validación de ownership.

## Ejercicio 5: Diseñar el flujo completo

Diseña conceptualmente el flujo completo de autenticación para una API de películas románticas con las siguientes reglas:

- Los usuarios se registran con email y password.

- El login retorna un Access Token (30 min) y un Refresh Token (7 días).

- Los Refresh Tokens se guardan en la base de datos.

- Un usuario puede tener máximo 3 sesiones activas simultáneas.

- Al hacer logout, el Refresh Token se invalida.

### Preguntas

**1. ¿Qué tablas necesitas en la base de datos?**

Tablas:Users, RefreshTokens (para guardar los tokens y gestionar las sesiones).

**2. ¿Qué endpoints necesitas en el AuthController?**
endpoints:
POST /register
POST /login
POST /refresh: Para renovar el Access Token con el Refresh Token
POST /logout

**3. ¿Qué información guardas en el Refresh Token de la base de datos?**

La nformación que guardaría sería: el token en sí, el UserId al que pertenece, fecha de creación, fecha de expiración, y si fue revocado o no.

**4. ¿Cómo implementas el límite de 3 sesiones activas?**

Consultaría directamente la tabla Refresh Tokens mediante el repositorio, contando cuántos tokens activos y no expirados tiene el usuario. Si hay más de tres tokens, rechazamos el login o invalidamos el Refresh Token más antiguo para permitir que el usuario siga haciendo login y no sacrificar la experiencia de usuario.

**5. ¿Qué pasa si el Access Token expira pero el Refresh Token sigue válido?**

El Refresh Token es enviado por el cliente, para que el servidor al validarlo, emita un nuevo Access Token para realizar las request con este nuevo token emitido.
