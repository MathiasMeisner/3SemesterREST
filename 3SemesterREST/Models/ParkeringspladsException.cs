using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _3SemesterREST.Models
{
    public class ParkeringspladsException : Exception
    {
        public ParkeringspladsException(string message) : base(message)
        {

        }
    }
}