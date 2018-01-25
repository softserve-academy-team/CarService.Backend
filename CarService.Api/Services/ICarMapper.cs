using CarService.Api.Models;

namespace CarService.Api.Services
{
    public interface ICarMapper
    {
        BaseCarInfo MapToBaseCarInfoObject(string jsonString);
    }
}