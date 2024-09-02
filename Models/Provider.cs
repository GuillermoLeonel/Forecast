namespace Forecast_Master.Models.Forecast.Models
{
    /// <summary>
    /// Representa un proveedor en el sistema.
    /// </summary>
    public class Provider
    {
        /// <summary>
        /// Identificador único del proveedor.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre del proveedor.
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Información de contacto del proveedor.
        /// </summary>
        public string Contacto { get; set; }

        /// <summary>
        /// Compras asociadas al proveedor. Propiedad de navegación.
        /// </summary>
        public ICollection<Purchase> Purchases { get; set; }
    }
}
