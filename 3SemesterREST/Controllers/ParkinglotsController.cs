using _3SemesterREST.Manager;
using _3SemesterREST.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.StatusCodes;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace _3SemesterREST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkinglotsController : ControllerBase
    {
        ParkinglotsManager manager = new ParkinglotsManager();

        // GET: api/<ProductOwnerController>
        [HttpGet]

        public IEnumerable<Parkinglots> Get()
        {
            return manager.GetAll();
        }

        // GET api/<ProductOwnerController>/5
        [HttpGet("{year}/{months}/{day}")]
        [ProducesResponseType(Status200OK)]
        [ProducesResponseType(Status404NotFound)]
        public int Getbyday(int year, int months, int day)
        {
            return manager.GetCarsByDay(year,months,day);
        }

        // POST api/<ProductOwnerController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Parkinglots> Post([FromBody] Parkinglots value)
        {
            try
            {
                Parkinglots parkinglots = manager.Add(value);
                string uri = Url.RouteUrl(RouteData.Values) + "/" + parkinglots.id;
                return Created(uri, parkinglots);
            }
            catch (ParkinglotsException ex)
            {
                return BadRequest(ex.Message);
            }
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
