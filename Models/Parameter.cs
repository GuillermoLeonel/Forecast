namespace Forecast_Master.Models
{
    public class Parameter
    {
        public string SucursalId { get; set; } // Identificador de la sucursal
        public string FirmaSupervisor { get; set; } // Firma del supervisor
        public string ExistenciasRequeridas { get; set; } // Indicador de si las existencias son requeridas (Sí/No)
    }
}
