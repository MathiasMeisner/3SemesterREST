using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _3SemesterREST.Models;
using Microsoft.EntityFrameworkCore;

namespace _3SemesterREST.Manager
{
    public class ParkeringspladsContext : DbContext
    {
        public ParkeringspladsContext(DbContextOptions<ParkeringspladsContext> options) : base(options)
        {

        }

        public DbSet<Parkeringsplads> Parkeringspladser { get; set; }
    }
}