namespace Forecast_Master.Models.Forecast.Models
{
    /// <summary>
    /// Representa un cliente en el sistema.
    /// </summary>
    public class Client
    {
        /// <summary>
        /// Clave primaria del cliente.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre del cliente.
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Dirección del cliente.
        /// </summary>
        public string Direccion { get; set; }

        /// <summary>
        /// Información de contacto del cliente.
        /// </summary>
        public string Contacto { get; set; }

        /// <summary>
        /// Ventas asociadas al cliente. Propiedad de navegación.
        /// </summary>
        public ICollection<Sale> Sales { get; set; }
    }
}
