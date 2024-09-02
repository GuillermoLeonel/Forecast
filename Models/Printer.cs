using System.ComponentModel.DataAnnotations;

namespace Forecast_Master.Models
{
    public class Printer
    {
        [Key]
        public string PrintersId { get; set; }

        [Required]
        public string TipoDocumento { get; set; }

        [Required]
        public string Impresora { get; set; }
    }
}
