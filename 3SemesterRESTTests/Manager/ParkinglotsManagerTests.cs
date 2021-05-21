using Microsoft.VisualStudio.TestTools.UnitTesting;
using _3SemesterREST.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3SemesterREST.Models;
using _3SemesterREST.Controllers;

namespace _3SemesterREST.Manager.Tests
{
    [TestClass()]
    public class ParkinglotsManagerTests
    {
        Parkinglots parkinglots;
        ParkinglotsController controller;
        ParkinglotsManager manager;

        [TestInitialize]
        public void StartTest()
        {
            parkinglots = new Parkinglots();
            controller = new ParkinglotsController();
            manager = new ParkinglotsManager();
        }

        [TestMethod()]
        public void GetAllTest()
        {
            var getall = controller.Get();
            Assert.AreNotEqual(controller.Get(), getall);
        }

        [TestMethod()]
        public void GetCarsByDayTest()
        {
            int count = 0;
            Assert.AreEqual(count,controller.Getbyday(2021,05,14));
        }

        [TestMethod()]
        public void AddTest()
        {

            Parkinglots parkinglots = new Parkinglots {isin = 1, day = DateTime.Today };
            Parkinglots test = manager.Add(parkinglots);
            Assert.AreEqual(parkinglots.isin, test.isin);
            Assert.AreEqual(parkinglots.day, test.day);
            int count = controller.Getbyday(2021,05,14);
            Assert.AreEqual(count, controller.Getbyday(2021,05,14));
        }
    }
}