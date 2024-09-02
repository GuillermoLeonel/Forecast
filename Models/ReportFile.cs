namespace Forecast_Master.Models
{
    /// <summary>
    /// Representa un archivo de reporte generado, incluyendo su contenido y metadatos.
    /// </summary>
    public class ReportFile
    {
        /// <summary>
        /// Nombre del archivo del reporte.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Tipo MIME del archivo (por ejemplo, "application/pdf" o "text/csv").
        /// </summary>
        public string MimeType { get; set; }

        /// <summary>
        /// Contenido del archivo en formato de bytes.
        /// </summary>
        public byte[] Content { get; set; }

        /// <summary>
        /// Constructor para inicializar las propiedades del archivo de reporte.
        /// </summary>
        /// <param name="fileName">Nombre del archivo del reporte.</param>
        /// <param name="mimeType">Tipo MIME del archivo.</param>
        /// <param name="content">Contenido del archivo en formato de bytes.</param>
        public ReportFile(string fileName, string mimeType, byte[] content)
        {
            FileName = fileName;
            MimeType = mimeType;
            Content = content;
        }
    }
}
