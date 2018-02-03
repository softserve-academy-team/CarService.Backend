using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CarService.Api.Models;
using CarService.Api.Services;

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
        public async Task<IActionResult> GetRandomCars()
        {
            try
            {
                IEnumerable<int> randomCarsIds = await _carService.GetRandomCarsIds();
                IEnumerable<BaseCarInfo> randomCars = await _carService.GetBaseInfoAboutCars(randomCarsIds);
                return Ok(randomCars);
            }
            catch
            {
                return NotFound();
            }
        }

        // GET api/cars/detailed-info/{autoId}
        [HttpGet("detailed-info/{autoId}")]
        public async Task<IActionResult> GetDetailedCarInfo(int autoId)
        {
            try
            {
                DetailedCarInfo detailedCarInfo = await _carService.GetDetailedCarInfo(autoId);
                return Ok(detailedCarInfo);
            }
            catch
            {
                return NotFound();
            }
        }

        // GET api/cars/detailed-info/{autoId}/photos
        [HttpGet("detailed-info/{autoId}/photos")]
        public async Task<IActionResult> GetCarPhotos(int autoId)
        {
            try
            {
                IEnumerable<string> carPhotos = await _carService.GetCarPhotos(autoId);
                return Ok(carPhotos);
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
