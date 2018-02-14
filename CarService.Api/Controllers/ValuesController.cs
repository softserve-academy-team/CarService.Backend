using System.Collections.Generic;
using System.Threading.Tasks;
using CarService.DbAccess.DAL;
using CarService.DbAccess.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CarService.Api.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public ValuesController(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create())
            {
                var autos = new List<Auto>();
                var tasks = new List<Task>();                
                for (int i = 0; i < 10; ++i)
                {
                    autos.Add(new Auto
                    {
                        Info = $"Best car in the world {i}"
                    });
                }
                
                foreach (var auto in autos)
                {
                  var autoRepository = unitOfWork.Repository<Auto>();
                  autoRepository.Add(auto);
                  tasks.Add(unitOfWork.SaveAsync());
                }

                await Task.WhenAll(tasks);

                return Ok(autos[9]);
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
