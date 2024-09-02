using Forecast_Master.Models.Forecast.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class SalesController : ControllerBase
{
    private readonly ForecastContext _context;

    public SalesController(ForecastContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Obtiene todas las ventas con paginación.
    /// </summary>
    /// <returns>Lista de ventas.</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Sale>>> GetSales(int pageNumber = 1, int pageSize = 10)
    {
        var sales = await _context.Sales
            .AsNoTracking()  // Mejora el rendimiento para consultas de solo lectura
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        
        return Ok(sales);
    }

    /// <summary>
    /// Obtiene una venta específica por su ID.
    /// </summary>
    /// <param name="id">ID de la venta.</param>
    /// <returns>Venta con el ID especificado.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<Sale>> GetSale(int id)
    {
        var sale = await _context.Sales
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == id); // Usa FirstOrDefaultAsync para evitar cargas innecesarias

        if (sale == null)
        {
            return NotFound();
        }

        return Ok(sale);
    }

    /// <summary>
    /// Crea una nueva venta.
    /// </summary>
    /// <param name="sale">Datos de la venta.</param>
    /// <returns>Venta creada.</returns>
    [HttpPost]
    public async Task<ActionResult<Sale>> CreateSale(Sale sale)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Validación adicional
        if (sale.Cantidad <= 0)
        {
            return BadRequest("La cantidad de la venta debe ser mayor que 0.");
        }

        try
        {
            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetSale), new { id = sale.Id }, sale);
        }
        catch (DbUpdateException ex)
        {
            return StatusCode(500, $"Error al guardar la venta: {ex.Message}");
        }
    }
}
