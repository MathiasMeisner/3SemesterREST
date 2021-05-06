using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _3SemesterREST.Manager;
using _3SemesterREST.Models;
using Microsoft.AspNetCore.Http;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace _3SemesterREST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly CarManager _manager;
        public CarsController(CarContext context)
        {
            _manager = new CarManager(context);
        }
        // GET: api/<CarsController>
        [HttpGet]
        public IEnumerable<Car> Get()
        {
            return _manager.GetAllCars();
        }

        // GET api/<CarsController>/5
        [HttpGet("{id}")]
        public Car Get(int id)
        {
            return _manager.GetCarById(id);
        }

        // POST api/<CarsController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Car> Post([FromBody] Car value)
        {
            try
            {
                Car newCar = _manager.AddCar(value);
                string uri = Url.RouteUrl(RouteData.Values) + "/" + newCar.Id;
                return Created(uri, newCar);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<CarsController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Car> Put(int id, [FromBody] Car value)
        {
            try
            {
                Car updatedCar = _manager.UpdateCar(id, value);
                if (updatedCar == null) return NotFound("No such car, id: " + id);
                return Ok(updatedCar);

            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<CarsController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Car> Delete(int id)
        {
            Car deletedCar = _manager.DeleteCar(id);
            if (deletedCar == null) return NotFound("No such car, id: " + id);
            return Ok(deletedCar);
        }
    }
}
