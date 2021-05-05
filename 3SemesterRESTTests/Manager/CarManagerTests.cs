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
            CarManager manager = new CarManager();
            List<Car> allCars = manager.GetAll().ToList();


            // Add
            //Car data = new Car { NumberOfCars = 1, ColorOfCar = "Red", IsIn = 1 };
            //7Car newCar = manager.Add(data);
            //Assert.IsTrue(newCar.Id > 0);
            //Assert.AreEqual(data.NumberOfCars, newCar.NumberOfCars);
            //Assert.AreEqual(data.ColorOfCar, newCar.ColorOfCar);
            //Assert.AreEqual(data.IsIn, newCar.IsIn);

            //Car nullModelData = new Car { NumberOfCars = 1, ColorOfCar = "Red", IsIn = 1 }; ;
            //Assert.ThrowsException<NullReferenceException>(() => manager.Add(nullModelData));

            // GetById
            //Car carById = manager.GetById(newCar.Id);
            //Assert.AreEqual(newCar.Id, carById.Id);
            //Assert.AreEqual(newCar.NumberOfCars, carById.NumberOfCars);
            //Assert.AreEqual(newCar.ColorOfCar, carById.ColorOfCar);
            //Assert.AreEqual(newCar.IsIn, carById.IsIn);

            //Assert.IsNull(manager.GetById(newCar.Id + 1);

        }

    }
}
