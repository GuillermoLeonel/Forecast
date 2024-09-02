using Forecast_Master.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Forecast_Master.Models.Forecast.Models;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly ForecastContext _context; // Contexto de la base de datos para interactuar con los datos de usuarios.

    /// <summary>
    /// Constructor del controlador de usuarios. Inyecta el contexto de la base de datos.
    /// </summary>
    /// <param name="context">Contexto de la base de datos para interactuar con los datos de usuarios.</param>
    public UsuariosController(ForecastContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Obtiene la lista de todos los usuarios.
    /// </summary>
    /// <returns>Una lista de usuarios en formato JSON.</returns>
    [HttpGet]
    public async Task<IActionResult> GetUsuarios()
    {
        var usuarios = await _context.Users.ToListAsync(); // Obtiene todos los usuarios de la base de datos.
        return Ok(usuarios); // Devuelve la lista de usuarios.
    }

    /// <summary>
    /// Crea un nuevo usuario en la base de datos.
    /// </summary>
    /// <param name="userDto">DTO que contiene los datos del usuario a crear.</param>
    /// <returns>El usuario creado en formato JSON y la URL del recurso creado.</returns>
    [HttpPost]
    public async Task<IActionResult> CreateUsuario([FromBody] UserCreateDto userDto)
    {
        // Crea una nueva instancia de User con los datos proporcionados.
        var user = new User
        {
            Id = Guid.NewGuid().ToString(), // Genera un nuevo ID único para el usuario.
            Clave = userDto.Password,  //Asigna la clave del Usuario.
            Nombre = userDto.Username, // Asigna el nombre del usuario.
            Estatus = userDto.Status, // Asigna el estado del usuario.
            Sucursal = userDto.Sucursal, // Asigna la sucursal del usuario.
            Aplicacion = userDto.Aplicacion // Asigna la aplicación del usuario.
        };

        user.SetPassword(userDto.Password);  // Usa BCrypt para hashear la contraseña.

        _context.Users.Add(user); // Agrega el nuevo usuario al contexto de la base de datos.
        await _context.SaveChangesAsync(); // Guarda los cambios en la base de datos.
        return CreatedAtAction(nameof(GetUsuario), new { id = user.Id }, user); // Devuelve el usuario creado y la URL del recurso.
    }

    /// <summary>
    /// Obtiene un usuario específico por su ID.
    /// </summary>
    /// <param name="id">ID del usuario a obtener.</param>
    /// <returns>El usuario en formato JSON si se encuentra; de lo contrario, NotFound.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUsuario(string id)
    {
        var user = await _context.Users.FindAsync(id); // Busca al usuario por ID en la base de datos.
        if (user == null)
            return NotFound(); // Si no se encuentra el usuario, devuelve NotFound.

        return Ok(user); // Devuelve el usuario encontrado.
    }

    /// <summary>
    /// Actualiza los datos de un usuario existente.
    /// </summary>
    /// <param name="id">ID del usuario a actualizar.</param>
    /// <param name="userDto">DTO que contiene los datos del usuario a actualizar.</param>
    /// <returns>NoContent si la actualización es exitosa; de lo contrario, NotFound.</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUsuario(string id, [FromBody] UserUpdateDto userDto)
    {
        var user = await _context.Users.FindAsync(id); // Busca al usuario por ID en la base de datos.
        if (user == null)
            return NotFound(); // Si no se encuentra el usuario, devuelve NotFound.

        // Actualiza los campos del usuario con los nuevos datos proporcionados.
        user.Nombre = userDto.Username;
        user.Contrasena = userDto.Password; // Nota: La contraseña debería ser hashada nuevamente.
        user.Estatus = userDto.Status;
        user.Sucursal = userDto.Sucursal;
        user.Aplicacion = userDto.Aplicacion;

        _context.Entry(user).State = EntityState.Modified; // Marca el usuario como modificado en el contexto.
        await _context.SaveChangesAsync(); // Guarda los cambios en la base de datos.

        return NoContent(); // Devuelve NoContent indicando que la actualización fue exitosa.
    }

    /// <summary>
    /// Elimina un usuario de la base de datos por su ID.
    /// </summary>
    /// <param name="id">ID del usuario a eliminar.</param>
    /// <returns>NoContent si la eliminación es exitosa; de lo contrario, NotFound.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUsuario(string id)
    {
        var user = await _context.Users.FindAsync(id); // Busca al usuario por ID en la base de datos.
        if (user == null)
            return NotFound(); // Si no se encuentra el usuario, devuelve NotFound.

        _context.Users.Remove(user); // Elimina el usuario del contexto de la base de datos.
        await _context.SaveChangesAsync(); // Guarda los cambios en la base de datos.
        return NoContent(); // Devuelve NoContent indicando que la eliminación fue exitosa.
    }
}
