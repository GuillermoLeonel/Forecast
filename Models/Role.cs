namespace Forecast_Master.Models
{
    // Representa un rol de usuario en el sistema.
    public class Role
    {
        // ID único del rol (clave primaria).
        public string Id { get; set; }

        // Nombre del rol, como "Administrador" o "Usuario".
        public string Nombre { get; set; }

        // Relación con los usuarios que tienen este rol.
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
