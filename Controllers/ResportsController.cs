using Forecast_Master.Models.Forecast.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class ReportsController : ControllerBase
{
    private readonly ForecastContext _context;

    public ReportsController(ForecastContext context)
    {
        _context = context;
    }

    [HttpGet("sales")]
    public async Task<ActionResult<IEnumerable<Sale>>> GetSalesReports()
    {
        return await _context.Sales.ToListAsync();
    }

    [HttpGet("purchases")]
    public async Task<ActionResult<IEnumerable<Purchase>>> GetPurchasesReports()
    {
        return await _context.Purchases.ToListAsync();
    }

    [HttpGet("products")]
    public async Task<ActionResult<IEnumerable<Product>>> GetProductsReports()
    {
        return await _context.Products.ToListAsync();
    }

    [HttpPost("generate")]
    public async Task<ActionResult<Report>> GenerateReport(Report report)
    {
        _context.Reports.Add(report);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GenerateReport), new { id = report.Id }, report);
    }
}
