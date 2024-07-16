using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumTwoNumbers
{
    public class SumNumberPageTests
    {
        private ChromeDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }

        [Test]
        public void AddTwoNumbers_ValidInput()
        {
            var calculatorPage = new SumNumberPage(driver);
            calculatorPage.openPage();
            string result = calculatorPage.AddNumbers("3", "4");

            Assert.AreEqual("Sum: 7", result);
        }

        [Test]
        public void AddTwoNumbers_InvalidInput()
        {
            var calculatorPage = new SumNumberPage(driver);
            calculatorPage.openPage();
            string result = calculatorPage.AddNumbers("hello", "world");

            Assert.AreEqual("Sum: invalid input", result);
        }

        [Test]
        public void ResetForm()
        {
            var calculatorPage = new SumNumberPage(driver);
            calculatorPage.openPage();
            string result = calculatorPage.AddNumbers("3", "7");

            Assert.AreEqual("Sum: 10", result);

            calculatorPage.ResetForm();

            Assert.True(calculatorPage.IsFormEmpty());
            Assert.AreEqual("1st Number", calculatorPage.FieldNum1.GetAttribute("placeholder"));
        }
    }
}
