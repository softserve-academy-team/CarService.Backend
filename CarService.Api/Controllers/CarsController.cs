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

        // GET api/cars/random
        [HttpGet]
        [Route("random")]
        public async Task<IEnumerable<BaseCarInfo>> GetListOfRandomCars()
        {
            var randomCarIds = await _carService.GetListOfRandomCarIds();
            return await _carService.GetBaseInfoAboutCars(randomCarIds);
        }
    }
}
