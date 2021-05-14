using Microsoft.VisualStudio.TestTools.UnitTesting;
using _3SemesterREST.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3SemesterREST.Models;
using Microsoft.EntityFrameworkCore;

namespace _3SemesterREST.Manager.Tests
{
    [TestClass()]
    public class CarManagerTests
    {
        private readonly CarContext _context;

        public CarManagerTests()
        {
            DbContextOptionsBuilder<CarContext> options = new DbContextOptionsBuilder<CarContext>();
            options.UseSqlServer(Secrets.ConnectionString);
            _context = new CarContext(options.Options);
        }


        [TestMethod()]
        public void TestItAll()
        {
            CarManager manager = new CarManager(_context);
            List<Car> allCars = manager.GetAllCars().ToList();

            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string numbers = "0123456789";
            var stringChars = new char[7];
            var random = new Random();

            for (int i = 0; i < 2; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }
            for (int i = 2; i < 7; i++)
            {
                stringChars[i] = numbers[random.Next(numbers.Length)];
            }

            string licensePlateString = new String(stringChars);

            // Add
            Car data = new Car { Color = "Red", LicensePlate = licensePlateString };
            Car newCar = manager.AddCar(data);
            Assert.IsTrue(newCar.Id > 0);
            Assert.AreEqual(data.Color, newCar.Color);
            Assert.AreEqual(data.LicensePlate, newCar.LicensePlate);
            
            Car nullModelData = new Car();
            Assert.ThrowsException<CarException>(() => manager.AddCar(nullModelData));
            
            // GetById
            Car carById = manager.GetCarById(newCar.Id);
            Assert.AreEqual(newCar.Id, carById.Id);
            Assert.AreEqual(newCar.Color, carById.Color);
            Assert.AreEqual(newCar.LicensePlate, carById.LicensePlate);

            Assert.IsNull(manager.GetCarById(newCar.Id + 1));
            
            // Update
            Car updates = new Car { Color = "Volvo" };
            int id = newCar.Id;
            Car updatedCar = manager.UpdateCar(id, updates);
            Assert.AreEqual(id, updatedCar.Id);
            Assert.AreEqual(updates.Color, updatedCar.Color);

            Assert.IsNull(manager.UpdateCar(id + 1, updates));
            //Assert.ThrowsException<CarException>(() => manager.UpdateCar(id, nullModelData));
            /*
            //Delete all
            foreach(var d in manager.GetAllCars().ToList())
            {
                manager.DeleteCar(d.Id);
            }*/

        }

    }
}
