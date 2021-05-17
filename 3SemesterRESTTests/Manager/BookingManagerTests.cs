using Microsoft.VisualStudio.TestTools.UnitTesting;
using _3SemesterREST.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _3SemesterREST.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace _3SemesterREST.Manager.Tests
{
    [TestClass()]
    public class BookingManagerTests
    {
        private readonly BookingContext _context;

        public BookingManagerTests()
        {
            DbContextOptionsBuilder<BookingContext> options = new DbContextOptionsBuilder<BookingContext>();
            options.UseSqlServer(Secrets.ConnectionString);
            _context = new BookingContext(options.Options);
        }

        [TestMethod()]
        public void TestItAll()
        {
            BookingManager bookingManager = new BookingManager(_context);
            List<Booking> allBookings = bookingManager.GetAllBookings().ToList();


            //LicensePlate skal være det samme som en LicensePlate i Cars, dette er add
            Booking data = new Booking {ParkingId = 1, Username = "John Enevoldsen", LicensePlate = "XR55143", StartTime = new DateTime(2021, 5, 15, 8, 10, 5), EndTime = new DateTime(2021, 5, 15, 14, 50, 4) };
            Booking newBooking = bookingManager.AddBooking(data);
            Assert.IsTrue(newBooking.Id > 0);
            Assert.AreEqual(data.ParkingId, newBooking.ParkingId);
            Assert.AreEqual(data.LicensePlate, newBooking.LicensePlate);
            Assert.AreEqual(data.Username, newBooking.Username);
            Assert.AreEqual(data.StartTime, newBooking.StartTime);
            Assert.AreEqual(data.EndTime, newBooking.EndTime);

            //Booking nullModelData = new Booking();
            //Assert.ThrowsException<BookingException>(() => bookingManager.AddBooking(nullModelData));

            //GetById
            Booking bookingById = bookingManager.GetBookingById(newBooking.Id);
            Assert.AreEqual(newBooking.Id, bookingById.Id);
            Assert.AreEqual(newBooking.ParkingId, bookingById.ParkingId);
            Assert.AreEqual(newBooking.LicensePlate, bookingById.LicensePlate);
            Assert.AreEqual(newBooking.Username, bookingById.Username);
            Assert.AreEqual(newBooking.StartTime, bookingById.StartTime);
            Assert.AreEqual(newBooking.EndTime, bookingById.EndTime);

            Assert.IsNull(bookingManager.GetBookingById(newBooking.Id + 1));

            //Update
            Booking updates = new Booking { Username = "Test Johnsen" };
            int id = newBooking.Id;
            Booking updatedBooking = bookingManager.UpdateBooking(id, updates);
            Assert.AreEqual(id, updatedBooking.Id);
            Assert.AreEqual(updates.Username, updatedBooking.Username);

            Assert.IsNull(bookingManager.UpdateBooking(id + 1, updates));

            //Delete all
            /*
            foreach (var b in bookingManager.GetAllBookings().ToList())
            {
                bookingManager.DeleteBooking(b.Id);
            }*/
        }
    }
}