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

        // GET api/cars/detailed-info/{id}
        [HttpGet("detailed-info/{id}")]
        public async Task<DetailedCarInfo> GetDetailedCarInfoById(int id)
        {
            return await _carService.GetDetailedCarInfoById(id);
        }

        // GET api/cars/detailed-info/{id}/photos
        [HttpGet("detailed-info/{id}/photos")]
        public async Task<string> GetCarsPhotosById(int id)
        {
            return await _carService.GetCarsPhotosById(id);
        }
    }
}
