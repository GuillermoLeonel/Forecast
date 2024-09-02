namespace Forecast_Master.Models.Forecast.Models
{
    /// <summary>
    /// Representa un vendedor en el sistema.
    /// </summary>
    public class Vendor
    {
        /// <summary>
        /// Identificador único del vendedor.
        /// </summary>
        public int Id { get; set; } // Clave primaria

        /// <summary>
        /// Nombre del vendedor.
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Comisión asociada al vendedor.
        /// </summary>
        public decimal Comision { get; set; }

        /// <summary>
        /// Ventas realizadas por el vendedor. Propiedad de navegación.
        /// </summary>
        public ICollection<Sale> Sales { get; set; }
    }
}
