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
    public class ParkeringspladserController : Controller
    {
        private readonly ParkeringspladsManager _parkeringspladsManager;
        public ParkeringspladserController(ParkeringspladsContext parkeringspladsContext)
        {
            _parkeringspladsManager = new ParkeringspladsManager(parkeringspladsContext);
        }

        // GET: api/<ParkeringspladserController>
        [HttpGet]
        public IEnumerable<Parkeringsplads> Get()
        {
            return _parkeringspladsManager.GetAllParkeringspladser();
        }

        // GET: api/<ParkeringspladserController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(Status200OK)]
        [ProducesResponseType(Status404NotFound)]
        public ActionResult<Parkeringsplads> Get(int id)
        {
            Parkeringsplads parkeringsplads = _parkeringspladsManager.GetParkeringspladsById(id);
            if (parkeringsplads == null) return NotFound("No such id: " + id);
            return parkeringsplads;
        }

        // POST api/<ParkeringspladserController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Parkeringsplads> Post([FromBody] Parkeringsplads value)
        {
            try
            {
                Parkeringsplads newParkeringsplads = _parkeringspladsManager.AddParkeringsplads(value);
                string uri = Url.RouteUrl(RouteData.Values) + "/" + newParkeringsplads.Id;
                return Created(uri, newParkeringsplads);
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
        public ActionResult<Parkeringsplads> Put(int id, [FromBody] Parkeringsplads value)
        {
            try
            {
                Parkeringsplads updatedParkeringsplads = _parkeringspladsManager.UpdateParkeringsplads(id, value);
                if (updatedParkeringsplads == null) return NotFound("No such parkeringsplads, Id: " + id);
                return Ok(updatedParkeringsplads);

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
        public ActionResult<Parkeringsplads> Delete(int id)
        {
            Parkeringsplads deletedParkeringsplads = _parkeringspladsManager.DeleteParkeringsplads(id);
            if (deletedParkeringsplads == null) return NotFound("No such parkeringsplads, Id: " + id);
            return Ok(deletedParkeringsplads);
        }
    }
}
