using System.Collections.Generic;
using System.Threading.Tasks;
using CarService.Api.Models;

namespace CarService.Api.Services
{
    public interface ICarService
    {
        Task<IEnumerable<int>> GetListOfCarIds(IDictionary<string, string> carParameters);
        Task<IEnumerable<int>> GetListOfRandomCarIds();
        Task<IEnumerable<BaseCarInfo>> GetBaseInfoAboutCars(IEnumerable<int> ids);
    }
}