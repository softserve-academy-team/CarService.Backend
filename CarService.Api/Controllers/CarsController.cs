using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> GetListOfRandomCars()
        {
            try
            {
                var randomCarsIds = await _carService.GetListOfRandomCarsIds();
                var res = await _carService.GetBaseInfoAboutCars(randomCarsIds);
                return Ok(res);
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
                var res = await _carService.GetDetailedCarInfo(autoId);
                return Ok(res);
            }
            catch
            {
                return NotFound();
            }
        }

        // GET api/cars/detailed-info/{autoId}/photos
        [HttpGet("detailed-info/{autoId}/photos")]
        public async Task<IActionResult> GetCarsPhotos(int autoId)
        {
            try
            {
                var res = await _carService.GetCarsPhotos(autoId);
                return Ok(res);
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
