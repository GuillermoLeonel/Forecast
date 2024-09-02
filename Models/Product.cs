using System.ComponentModel.DataAnnotations;

namespace Forecast_Master.Models.Forecast.Models
{
    /// <summary>
    /// Representa un producto en el sistema.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Clave primaria del producto.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre del producto.
        /// </summary>
        [Required(ErrorMessage = "El nombre del producto es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
        public string Nombre { get; set; }

        /// <summary>
        /// Categoría del producto.
        /// </summary>
        [MaxLength(50, ErrorMessage = "La categoría no puede superar los 50 caracteres.")]
        public string Categoria { get; set; }

        /// <summary>
        /// Precio del producto.
        /// </summary>
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor que 0.")]
        public decimal Precio { get; set; }

        /// <summary>
        /// Stock disponible del producto.
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo.")]
        public int Stock { get; set; }

        /// <summary>
        /// Ventas asociadas al producto. Propiedad de navegación.
        /// </summary>
        public ICollection<Sale> Sales { get; set; }

        /// <summary>
        /// Compras asociadas al producto. Propiedad de navegación.
        /// </summary>
        public ICollection<Purchase> Purchases { get; set; }
    }
}
