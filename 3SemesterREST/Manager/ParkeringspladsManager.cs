using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _3SemesterREST.Models;
using Microsoft.EntityFrameworkCore;

namespace _3SemesterREST.Manager
{
    public class ParkeringspladsManager
    {
        private readonly ParkeringspladsContext _context;

        public ParkeringspladsManager(ParkeringspladsContext context)
        {
            _context = context;
        }

        public ParkeringspladsManager()
        {

        }

        public IEnumerable<Parkeringsplads> GetAllParkeringspladser()
        {
            return _context.Parkeringspladser;
        }

        public Parkeringsplads GetParkeringspladsById(int id)
        {
            return _context.Parkeringspladser.Find(id);
        }

        public Parkeringsplads AddParkeringsplads(Parkeringsplads newParkeringsplads)
        {
            try
            {
                _context.Parkeringspladser.Add(newParkeringsplads);
                _context.SaveChanges();
                return newParkeringsplads;
            }
            catch (DbUpdateException ex)
            {
                _context.Parkeringspladser.Remove(newParkeringsplads);
                throw new ParkeringspladsException(ex.InnerException.Message);
            }
        }

        public Parkeringsplads UpdateParkeringsplads(int id, Parkeringsplads updates)
        {
            try
            {
                Parkeringsplads parkeringsplads = _context.Parkeringspladser.Find(id);
                if (parkeringsplads == null) return null;
                parkeringsplads.Id = updates.Id;
                parkeringsplads.IsBooked = updates.IsBooked;
                parkeringsplads.IsOccupied = updates.IsOccupied;
                _context.Entry(parkeringsplads).State = EntityState.Modified;
                _context.SaveChanges();
                return parkeringsplads;
            }
            catch (DbUpdateException ex)
            {
                throw new BookingException(updates.Id + " " + updates.IsBooked + " " + updates.IsOccupied + " " + ex.InnerException.Message);
            }
        }

        public Parkeringsplads DeleteParkeringsplads(int id)
        {
            Parkeringsplads parkeringsplads = _context.Parkeringspladser.Find(id);
            if (parkeringsplads == null) return null;
            _context.Parkeringspladser.Remove(parkeringsplads);
            _context.SaveChanges();
            return parkeringsplads;
        }
    }
}
