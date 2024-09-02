namespace Forecast_Master.Models
{
    // DTO para la creación de un nuevo usuario en el sistema.
    public class UserCreateDto
    {
        // Nombre de usuario para el inicio de sesión.
        public string Username { get; set; }

        // Contraseña del usuario (en texto plano, será hashada antes de almacenarse).
        public string Password { get; set; }

        // Estado del usuario, como "Activo" o "Inactivo".
        public string Status { get; set; }

        // Sucursal a la que pertenece el usuario.
        public string Sucursal { get; set; }

        // Aplicación o módulo al que el usuario tiene acceso.
        public string Aplicacion { get; set; }
    }
}
