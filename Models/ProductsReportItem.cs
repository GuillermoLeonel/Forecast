using System.ComponentModel.DataAnnotations;

namespace Forecast_Master.Models
{
    /// <summary>
    /// Representa un elemento del reporte de productos, con detalles del producto y su inventario.
    /// </summary>
    public class ProductsReportItem
    {
        /// <summary>
        /// Identificador único del producto.
        /// </summary>
        [Required(ErrorMessage = "El ID del producto es requerido.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID del producto debe ser un valor positivo.")]
        public int ProductId { get; set; }

        /// <summary>
        /// Nombre del producto.
        /// </summary>
        [Required(ErrorMessage = "El nombre del producto es requerido.")]
        [MaxLength(100, ErrorMessage = "El nombre del producto no puede exceder los 100 caracteres.")]
        public string ProductName { get; set; }

        /// <summary>
        /// Categoría del producto.
        /// </summary>
        [MaxLength(100, ErrorMessage = "La categoría no puede exceder los 100 caracteres.")]
        public string Category { get; set; }

        /// <summary>
        /// Precio del producto.
        /// </summary>
        [Required(ErrorMessage = "El precio es requerido.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser un valor positivo.")]
        public decimal Price { get; set; }

        /// <summary>
        /// Cantidad en stock del producto.
        /// </summary>
        [Required(ErrorMessage = "El stock es requerido.")]
        [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo.")]
        public int Stock { get; set; }
    }
}
