using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _3SemesterREST.Models
{
    public class ParkinglotsException : Exception
    {
        public ParkinglotsException(string message) : base(message)
        {

        }
    }
}
