using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CarService.Api.Models;
using CarService.Api.Services;

namespace CarService.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CarsController : Controller
    {
        private readonly ICarService _carService;

        public CarsController(ICarService carService)
        {
            _carService = carService;
        }

        /// <summary>Get random cars from AutoRia</summary>
        /// <returns>Return random cars from AutoRia</returns>
        /// <remarks>TODO detail description</remarks>
        /// <response code="200">Return list random cars</response>
        /// <response code="404">Not found response</response> 
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

        /// <summary>Get car from AutoRia by Id</summary>
        /// <param name="autoId">Id auto on AutoRia</param> 
        /// <returns>Return car detail information</returns>
        /// <remarks>TODO detail description</remarks>
        /// <response code="200">Return car by Id</response>
        /// <response code="404">Not found response</response> 
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

        /// <summary>Get all car photos</summary>
        /// <param name="autoId">Id auto on AutoRia</param> 
        /// <returns>Return all car photos urls</returns>
        /// <remarks>TODO detail description</remarks>
        /// <response code="200">Return car photos by Id</response>
        /// <response code="404">Not found response</response> 
        [HttpGet("detailed-info/{autoId}/photos")]
        // [ProducesResponseType(typeof(CarsController), 200)]
        // [ProducesResponseType(typeof(CarsController), 404)]
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

        /// <summary>Get cars type</summary>
        /// <returns>Return all cars type</returns>
        /// <remarks>TODO detail description</remarks>
        /// <response code="200">Return cars type</response>
        /// <response code="404">Not found response</response> 
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

        /// <summary>Get car mark from AutoRia by category Id</summary>
        /// <param name="categoryId">Category Id auto on AutoRia</param> 
        /// <returns>Return car mark information</returns>
        /// <remarks>TODO detail description</remarks>
        /// <response code="200">Return car mark by Category Id</response>
        /// <response code="404">Not found response</response> 
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

        /// <summary>Get car model from AutoRia by category Id and mark Id</summary>
        /// <param name="categoryId">Category Id auto on AutoRia</param> 
        /// <param name="makeId">Mark Id auto on AutoRia</param> 
        /// <returns>Return car models information</returns>
        /// <remarks>TODO detail description</remarks>
        /// <response code="200">Return car models by category Id and mark Id</response>
        /// <response code="404">Not found response</response> 
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

        /// <summary>Get all cars from AutoRia by category Id, mark Id and model Id</summary>
        /// <param name="categoryId">Category Id auto on AutoRia</param> 
        /// <param name="makeId">Mark Id auto on AutoRia</param> 
        /// <param name="modelId">Model Id auto on AutoRia</param>
        /// <returns>Return list of cars information</returns>
        /// <remarks>TODO detail description</remarks>
        /// <response code="200">Return list of cars by category Id mark Id and model Id</response>
        /// <response code="404">Not found response</response> 
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
