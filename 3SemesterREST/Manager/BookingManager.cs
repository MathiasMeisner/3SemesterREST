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

            string SelectString = "select * from Bookings where LicensePlate = @licenseplate";



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
                booking.LicensePlate = updates.LicensePlate;
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

            string Username = GuardedGet<string>(reader, 1);

            string LicensePlate = GuardedGet<string>(reader, 2);

            DateTime BookTime = GuardedGet<DateTime>(reader, 3);



            Booking booking = new Booking

            {

                Id = Id,

                Username = Username,

                LicensePlate = LicensePlate,

                BookTime = BookTime

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
