﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _3SemesterREST.Models;
using Microsoft.EntityFrameworkCore;

namespace _3SemesterREST.Manager
{
    public class CarManager
    {
        private readonly CarContext _context;

        public CarManager(CarContext context)
        {
            _context = context;
        }

        public IEnumerable<Car> GetAll()
        {
            return _context.Cars;
        }

        public Car GetById(int id)
        {
            return _context.Cars.Find(id);
        }

        public Car Add(Car newCar)
        {
            try
            {
                _context.Cars.Add(newCar);
                _context.SaveChanges();
                return newCar;
            }
            catch (DbUpdateException ex)
            {
                _context.Cars.Remove(newCar);
                throw new CarException(ex.InnerException.Message);

            }
        }

        public Car Delete(int id)
        {
            Car car = _context.Cars.Find(id);
            if (car == null) return null;
            _context.Cars.Remove(car);
            _context.SaveChanges();
            return car;
        }

        public Car Update(int id, Car updates)
        {
            try
            {
                Car car = _context.Cars.Find(id);
                if (car == null) return null;
                car.IsIn = updates.IsIn;
                _context.Entry(car).State = EntityState.Modified;
                _context.SaveChanges();
                return car;
            }
            catch (DbUpdateException ex)
            {
                throw new CarException(updates.IsIn + " " + ex.InnerException.Message);
            }
        }
    }
}
