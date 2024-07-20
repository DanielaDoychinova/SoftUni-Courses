using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Internal;

namespace AppiumTestingLab
{
    public class Tests
    {
        private AndroidDriver driver;
        private AppiumLocalService service;

        [SetUp]
        public void Setup()
        {
            service = new AppiumServiceBuilder().WithIPAddress("127.0.0.1").UsingPort(4723).Build();

            AppiumOptions options = new AppiumOptions();

            options.App = @"C:\Users\Daniela\Desktop\SoftUni Courses\SoftUni-Courses\Front-End Test Automation\Appium Testing Lab\com.example.androidappsummator.apk";
            options.PlatformName = "Android";
            options.DeviceName = "Test Phone";
            options.AutomationName = "UiAutomator2";

            driver = new AndroidDriver(service, options);
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
            service.Dispose();
        }

        [Test]
        public void TestValidSumation()
        {
            var firstInput = driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText1"));
            firstInput.Clear();
            firstInput.SendKeys("2");

            var secondInput = driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText2"));
            secondInput.Clear();
            secondInput.SendKeys("3");

            var calcButton = driver.FindElement(MobileBy.ClassName("android.widget.Button"));
            calcButton.Click();

            var result = driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editTextSum"));

            Assert.That(result.Text, Is.EqualTo("5"));
        }

        [Test]
        public void TestInvalidSumation()
        {
            var firstInput = driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText1"));
            firstInput.Clear();
            firstInput.SendKeys("e");

            var secondInput = driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText2"));
            secondInput.Clear();
            secondInput.SendKeys("3");

            var calcButton = driver.FindElement(MobileBy.ClassName("android.widget.Button"));
            calcButton.Click();

            var result = driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editTextSum"));

            Assert.That(result.Text, Is.EqualTo("error"));
        }
    }
}