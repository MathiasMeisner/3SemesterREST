using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _3SemesterREST.Models;
using Microsoft.EntityFrameworkCore;

namespace _3SemesterREST.Manager
{
    public class BookingManager
    {
        private readonly BookingContext _context;

        public BookingManager(BookingContext context)
        {
            _context = context;
        }

        public BookingManager()
        {

        }

        public IEnumerable<Booking> GetAllBookings()
        {
            return _context.Bookings;
        }

        public Booking GetBookingById(int id)
        {
            return _context.Bookings.Find(id);
        }

        public Booking AddBooking(Booking newBooking)
        {
            try
            {
                _context.Bookings.Add(newBooking);
                _context.SaveChanges();
                return newBooking;
            }
            catch (DbUpdateException ex)
            {
                _context.Bookings.Remove(newBooking);
                throw new BookingException(ex.InnerException.Message);
            }
        }

        public Booking UpdateBooking(int id, Booking updates)
        {
            try
            {
                Booking booking = _context.Bookings.Find(id);
                if (booking == null) return null;
                booking.Name = updates.Name;
                booking.LicensePlate = updates.LicensePlate;
                _context.Entry(booking).State = EntityState.Modified;
                _context.SaveChanges();
                return booking;
            }
            catch (DbUpdateException ex)
            {
                throw new BookingException(updates.Name + " " + updates.LicensePlate + " " + ex.InnerException.Message);
            }
        }

        public Booking DeleteBooking(int id)
        {
            Booking booking = _context.Bookings.Find(id);
            if (booking == null) return null;
            _context.Bookings.Remove(booking);
            _context.SaveChanges();
            return booking;
        }
    }
}
