using Forecast_Master.Models;
using System.Collections.Generic;

namespace Forecast_Master.Interfaces
{
    /// <summary>
    /// Interfaz que define los métodos necesarios para manejar la generación y exportación de reportes en el sistema.
    /// </summary>
    public interface IReportService
    {
        /// <summary>
        /// Obtiene los tipos de reportes disponibles en el sistema.
        /// </summary>
        /// <returns>Una lista de cadenas que representan los tipos de reportes disponibles.</returns>
        List<string> GetReportTypes();

        /// <summary>
        /// Guarda la configuración de un reporte, como el tipo de reporte, fechas de inicio y fin, y otros criterios específicos.
        /// </summary>
        /// <param name="model">El modelo de configuración del reporte que contiene los parámetros y criterios de configuración.</param>
        void SaveConfiguration(ReportConfigurationModel model);

        /// <summary>
        /// Genera un reporte basado en el tipo de reporte y la configuración proporcionada.
        /// </summary>
        /// <typeparam name="T">El tipo de reporte a generar (por ejemplo, SalesReportItem, PurchasesReportItem, etc.).</typeparam>
        /// <param name="model">El modelo de configuración del reporte que contiene los parámetros y criterios de configuración.</param>
        /// <returns>Un objeto ReportViewModel que contiene los datos generados del reporte.</returns>
        Task<ReportViewModel<T>> GenerateReport<T>(ReportConfigurationModel model) where T : class;

        /// <summary>
        /// Obtiene un archivo de reporte en el formato especificado (por ejemplo, PDF, Excel).
        /// </summary>
        /// <param name="reportId">El ID del reporte para el cual se generará el archivo.</param>
        /// <param name="format">El formato de archivo deseado (por ejemplo, "pdf", "xlsx").</param>
        /// <returns>Un objeto ReportFile que contiene el nombre del archivo, el tipo MIME y el contenido del archivo en bytes.</returns>
        ReportFile GetReportFile(int reportId, string format);
    }
}
