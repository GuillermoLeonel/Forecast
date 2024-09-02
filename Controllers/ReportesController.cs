using Microsoft.AspNetCore.Mvc;
using Forecast_Master.Interfaces;
using Forecast_Master.Models;
using System.Linq;

namespace Forecast_Master.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportesController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportesController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("types")]
        public IActionResult GetReportTypes()
        {
            // Obtener los tipos de reportes disponibles como una lista de strings
            var reportTypes = _reportService.GetReportTypes();
            return Ok(reportTypes);
        }

        [HttpPost("generate/sales")]
        public IActionResult GenerateSalesReport([FromBody] ReportConfigurationModel model)
        {
            if (ModelState.IsValid)
            {
                var report = _reportService.GenerateReport<SalesReportItem>(model);
                return Ok(report);
            }
            return BadRequest(ModelState);
        }

        [HttpPost("generate/purchases")]
        public IActionResult GeneratePurchasesReport([FromBody] ReportConfigurationModel model)
        {
            if (ModelState.IsValid)
            {
                var report = _reportService.GenerateReport<PurchasesReportItem>(model);
                return Ok(report);
            }
            return BadRequest(ModelState);
        }

        [HttpPost("generate/products")]
        public IActionResult GenerateProductsReport([FromBody] ReportConfigurationModel model)
        {
            if (ModelState.IsValid)
            {
                var report = _reportService.GenerateReport<ProductsReportItem>(model);
                return Ok(report);
            }
            return BadRequest(ModelState);
        }

    }
}
