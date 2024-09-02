using System;
using System.ComponentModel.DataAnnotations;

namespace Forecast_Master.Models
{
    /// <summary>
    /// Representa el modelo de configuración utilizado para generar un reporte.
    /// </summary>
    public class ReportConfigurationModel
    {
        /// <summary>
        /// Enumeración que define los tipos de reportes disponibles.
        /// </summary>
        public enum ReportType
        {
            Sales,
            Purchases,
            Products
        }

        /// <summary>
        /// Tipo de reporte seleccionado.
        /// </summary>
        [Required(ErrorMessage = "El tipo de reporte es requerido.")]
        public ReportType ReportTypes { get; set; }

        /// <summary>
        /// Fecha de inicio del periodo del reporte.
        /// </summary>
        [Required(ErrorMessage = "La fecha de inicio es requerida.")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Fecha de fin del periodo del reporte. Debe ser mayor que la fecha de inicio.
        /// </summary>
        [Required(ErrorMessage = "La fecha de fin es requerida.")]
        [DataType(DataType.Date)]
        [DateGreaterThan("StartDate", ErrorMessage = "La fecha de fin debe ser mayor que la fecha de inicio.")]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Indica si el reporte es un resumen.
        /// </summary>
        public bool IsSummary { get; set; }

        /// <summary>
        /// Criterios adicionales utilizados para filtrar o personalizar el reporte.
        /// </summary>
        [MaxLength(255, ErrorMessage = "Los criterios no pueden exceder los 255 caracteres.")]
        public string Criteria { get; set; }

        /// <summary>
        /// Constructor sin parámetros.
        /// </summary>
        public ReportConfigurationModel() { }

        /// <summary>
        /// Constructor con parámetros para inicializar el modelo de configuración del reporte.
        /// </summary>
        public ReportConfigurationModel(ReportType reportTypes, DateTime startDate, DateTime endDate, bool isSummary)
        {
            ReportTypes = reportTypes;
            StartDate = startDate;
            EndDate = endDate;
            IsSummary = isSummary;
        }
    }

    /// <summary>
    /// Validación personalizada para asegurarse de que la fecha de fin sea mayor que la fecha de inicio.
    /// </summary>
    public class DateGreaterThanAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public DateGreaterThanAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var currentValue = (DateTime)value;

            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);

            if (property == null)
                throw new ArgumentException("Property with this name not found");

            var comparisonValue = (DateTime)property.GetValue(validationContext.ObjectInstance);

            if (currentValue <= comparisonValue)
                return new ValidationResult(ErrorMessage);

            return ValidationResult.Success;
        }
    }
}
