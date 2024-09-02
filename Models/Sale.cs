using System.ComponentModel.DataAnnotations;

namespace Forecast_Master.Models.Forecast.Models
{
    /// <summary>
    /// Representa una venta en el sistema.
    /// </summary>
    public class Sale
    {
        /// <summary>
        /// Identificador único de la venta.
        /// </summary>
        public int Id { get; set; } // Clave primaria

        /// <summary>
        /// Fecha en la que se realizó la venta.
        /// </summary>
        [Required(ErrorMessage = "La fecha de la venta es requerida.")]
        public DateTime Fecha { get; set; }

        /// <summary>
        /// Identificador del producto vendido (Foreign Key).
        /// </summary>
        [Required(ErrorMessage = "El ID del producto es requerido.")]
        public int ProductoId { get; set; }

        /// <summary>
        /// Cantidad de producto vendida.
        /// </summary>
        [Required(ErrorMessage = "La cantidad es requerida.")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser al menos 1.")]
        public int Cantidad { get; set; }

        /// <summary>
        /// Precio total de la venta.
        /// </summary>
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor que 0.")]
        public decimal Precio { get; set; }

        /// <summary>
        /// Identificador del cliente que realizó la compra (Foreign Key).
        /// </summary>
        [Required(ErrorMessage = "El ID del cliente es requerido.")]
        public int ClienteId { get; set; }

        /// <summary>
        /// Identificador del vendedor que realizó la venta (Foreign Key).
        /// </summary>
        [Required(ErrorMessage = "El ID del vendedor es requerido.")]
        public int VendedorId { get; set; }

        /// <summary>
        /// Producto asociado a la venta. Propiedad de navegación.
        /// </summary>
        public Product Producto { get; set; }

        /// <summary>
        /// Cliente asociado a la venta. Propiedad de navegación.
        /// </summary>
        public Client Cliente { get; set; }

        /// <summary>
        /// Vendedor asociado a la venta. Propiedad de navegación.
        /// </summary>
        public Vendor Vendedor { get; set; }
    }
}
