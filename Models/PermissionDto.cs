namespace Forecast_Master.Models
{
    // DTO (Data Transfer Object) para transferir datos de permisos entre la API y el cliente.
    public class PermissionDto
    {
        // Nombre del módulo al que se aplica el permiso, como "Ventas" o "Inventario".
        public string Module { get; set; }

        // Nivel de acceso para el módulo, como "Lectura", "Escritura", "Actualización".
        public string AccessLevel { get; set; }
    }
}
