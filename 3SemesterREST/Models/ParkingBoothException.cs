using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _3SemesterREST.Models
{
    public class ParkingBoothException : Exception
    {
        public ParkingBoothException(string message) : base(message)
        {

        }
    }
}