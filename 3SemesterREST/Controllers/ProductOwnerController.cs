using _3SemesterREST.Manager;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace _3SemesterREST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductOwnerController : ControllerBase
    {
        ProductOwnerManager manager = new ProductOwnerManager();
        // GET: api/<ProductOwnerController>
        [HttpGet]
        public int Get()
        {
            return manager.GetCarsByDay();
        }

        // GET api/<ProductOwnerController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ProductOwnerController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ProductOwnerController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProductOwnerController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
