using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CarService.Api.Services;
using CarService.Api.Models;

namespace CarService.Api.Controllers
{
    [Route("api/[controller]")]
    public class CarsController : Controller
    {
        private readonly ICarService _carService;

        public CarsController(ICarService carService)
        {
            _carService = carService;
        }

        // GET api/cars/base-info/random
        [HttpGet]
        [Route("base-info/random")]
        public async Task<IEnumerable<BaseCarInfo>> GetListOfRandomCars()
        {
            var randomCarIds = await _carService.GetListOfRandomCarIds();
            return await _carService.GetBaseInfoAboutCars(randomCarIds);
        }

        // GET api/cars/detailed-info/{autoId}
        [HttpGet("detailed-info/{autoId}")]
        public async Task<DetailedCarInfo> GetDetailedCarInfo(int autoId)
        {
            return await _carService.GetDetailedCarInfo(autoId);
        }

        // GET api/cars/detailed-info/{autoId}/photos
        [HttpGet("detailed-info/{autoId}/photos")]
        public async Task<string> GetCarsPhotos(int autoId)
        {
            return await _carService.GetCarsPhotos(autoId);
        }
    }
}
