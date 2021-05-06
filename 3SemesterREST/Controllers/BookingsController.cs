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
    public class BookingsController : Controller
    {
        private readonly BookingManager _bookingManager;
        public BookingsController(BookingContext bookingContext)
        {
            _bookingManager = new BookingManager(bookingContext);
        }

        // GET: api/<BookingsController>
        [HttpGet]
        public IEnumerable<Booking> Get()
        {
            return _bookingManager.GetAllBookings();
        }

        // GET: api/<BookingsController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(Status200OK)]
        [ProducesResponseType(Status404NotFound)]
        public ActionResult<Booking> Get(int id)
        {
            Booking booking = _bookingManager.GetBookingById(id);
            if (booking == null) return NotFound("No such id: " + id);
            return booking;
        }

        // POST api/<BookingsController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Booking> Post([FromBody] Booking value)
        {
            try
            {
                Booking newBooking = _bookingManager.AddBooking(value);
                string uri = Url.RouteUrl(RouteData.Values) + "/" + newBooking.Id;
                return Created(uri, newBooking);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<BookingsController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Booking> Put(int id, [FromBody] Booking value)
        {
            try
            {
                Booking updatedBooking = _bookingManager.UpdateBooking(id, value);
                if (updatedBooking == null) return NotFound("No such booking, id: " + id);
                return Ok(updatedBooking);

            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<BookingsController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Booking> Delete(int id)
        {
            Booking deletedBooking = _bookingManager.DeleteBooking(id);
            if (deletedBooking == null) return NotFound("No such booking, id: " + id);
            return Ok(deletedBooking);
        }
    }
}
