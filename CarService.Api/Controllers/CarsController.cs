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

        // GET api/cars/dropdown/types  
        [HttpGet("dropdown/types")]
        public async Task<IActionResult> GetCarTypes()
        {
            try
            {
                var res = await _carService.GetInitialTypesDropdownInfo();
                return Ok(res);
            }
            catch
            {
                return NotFound();
            }
        }
        // GET api/cars/dropdown/makes/{categoryId} 
        [HttpGet("dropdown/makes/{categoryId}")]
        public async Task<IActionResult> GetMakes([FromRoute]int categoryId)
        {
            try
            {
                var res = await _carService.GetMakesDropdownInfo(categoryId);
                return Ok(res);
            }
            catch
            {
                return NotFound();
            }
        }
       
        // GET api/cars/dropdown/models/{categoryId}/{makeId} 
        [HttpGet("dropdown/models/{categoryId}/{makeId}")]
        public async Task<IActionResult> GetModels([FromRoute]int categoryId, int makeId)
        {
            try
            {
                var res = await _carService.GetModelsDropdownInfo(categoryId, makeId);
                return Ok(res);
            }
            catch
            {
                return NotFound();
            }
        }

 // GET api/cars/search
        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> GetListOfCars([FromQuery] string categoryId, string makeId, string modelId)
        {
          
            try
            {
                var carParameters = new Dictionary<string, string>(){
                    {"category_id", categoryId},
                    {"marka_id", makeId},
                    {"model_id", modelId}
                };

                // "Key: {0}, Value: {1}", item.Key, item.Value
                foreach (var item in carParameters)
                {
                System.Console.WriteLine("{0} {1}", item.Key, item.Value);
                }
                
                var carIds = await _carService.GetCarsIds(carParameters);
                var res = await _carService.GetBaseInfoAboutCars(carIds);
                return Ok(res);
            }
            catch
            {
                return NotFound();
            }
        }

    }
}
