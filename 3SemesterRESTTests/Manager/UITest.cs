using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3SemesterRESTTests.Manager
{
    [TestClass]
    public class UITest
    {
        private static readonly string DriverDirectory = "C:/Users/herso/OneDrive/Skrivebord/3. semester/Programming/Webdrivers/chromedriver_win32";
        private static IWebDriver driver;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            driver = new ChromeDriver(DriverDirectory);
        }

        [ClassCleanup]
        public static void TearDown()
        {
            driver.Dispose();
        }

        [TestMethod]
        public void TestWebPage()
        {
            driver.Navigate().GoToUrl("file:///C:/Users/herso/OneDrive/Skrivebord/3.%20semester/3SemesterREST/WebPage/ProductOwner.html");
            Assert.AreEqual("Document", driver.Title);

            IWebElement Day = driver.FindElement(By.Id("inputfield1"));
            Day.SendKeys("14");

            IWebElement Month = driver.FindElement(By.Id("Month"));
            Month.SendKeys("05");

            IWebElement Year = driver.FindElement(By.Id("Year"));
            Year.SendKeys("2021");

            IWebElement Button = driver.FindElement(By.Id("Button"));
            Button.Click();

            IWebElement Output = driver.FindElement(By.Id("Output"));
            string Text = Output.Text;

            Assert.AreEqual("", Text);

        }
    }
}
