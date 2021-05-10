using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _3SemesterREST.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string LicensePlate { get; set; }
        public DateTime BookTime { get; set; }

    }
}
