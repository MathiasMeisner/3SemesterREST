using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _3SemesterREST.Models
{
    public class Car
    {
        public int Id { get; set; }
        //public int NumberOfCars { get; set; }
        public string ColorOfCar { get; set; }
        public string LicensePlate { get; set; }
        public int IsIn { get; set; }
        public DateTime TodayDate { get; set; }
    }
}
