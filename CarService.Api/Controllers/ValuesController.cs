using System.Collections.Generic;
using System.Threading.Tasks;
using CarService.DbAccess.DAL;
using CarService.DbAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CarService.Api.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Values here!");
        }

        // GET api/values/5
        [Authorize(Roles = "mechanic")]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok("You are mechanic!");
        }

        // POST api/values
        [Authorize(Roles = "customer")]
        [HttpPost("customer")]
        public IActionResult Post([FromBody]string value)
        {
            return Ok("You are customer!");
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
