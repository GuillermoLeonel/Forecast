using System.ComponentModel.DataAnnotations;

namespace Forecast_Master.Models
{
    public class Folio
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string TipoDocumento { get; set; }

        [Required]
        public int SiguienteFolio { get; set; }
    }
}
