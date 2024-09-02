using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BCrypt.Net; // Importa BCrypt para el hashing de contraseñas

namespace Forecast_Master.Models
{
    // Representa a un usuario en el sistema con credenciales, estado, y sucursal.
    public class User
    {
        // ID único del usuario (clave primaria).
        public string Id { get; set; }

        // Clave del usuario, como número de empleado. Requerido, máx. 100 caracteres.
        [Required]
        [MaxLength(100)]
        public string Clave { get; set; }

        // Nombre del usuario para identificación. Requerido, máx. 100 caracteres.
        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }

        // Contraseña del usuario (encriptada con BCrypt). Requerido.
        [Required]
        public string Contrasena { get; set; }

        // Estado del usuario (Activo/Inactivo). Requerido.
        [Required]
        public string Estatus { get; set; }

        // Sucursal asociada al usuario.
        public string Sucursal { get; set; }

        // Aplicación o sistema a la que el usuario tiene acceso.
        public string Aplicacion { get; set; }

        // Roles asignados al usuario.
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

        // Permisos asociados al usuario.
        public ICollection<Permission> Permissions { get; set; } = new List<Permission>();

        // Hashea la contraseña usando BCrypt.
        public void SetPassword(string password)
        {
            this.Contrasena = BCrypt.Net.BCrypt.HashPassword(password);
        }

        // Verifica si la contraseña proporcionada coincide con el hash almacenado.
        public bool VerifyPassword(string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, this.Contrasena);
        }
    }
}
