using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Security.Claims;
using Forecast_Master.Models.Forecast.Models;
using Forecast_Master.Models;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration; // Objeto de configuración para acceder a configuraciones como claves JWT.
    private readonly ForecastContext _context; // Contexto de base de datos para acceder a los datos del usuario y permisos.

    /// <summary>
    /// Constructor del controlador de autenticación. Inyecta dependencias de configuración y contexto de base de datos.
    /// </summary>
    /// <param name="configuration">Interfaz para acceder a configuraciones como las claves JWT.</param>
    /// <param name="context">Contexto de la base de datos para interactuar con los datos de usuarios y permisos.</param>
    public AuthController(IConfiguration configuration, ForecastContext context)
    {
        _configuration = configuration;
        _context = context;
    }

    /// <summary>
    /// Endpoint para autenticar a un usuario utilizando sus credenciales de inicio de sesión.
    /// </summary>
    /// <param name="loginDto">DTO que contiene el nombre de usuario y la contraseña.</param>
    /// <returns>Un objeto JSON con un token JWT si la autenticación es exitosa; de lo contrario, devuelve Unauthorized.</returns>
    [HttpPost("login")]
    public IActionResult Login([FromBody] UserLoginDto loginDto)
    {
        // Busca al usuario en la base de datos utilizando el nombre de usuario y carga sus roles.
        var user = _context.Users
            .Include(u => u.UserRoles) // Incluye los roles del usuario
            .ThenInclude(ur => ur.Role) // Incluye la información de cada rol
            .SingleOrDefault(u => u.Nombre == loginDto.Username); // Busca el usuario por nombre

        // Verifica si el usuario existe y si la contraseña proporcionada es correcta
        if (user == null || !user.VerifyPassword(loginDto.Password))
            return Unauthorized(); // Si no se encuentra o la contraseña es incorrecta, devuelve Unauthorized

        // Genera un token JWT para el usuario autenticado
        var token = GenerateJwtToken(user);
        return Ok(new { Token = token }); // Retorna el token JWT en la respuesta
    }

    /// <summary>
    /// Genera un token JWT para el usuario autenticado que incluye claims personalizados como roles y permisos.
    /// </summary>
    /// <param name="user">El usuario autenticado para el cual se generará el token JWT.</param>
    /// <returns>Una cadena que representa el token JWT generado.</returns>
    private string GenerateJwtToken(User user)
    {
        // Configura la clave de seguridad y las credenciales de firma para el token JWT
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        // Obtener el primer rol del usuario de la lista de UserRoles; si no hay roles, asigna "Usuario" por defecto
        var userRole = user.UserRoles.FirstOrDefault()?.Role.Nombre ?? "Usuario";

        // Crea los claims personalizados para incluir en el token JWT
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()), // Identificador del usuario
            new Claim(ClaimTypes.Name, user.Nombre), // Nombre del usuario
            new Claim(ClaimTypes.Role, userRole) // Rol del usuario
        };

        // Agrega claims adicionales basados en los permisos asociados al usuario
        var userPermissions = _context.Permissions.Where(p => p.UserId == user.Id).ToList();
        foreach (var permiso in userPermissions)
        {
            // Agrega cada permiso como un claim personalizado
            claims.Add(new Claim("Permiso", $"{permiso.Modulo}:{permiso.NivelAcceso}"));
        }

        // Configura y crea el token JWT con los claims y las credenciales de firma
        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"], // Emisor del token
            audience: _configuration["Jwt:Audience"], // Audiencia del token
            claims: claims, // Claims a incluir en el token
            expires: DateTime.Now.AddMinutes(120), // Tiempo de expiración del token (120 minutos)
            signingCredentials: credentials); // Credenciales de firma

        // Genera y retorna el token JWT como una cadena
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
