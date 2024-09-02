namespace Forecast_Master.Models
{
    // DTO para actualizar la información de un usuario existente en el sistema.
    public class UserUpdateDto
    {
        // Nuevo nombre de usuario.
        public string Username { get; set; }

        // Nueva contraseña del usuario (en texto plano, será hashada antes de almacenarse).
        public string Password { get; set; }

        // Nuevo estado del usuario, como "Activo" o "Inactivo".
        public string Status { get; set; }

        // Nueva sucursal a la que pertenece el usuario.
        public string Sucursal { get; set; }

        // Nueva aplicación o módulo al que el usuario tiene acceso.
        public string Aplicacion { get; set; }
    }
}
