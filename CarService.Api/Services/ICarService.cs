using System.Collections.Generic;
using System.Threading.Tasks;
using CarService.Api.Models;

namespace CarService.Api.Services
{
    public interface ICarService
    {
        Task<IEnumerable<int>> GetCarsIds(IDictionary<string, string> carsParameters);
        Task<IEnumerable<int>> GetRandomCarsIds();
        Task<IEnumerable<BaseCarInfo>> GetBaseInfoAboutCars(IEnumerable<int> autoIds);
        Task<DetailedCarInfo> GetDetailedCarInfo(int autoId);
        Task<IEnumerable<string>> GetCarPhotos(int autoId);
        Task<string> GetInitialTypesDropdownInfo();
        Task<string> GetMakesDropdownInfo(int categoryId);
        Task<string> GetModelsDropdownInfo(int categoryId, int makeId);
        
    }
}