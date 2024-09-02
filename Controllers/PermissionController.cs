using Forecast_Master.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class PermisosController : ControllerBase
{
    private readonly ForecastContext _context; // Contexto de la base de datos para interactuar con los datos de usuarios y permisos.

    /// <summary>
    /// Constructor del controlador de permisos. Inyecta el contexto de la base de datos.
    /// </summary>
    /// <param name="context">Contexto de la base de datos para interactuar con los datos de usuarios y permisos.</param>
    public PermisosController(ForecastContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Asigna una lista de permisos a un usuario específico.
    /// </summary>
    /// <param name="userId">ID del usuario al que se le asignarán los permisos.</param>
    /// <param name="permisosDto">Lista de DTOs de permisos a asignar.</param>
    /// <returns>Una respuesta HTTP indicando el éxito o el fallo de la operación.</returns>
    [HttpPost("assign")]
    public async Task<IActionResult> AssignPermisos(string userId, [FromBody] List<PermissionDto> permisosDto)
    {
        // Busca al usuario por ID y carga sus permisos actuales.
        var user = await _context.Users.Include(u => u.Permissions).SingleOrDefaultAsync(u => u.Id == userId);
        if (user == null)
            return NotFound(); // Si no se encuentra el usuario, devuelve NotFound.

        // Asigna cada permiso del DTO a un nuevo objeto Permission y lo agrega al usuario.
        foreach (var permisoDto in permisosDto)
        {
            var permiso = new Permission
            {
                Id = Guid.NewGuid().ToString(), // Genera un nuevo ID para el permiso.
                Modulo = permisoDto.Module, // Asigna el módulo del permiso.
                NivelAcceso = permisoDto.AccessLevel, // Asigna el nivel de acceso del permiso.
                UserId = userId // Asigna el ID del usuario.
            };
            user.Permissions.Add(permiso); // Agrega el permiso a la colección de permisos del usuario.
        }

        await _context.SaveChangesAsync(); // Guarda los cambios en la base de datos.
        return Ok(); // Devuelve una respuesta de éxito.
    }

    /// <summary>
    /// Actualiza los permisos de un usuario específico comparando los permisos actuales con los nuevos.
    /// </summary>
    /// <param name="userId">ID del usuario cuyos permisos se actualizarán.</param>
    /// <param name="permisosDto">Lista de DTOs de permisos a actualizar.</param>
    /// <returns>Una respuesta HTTP indicando el éxito o el fallo de la operación.</returns>
    [HttpPut("update/{userId}")]
    public async Task<IActionResult> UpdatePermisos(string userId, [FromBody] List<PermissionDto> permisosDto)
    {
        // Busca al usuario por ID y carga sus permisos actuales.
        var user = await _context.Users.Include(u => u.Permissions).SingleOrDefaultAsync(u => u.Id == userId);
        if (user == null)
            return NotFound(); // Si no se encuentra el usuario, devuelve NotFound.

        // Lista de permisos actuales del usuario.
        var currentPermissions = user.Permissions.ToList();

        // Crea una lista de nuevos permisos a partir de los DTOs proporcionados.
        var newPermissions = permisosDto.Select(p => new Permission
        {
            Id = Guid.NewGuid().ToString(), // Genera un nuevo ID para cada permiso.
            Modulo = p.Module, // Asigna el módulo del permiso.
            NivelAcceso = p.AccessLevel, // Asigna el nivel de acceso del permiso.
            UserId = userId // Asigna el ID del usuario.
        }).ToList();

        // Identifica los permisos que deben eliminarse (no presentes en la nueva lista).
        var permissionsToRemove = currentPermissions
            .Where(cp => !newPermissions.Any(np => np.Modulo == cp.Modulo && np.NivelAcceso == cp.NivelAcceso))
            .ToList();

        // Identifica los permisos que deben agregarse (nuevos o modificados).
        var permissionsToAdd = newPermissions
            .Where(np => !currentPermissions.Any(cp => cp.Modulo == np.Modulo && cp.NivelAcceso == np.NivelAcceso))
            .ToList();

        // Elimina los permisos que ya no son necesarios.
        foreach (var permiso in permissionsToRemove)
        {
            user.Permissions.Remove(permiso);
        }

        // Agrega los permisos nuevos.
        foreach (var permiso in permissionsToAdd)
        {
            user.Permissions.Add(permiso);
        }

        await _context.SaveChangesAsync(); // Guarda los cambios en la base de datos.
        return Ok(); // Devuelve una respuesta de éxito.
    }
}
