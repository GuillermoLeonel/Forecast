namespace Forecast_Master.Models
{
    // Representa un permiso asignado a un usuario para un módulo específico del sistema.
    public class Permission
    {
        // ID único del permiso (clave primaria).
        public string Id { get; set; }

        // ID del usuario al que pertenece este permiso.
        public string UserId { get; set; }

        // Nombre del módulo al que se aplica este permiso, como "Ventas" o "Inventario".
        public string Modulo { get; set; }

        // Nivel de acceso para el módulo, como "Lectura", "Escritura", "Actualización".
        public string NivelAcceso { get; set; }

        // Relación con el usuario que tiene este permiso.
        public User User { get; set; }
    }
}
