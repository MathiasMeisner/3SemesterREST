using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

        //public Booking GetByLicensePlate(string licenseplate)
        //{
        //    return _context.Bookings.Find(Int32.Parse(licenseplate));
        //}

        public Booking GetByLicensePlate(string licenseplate)

        {

            string SelectString = $"select * from Bookings where LicensePlate = '{licenseplate}'";



            using (SqlConnection conn = new SqlConnection(Secrets.ConnectionString))

            {

                conn.Open();



                using (SqlCommand command = new SqlCommand(SelectString, conn))

                {

                    command.Parameters.AddWithValue("@LicensePlate", licenseplate);



                    using (SqlDataReader reader = command.ExecuteReader())

                    {

                        if (reader.HasRows)

                        {

                            reader.Read();

                            return ReadName(reader);

                        }

                        return null;

                    }

                }

            }

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
                booking.Username = updates.Username;
                _context.Entry(booking).State = EntityState.Modified;
                _context.SaveChanges();
                return booking;
            }
            catch (DbUpdateException ex)
            {
                throw new BookingException(updates.Username + " " + updates.LicensePlate + " " + ex.InnerException.Message);
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

        private Booking ReadName(SqlDataReader reader)

        {

            int Id = reader.GetInt32(0);
            
            string LicensePlate = GuardedGet<string>(reader, 1);
            
            DateTime StartTime = GuardedGet<DateTime>(reader, 2);

            DateTime EndTime = GuardedGet<DateTime>(reader, 3);

            int ParkingId = reader.GetInt32(4);

            string Username = GuardedGet<string>(reader, 5);

            





            Booking booking = new Booking

            {

                Id = Id,

                ParkingId = ParkingId,

                Username = Username,

                LicensePlate = LicensePlate,

                StartTime = StartTime,

                EndTime = EndTime

            };



            return booking;


        }

        private static T GuardedGet<T>(SqlDataReader reader, int column)

        {

            if (reader.IsDBNull(column))

            {

                return default(T);

            }

            return reader.GetFieldValue<T>(column);

        }
    }
}
