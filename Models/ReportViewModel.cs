using System.Collections.Generic;

namespace Forecast_Master.Models
{
    /// <summary>
    /// Modelo de vista para representar un reporte y sus datos asociados.
    /// </summary>
    /// <typeparam name="T">El tipo de datos que contiene el reporte.</typeparam>
    public class ReportViewModel<T>
    {
        /// <summary>
        /// Título del reporte.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Tipo de reporte.
        /// </summary>
        public string ReportType { get; set; }

        /// <summary>
        /// Datos del reporte. Contiene la lista de elementos de tipo <typeparamref name="T"/>.
        /// </summary>
        public List<T> ReportData { get; set; }

        /// <summary>
        /// Fecha de inicio del periodo cubierto por el reporte.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Fecha de fin del periodo cubierto por el reporte.
        /// </summary>
        public DateTime EndDate { get; set; }
    }
}
