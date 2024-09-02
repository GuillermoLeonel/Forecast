namespace Forecast_Master.Models.Forecast.Models
{
    /// <summary>
    /// Representa un reporte generado en el sistema.
    /// </summary>
    public class Report
    {
        /// <summary>
        /// Identificador único del reporte.
        /// </summary>
        public int Id { get; set; } // Clave primaria

        /// <summary>
        /// Tipo de reporte (por ejemplo, Ventas, Compras, Productos).
        /// </summary>
        public string Tipo { get; set; }

        /// <summary>
        /// Fecha de inicio del periodo para el cual se genera el reporte.
        /// </summary>
        public DateTime FechaInicio { get; set; }

        /// <summary>
        /// Fecha de fin del periodo para el cual se genera el reporte.
        /// </summary>
        public DateTime FechaFin { get; set; }

        /// <summary>
        /// Detalles adicionales del reporte, que pueden incluir criterios o configuraciones específicas.
        /// </summary>
        public string Detalles { get; set; }
    }
}
