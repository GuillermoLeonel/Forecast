using System;
using System.ComponentModel.DataAnnotations;

namespace Forecast_Master.Models
{
    /// <summary>
    /// Representa un elemento del reporte de ventas, con detalles sobre cada venta.
    /// </summary>
    public class SalesReportItem
    {
        /// <summary>
        /// Fecha de la venta.
        /// </summary>
        [Required(ErrorMessage = "La fecha de venta es requerida.")]
        public DateTime SaleDate { get; set; }

        /// <summary>
        /// Identificador del producto vendido.
        /// </summary>
        [Required(ErrorMessage = "El ID del producto es requerido.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID del producto debe ser un valor positivo.")]
        public int ProductId { get; set; }

        /// <summary>
        /// Cantidad de producto vendida.
        /// </summary>
        [Required(ErrorMessage = "La cantidad es requerida.")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser un número positivo.")]
        public int Quantity { get; set; }

        /// <summary>
        /// Precio total de la venta.
        /// </summary>
        [Required(ErrorMessage = "El precio es requerido.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser un valor positivo.")]
        public decimal Price { get; set; }

        /// <summary>
        /// Identificador del cliente que realizó la compra.
        /// </summary>
        [Required(ErrorMessage = "El ID del cliente es requerido.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID del cliente debe ser un valor positivo.")]
        public int ClientId { get; set; }

        /// <summary>
        /// Identificador del vendedor que realizó la venta.
        /// </summary>
        [Required(ErrorMessage = "El ID del vendedor es requerido.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID del vendedor debe ser un valor positivo.")]
        public int VendorId { get; set; }
    }
}
