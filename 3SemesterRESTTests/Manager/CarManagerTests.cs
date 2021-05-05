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
            List<Car> allCars = manager.GetAll().ToList();


            // Add
            Car data = new Car { ColorOfCar = "Red", IsIn = 1 };
            Car newCar = manager.Add(data);
            Assert.IsTrue(newCar.Id > 0);
            Assert.AreEqual(data.ColorOfCar, newCar.ColorOfCar);
            Assert.AreEqual(data.IsIn, newCar.IsIn);

            Car nullModelData = new Car { ColorOfCar = "Dette er fra test"};
            //Assert.ThrowsException<Exception>(() => manager.Add(nullModelData));

            // GetById
            Car carById = manager.GetById(newCar.Id);
            Assert.AreEqual(newCar.Id, carById.Id);
            Assert.AreEqual(newCar.ColorOfCar, carById.ColorOfCar);
            Assert.AreEqual(newCar.IsIn, carById.IsIn);

            Assert.IsNull(manager.GetById(newCar.Id + 1));

            // Update
            Car updates = new Car { ColorOfCar = "Volvo", IsIn = 1 };
            int id = newCar.Id;
            Car updatedCar = manager.Update(id, updates);
            Assert.AreEqual(id, updatedCar.Id);
            Assert.AreEqual(updates.IsIn, updatedCar.IsIn);

            Assert.IsNull(manager.Update(id + 1, updates));
            //Assert.ThrowsException<CarException>(() => manager.Update(id, nullModelData));

            //Delete all
            /*foreach(var d in manager.GetAll().ToList())
            {
                manager.Delete(d.);
            }*/

        }

    }
}
