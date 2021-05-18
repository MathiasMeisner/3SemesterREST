using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _3SemesterREST.Manager;
using _3SemesterREST.Models;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace _3SemesterREST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingBoothsController : Controller
    {
        private readonly ParkingBoothManager _parkingBoothManager;
        public ParkingBoothsController(ParkingBoothContext parkingBoothContext)
        {
            _parkingBoothManager = new ParkingBoothManager(parkingBoothContext);
        }

        // GET: api/<ParkeringspladserController>
        [HttpGet]
        [ProducesResponseType(Status200OK)]
        public IEnumerable<ParkingBooth> Get()
        {
            return _parkingBoothManager.GetAllParkingBooths();
        }

        // GET: api/<ParkeringspladserController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(Status200OK)]
        [ProducesResponseType(Status404NotFound)]
        public ActionResult<ParkingBooth> Get(int id)
        {
            ParkingBooth parkingBooth = _parkingBoothManager.GetParkingBoothById(id);
            if (parkingBooth == null) return NotFound("No such id: " + id);
            return parkingBooth;
        }

        // POST api/<ParkeringspladserController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<ParkingBooth> Post([FromBody] ParkingBooth value)
        {
            try
            {
                ParkingBooth newParkingBooth = _parkingBoothManager.AddParkingBooth(value);
                string uri = Url.RouteUrl(RouteData.Values) + "/" + newParkingBooth.Id;
                return Created(uri, newParkingBooth);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<ParkeringspladserController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ParkingBooth> Put(int id, [FromBody] ParkingBooth value)
        {
            try
            {
                ParkingBooth updatedParkingBooth = _parkingBoothManager.UpdateParkingBooth(id, value);
                if (updatedParkingBooth == null) return NotFound("No such parking booth, Id: " + id);
                return Ok(updatedParkingBooth);

            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<ParkeringspladserController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ParkingBooth> Delete(int id)
        {
            ParkingBooth deletedParkingBooth = _parkingBoothManager.DeleteParkingBooth(id);
            if (deletedParkingBooth == null) return NotFound("No such parking booth, Id: " + id);
            return Ok(deletedParkingBooth);
        }
    }
}
