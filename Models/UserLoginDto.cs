namespace Forecast_Master.Models
{
    // DTO para manejar la información de inicio de sesión de un usuario.
    public class UserLoginDto
    {
        // Nombre de usuario utilizado para el inicio de sesión.
        public string Username { get; set; }

        // Contraseña del usuario para autenticación.
        public string Password { get; set; }
    }
}
