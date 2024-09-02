using System.ComponentModel.DataAnnotations;

namespace Forecast_Master.Models
{
    public class Point
    {
        [Key]
        public string PointsId { get; set; }

        [Required]
        [Range(0.01, float.MaxValue, ErrorMessage = "El valor debe ser positivo.")]
        public float PesosPorPunto { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El valor debe ser un número entero positivo.")]
        public int PuntosMinimos { get; set; }
    }
}
