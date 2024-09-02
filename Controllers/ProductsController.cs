using Forecast_Master.Models;
using Forecast_Master.Models.Forecast.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly ForecastContext _context;

    public ProductsController(ForecastContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Obtiene todos los productos con paginación.
    /// </summary>
    /// <returns>Lista de productos.</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts(int pageNumber = 1, int pageSize = 10)
    {
        var products = await _context.Products
            .AsNoTracking()
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        
        return Ok(products);
    }

    /// <summary>
    /// Obtiene un producto específico por su ID.
    /// </summary>
    /// <param name="id">ID del producto.</param>
    /// <returns>Producto con el ID especificado.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await _context.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    /// <summary>
    /// Crea un nuevo producto.
    /// </summary>
    /// <param name="product">Datos del producto.</param>
    /// <returns>Producto creado.</returns>
    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        // Validar el producto
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Validación adicional
        if (product.Precio <= 0)
        {
            return BadRequest("El precio del producto debe ser mayor que 0.");
        }

        try
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }
        catch (DbUpdateException ex)
        {
            return StatusCode(500, $"Error al guardar el producto: {ex.Message}");
        }
    }
}
