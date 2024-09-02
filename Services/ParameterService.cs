using Forecast_Master.Interfaces;
using Forecast_Master.Models;
using System.Collections.Generic;
using System.Linq;

namespace Forecast_Master.Services
{
    public class ParameterService : IParameterService
    {
        private readonly ForecastContext _context;

        public ParameterService(ForecastContext context)
        {
            _context = context;
        }

        public List<Parameter> GetAllParameters()
        {
            return _context.Parameters.ToList();
        }

        public Parameter GetParameterById(string id)
        {
            return _context.Parameters.Find(id);
        }

        public bool ValidateSucursal(string sucursalId)
        {
            return _context.Sucursales.Any(s => s.Id == sucursalId);
        }

        public bool ValidateBooleanField(string field)
        {
            return field.ToLower() == "true" || field.ToLower() == "false";
        }

        public void CreateParameter(Parameter model)
        {
            var newParameter = new Parameter
            {
                SucursalId = model.SucursalId,
                FirmaSupervisor = model.FirmaSupervisor,
                ExistenciasRequeridas = model.ExistenciasRequeridas.ToLower() == "true" ? "true" : "false"  // Convertimos a string "true" o "false"
            };
                _context.Parameters.Add(newParameter);
                _context.SaveChanges();
        }

        public void UpdateParameter(string id, Parameter model)
        {
            var parameter = GetParameterById(id);
            if (parameter != null)
            {
                parameter.SucursalId = model.SucursalId;
                parameter.FirmaSupervisor = model.FirmaSupervisor;
                parameter.ExistenciasRequeridas = model.ExistenciasRequeridas.ToLower() == "true" ? "true" : "false";  // Convertimos a string "true" o "false"
                _context.SaveChanges();
            }  
        }


        public void DeleteParameter(string id)
        {
            var parameter = GetParameterById(id);
            if (parameter != null)
            {
                _context.Parameters.Remove(parameter);
                _context.SaveChanges();
            }
        }
    }
}
