namespace Forecast_Master.Models
{
    /// <summary>
    /// Representa la configuración utilizada para generar un reporte.
    /// </summary>
    public class ReportConfiguration
    {
        /// <summary>
        /// Identificador único de la configuración del reporte.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Tipo de reporte (por ejemplo, Ventas, Compras, Productos).
        /// </summary>
        public string Tipo { get; set; }

        /// <summary>
        /// Fecha de inicio del periodo para el reporte.
        /// </summary>
        public DateTime FechaInicio { get; set; }

        /// <summary>
        /// Fecha de fin del periodo para el reporte.
        /// </summary>
        public DateTime FechaFin { get; set; }

        /// <summary>
        /// Criterios adicionales utilizados para configurar el reporte.
        /// </summary>
        public string Criterios { get; set; }
    }
}
