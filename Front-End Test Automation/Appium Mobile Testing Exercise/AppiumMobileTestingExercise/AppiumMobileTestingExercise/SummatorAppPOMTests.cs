using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium;

namespace AppiumMobileTestingSummator
{
    public class SummatorAppPOMTests
    {
        private AndroidDriver _driver;
        private AppiumLocalService _appiumLocalService;
        private SummatorPage _summatorPage;


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
            _summatorPage = new SummatorPage(_driver);
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
            var result = _summatorPage.Calculate("2", "5");

            Assert.That(result, Is.EqualTo("7"));
        }

        [Test]
        public void TestWithInvalidData()
        {
            _summatorPage.ClearFields();

            _summatorPage.SumBtn.Click();

            Assert.That(_summatorPage.Result.Text, Is.EqualTo("error"));

        }

        [Test]
        public void TestFillOnlyFirstField()
        {
            _summatorPage.ClearFields();
            _summatorPage.Field1.SendKeys("7");
            _summatorPage.SumBtn.Click();

            Assert.That(_summatorPage.Result.Text, Is.EqualTo("error"));
        }

        [Test]
        public void TestFillOnlySecondField()
        {
            _summatorPage.ClearFields();
            _summatorPage.Field2.SendKeys("7");
            _summatorPage.SumBtn.Click();

            Assert.That(_summatorPage.Result.Text, Is.EqualTo("error"));
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
            var result = _summatorPage.Calculate(input1, input2);
            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}
