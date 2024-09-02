namespace Forecast_Master.Models
{
    public class Sucursal
    {
        public string Id { get; set; }  // Clave primaria para la sucursal
        public string Nombre { get; set; }  // Nombre de la sucursal

        // Otros campos relacionados con la sucursal
        public ICollection<Parameter> Parameters { get; set; } // Relación con Parameters si es necesario
    }
}
