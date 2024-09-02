using System.ComponentModel.DataAnnotations;

namespace Forecast_Master.Models.Forecast.Models
{
    /// <summary>
    /// Representa una compra en el sistema.
    /// </summary>
    public class Purchase
    {
        /// <summary>
        /// Identificador único de la compra.
        /// </summary>
        public int Id { get; set; } // Clave primaria

        /// <summary>
        /// Fecha de la compra.
        /// </summary>
        [Required(ErrorMessage = "La fecha de la compra es requerida.")]
        public DateTime Fecha { get; set; }

        /// <summary>
        /// Identificador del producto comprado (Foreign Key).
        /// </summary>
        [Required(ErrorMessage = "El ID del producto es requerido.")]
        public int ProductoId { get; set; }

        /// <summary>
        /// Cantidad de producto comprada.
        /// </summary>
        [Required(ErrorMessage = "La cantidad es requerida.")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser al menos 1.")]
        public int Cantidad { get; set; }

        /// <summary>
        /// Precio total de la compra.
        /// </summary>
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor que 0.")]
        public decimal Precio { get; set; }

        /// <summary>
        /// Identificador del proveedor (Foreign Key).
        /// </summary>
        [Required(ErrorMessage = "El ID del proveedor es requerido.")]
        public int ProveedorId { get; set; }

        /// <summary>
        /// Producto asociado a la compra. Propiedad de navegación.
        /// </summary>
        public Product Producto { get; set; }

        /// <summary>
        /// Proveedor asociado a la compra. Propiedad de navegación.
        /// </summary>
        public Provider Proveedor { get; set; }
    }
}
