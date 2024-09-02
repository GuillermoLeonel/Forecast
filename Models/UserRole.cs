namespace Forecast_Master.Models
{
    // Representa la asignación de un rol específico a un usuario en una sucursal.
    public class UserRole
    {
        // ID único de la asignación de usuario-rol.
        public string Id { get; set; }

        // ID del usuario al que se le asigna el rol.
        public string UserId { get; set; }

        // ID del rol asignado al usuario.
        public string RoleId { get; set; }

        // Sucursal donde el rol es aplicable para el usuario.
        public string Sucursal { get; set; }

        // Usuario asociado a esta asignación.
        public User User { get; set; }

        // Rol asociado a esta asignación.
        public Role Role { get; set; }
    }
}
