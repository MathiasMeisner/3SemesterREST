using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _3SemesterREST.Models;
using Microsoft.EntityFrameworkCore;

namespace _3SemesterREST.Manager
{
    public class ParkingBoothContext : DbContext
    {
        public ParkingBoothContext(DbContextOptions<ParkingBoothContext> options) : base(options)
        {

        }

        public DbSet<ParkingBooth> ParkingBooths { get; set; }
    }
}