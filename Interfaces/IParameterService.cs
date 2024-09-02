using Forecast_Master.Models;
using System.Collections.Generic;

namespace Forecast_Master.Interfaces
{
    public interface IParameterService
    {
        List<Parameter> GetAllParameters();
        Parameter GetParameterById(string id);
        bool ValidateSucursal(string sucursalId);
        bool ValidateBooleanField(string field);
        void CreateParameter(Parameter model);
        void UpdateParameter(string id, Parameter model);
        void DeleteParameter(string id);
    }
}
