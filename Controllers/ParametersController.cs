using Microsoft.AspNetCore.Mvc;
using Forecast_Master.Interfaces;
using Forecast_Master.Models;
using System.Threading.Tasks;
using System.Linq;

namespace Forecast_Master.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParametersController : ControllerBase
    {
        private readonly IParameterService _parameterService;

        public ParametersController(IParameterService parameterService)
        {
            _parameterService = parameterService;
        }

        [HttpGet]
        public IActionResult GetAllParameters()
        {
            var parameters = _parameterService.GetAllParameters();
            return Ok(parameters);
        }

        [HttpGet("{id}")]
        public IActionResult GetParameterById(string id)
        {
            var parameter = _parameterService.GetParameterById(id);
            if (parameter == null)
                return NotFound("El parámetro no se encontró.");
            
            return Ok(parameter);
        }

        [HttpPost]
        public IActionResult CreateParameter([FromBody] Parameter model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Validaciones de negocio específicas
            if (!_parameterService.ValidateSucursal(model.SucursalId))
                return BadRequest("La sucursal proporcionada no es válida.");

            if (string.IsNullOrWhiteSpace(model.FirmaSupervisor))
                return BadRequest("La firma del supervisor es obligatoria.");

            if (!_parameterService.ValidateBooleanField(model.ExistenciasRequeridas))
                return BadRequest("El valor de existencias requeridas debe ser verdadero o falso.");

            _parameterService.CreateParameter(model);
            return Ok("Parámetro creado exitosamente.");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateParameter(string id, [FromBody] Parameter model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var parameter = _parameterService.GetParameterById(id);
            if (parameter == null)
                return NotFound("El parámetro no se encontró.");

            // Validaciones de negocio antes de la actualización
            if (!_parameterService.ValidateSucursal(model.SucursalId))
                return BadRequest("La sucursal proporcionada no es válida.");

            if (string.IsNullOrWhiteSpace(model.FirmaSupervisor))
                return BadRequest("La firma del supervisor es obligatoria.");

            if (!_parameterService.ValidateBooleanField(model.ExistenciasRequeridas))
                return BadRequest("El valor de existencias requeridas debe ser verdadero o falso.");

            _parameterService.UpdateParameter(id, model);
            return Ok("Parámetro actualizado exitosamente.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteParameter(string id)
        {
            var parameter = _parameterService.GetParameterById(id);
            if (parameter == null)
                return NotFound("El parámetro no se encontró.");

            _parameterService.DeleteParameter(id);
            return Ok("Parámetro eliminado exitosamente.");
        }
    }
}
