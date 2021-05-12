using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _3SemesterREST.Models;
using Microsoft.EntityFrameworkCore;

namespace _3SemesterREST.Manager
{
    public class ParkingBoothManager
    {
        private readonly ParkingBoothContext _context;

        public ParkingBoothManager(ParkingBoothContext context)
        {
            _context = context;
        }

        public ParkingBoothManager()
        {

        }

        public IEnumerable<ParkingBooth> GetAllParkingBooths()
        {
            return _context.ParkingBooths;
        }

        public ParkingBooth GetParkingBoothById(int id)
        {
            return _context.ParkingBooths.Find(id);
        }

        public ParkingBooth AddParkingBooth(ParkingBooth newParkingBooth)
        {
            try
            {
                _context.ParkingBooths.Add(newParkingBooth);
                _context.SaveChanges();
                return newParkingBooth;
            }
            catch (DbUpdateException ex)
            {
                _context.ParkingBooths.Remove(newParkingBooth);
                throw new ParkingBoothException(ex.InnerException.Message);
            }
        }

        public ParkingBooth UpdateParkingBooth(int id, ParkingBooth updates)
        {
            try
            {
                ParkingBooth parkingBooth = _context.ParkingBooths.Find(id);
                if (parkingBooth == null) return null;
                parkingBooth.Id = updates.Id;
                parkingBooth.IsBooked = updates.IsBooked;
                parkingBooth.IsOccupied = updates.IsOccupied;
                _context.Entry(parkingBooth).State = EntityState.Modified;
                _context.SaveChanges();
                return parkingBooth;
            }
            catch (DbUpdateException ex)
            {
                throw new ParkingBoothException(updates.Id + " " + updates.IsBooked + " " + updates.IsOccupied + " " + ex.InnerException.Message);
            }
        }

        public ParkingBooth DeleteParkingBooth(int id)
        {
            ParkingBooth parkingBooth = _context.ParkingBooths.Find(id);
            if (parkingBooth == null) return null;
            _context.ParkingBooths.Remove(parkingBooth);
            _context.SaveChanges();
            return parkingBooth;
        }
    }
}
