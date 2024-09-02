using Forecast_Master.Models.Forecast.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class PurchasesController : ControllerBase
{
    private readonly ForecastContext _context;

    public PurchasesController(ForecastContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Obtiene todas las compras con paginación.
    /// </summary>
    /// <returns>Lista de compras.</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Purchase>>> GetPurchases(int pageNumber = 1, int pageSize = 10)
    {
        var purchases = await _context.Purchases
            .AsNoTracking()
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        
        return Ok(purchases);
    }

    /// <summary>
    /// Obtiene una compra específica por su ID.
    /// </summary>
    /// <param name="id">ID de la compra.</param>
    /// <returns>Compra con el ID especificado.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<Purchase>> GetPurchase(int id)
    {
        var purchase = await _context.Purchases
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);

        if (purchase == null)
        {
            return NotFound();
        }

        return Ok(purchase);
    }

    /// <summary>
    /// Crea una nueva compra.
    /// </summary>
    /// <param name="purchase">Datos de la compra.</param>
    /// <returns>Compra creada.</returns>
    [HttpPost]
    public async Task<ActionResult<Purchase>> CreatePurchase(Purchase purchase)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Validación adicional
        if (purchase.Cantidad <= 0)
        {
            return BadRequest("La cantidad de la compra debe ser mayor que 0.");
        }

        try
        {
            _context.Purchases.Add(purchase);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPurchase), new { id = purchase.Id }, purchase);
        }
        catch (DbUpdateException ex)
        {
            return StatusCode(500, $"Error al guardar la compra: {ex.Message}");
        }
    }
}
