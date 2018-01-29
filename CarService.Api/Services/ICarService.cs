using System.Collections.Generic;
using System.Threading.Tasks;
using CarService.Api.Models;

namespace CarService.Api.Services
{
    public interface ICarService
    {
        Task<IEnumerable<int>> GetListOfCarsIds(IDictionary<string, string> carParameters);
        Task<IEnumerable<int>> GetListOfRandomCarsIds();
        Task<IEnumerable<BaseCarInfo>> GetBaseInfoAboutCars(IEnumerable<int> autoIds);
        Task<DetailedCarInfo> GetDetailedCarInfo(int autoId);
        Task<IEnumerable<string>> GetCarsPhotos(int autoId);
    }
}