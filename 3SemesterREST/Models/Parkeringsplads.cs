using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _3SemesterREST.Models
{
    public class Parkeringsplads
    {
        public int Id { get; set; }

        public int IsBooked { get; set; }

        public int IsIn { get; set; }  
    }
}