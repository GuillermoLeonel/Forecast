using System;
using System.ComponentModel.DataAnnotations;

namespace Forecast_Master.Models
{
    /// <summary>
    /// Representa un elemento del reporte de compras, con detalles sobre cada compra.
    /// </summary>
    public class PurchasesReportItem
    {
        /// <summary>
        /// Fecha de la compra.
        /// </summary>
        [Required(ErrorMessage = "La fecha de compra es requerida.")]
        public DateTime PurchaseDate { get; set; }

        /// <summary>
        /// Identificador del producto comprado.
        /// </summary>
        [Required(ErrorMessage = "El ID del producto es requerido.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID del producto debe ser un valor positivo.")]
        public int ProductId { get; set; }

        /// <summary>
        /// Cantidad de producto comprada.
        /// </summary>
        [Required(ErrorMessage = "La cantidad es requerida.")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser un número positivo.")]
        public int Quantity { get; set; }

        /// <summary>
        /// Precio total de la compra.
        /// </summary>
        [Required(ErrorMessage = "El precio es requerido.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser un valor positivo.")]
        public decimal Price { get; set; }

        /// <summary>
        /// Identificador del proveedor.
        /// </summary>
        [Required(ErrorMessage = "El ID del proveedor es requerido.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID del proveedor debe ser un valor positivo.")]
        public int ProviderId { get; set; }
    }
}
