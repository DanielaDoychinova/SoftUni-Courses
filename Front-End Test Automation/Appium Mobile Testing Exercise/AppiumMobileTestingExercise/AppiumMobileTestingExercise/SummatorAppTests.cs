using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Internal;

namespace AppiumMobileTestingSummator
{
    public class SummatorAppTests
    {
        private AndroidDriver _driver;
        private AppiumLocalService _appiumLocalService;

        [OneTimeSetUp]
        public void Setup()
        {
            _appiumLocalService = new AppiumServiceBuilder()
                .WithIPAddress("127.0.0.1")
                .UsingPort(4723)
                .Build();
            _appiumLocalService.Start();

            var androidOptions = new AppiumOptions
            {
                PlatformName = "Android",
                AutomationName = "UiAutomator2",
                DeviceName = "Test Phone",
                App = @"C:\\Users\\Daniela\\Desktop\\Front-End Test Automation\\Front-End Test Automation\\9. Appium Mobile Part 1 EXERCISE\\com.example.androidappsummator.apk",
                PlatformVersion = "7.0"
            };

            _driver = new AndroidDriver(_appiumLocalService, androidOptions);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _driver?.Quit();
            _driver?.Dispose();
            _appiumLocalService?.Dispose();
        }

        [Test]
        public void TestWithValidData()
        {
            IWebElement field1 = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText1"));
            field1.Clear();
            field1.SendKeys("2");

            IWebElement field2 = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText2"));
            field2.Clear();
            field2.SendKeys("5");

            IWebElement sumBtn = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/buttonCalcSum"));
            sumBtn.Click();

            IWebElement result = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editTextSum"));

            Assert.That(result.Text, Is.EqualTo("7"));
        }

        [Test]
        public void TestWithInvalidData()
        {
            IWebElement field1 = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText1"));
            field1.Clear();

            IWebElement field2 = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText2"));
            field2.Clear();

            IWebElement sumBtn = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/buttonCalcSum"));
            sumBtn.Click();

            IWebElement result = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editTextSum"));

            Assert.That(result.Text, Is.EqualTo("error"));

        }

        [Test]
        public void TestFillOnlyFirstField()
        {
            IWebElement field1 = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText1"));
            field1.Clear();
            field1.SendKeys("7");

            IWebElement field2 = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText2"));
            field2.Clear();

            IWebElement sumBtn = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/buttonCalcSum"));
            sumBtn.Click();

            IWebElement result = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editTextSum"));

            Assert.That(result.Text, Is.EqualTo("error"));
        }

        [Test]
        [TestCase("10", "10", "20")]
        [TestCase("100", "10", "110")]
        [TestCase("99", "2", "101")]
        [TestCase("0", "1000", "1000")]
        [TestCase("10000", "10000", "20000")]
        [TestCase("10.9", "0.1", "11.0")]
        [TestCase("8.3", "5.9", "14.2")]
        public void TestWithValidData_Parametrized(string input1, string input2, string expectedResult)
        {
            IWebElement field1 = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText1"));
            field1.Clear();
            field1.SendKeys(input1);

            IWebElement field2 = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText2"));
            field2.Clear();
            field2.SendKeys(input2);

            IWebElement sumBtn = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/buttonCalcSum"));
            sumBtn.Click();

            IWebElement result = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editTextSum"));

            Assert.That(result.Text, Is.EqualTo(expectedResult));
        }
    }
}