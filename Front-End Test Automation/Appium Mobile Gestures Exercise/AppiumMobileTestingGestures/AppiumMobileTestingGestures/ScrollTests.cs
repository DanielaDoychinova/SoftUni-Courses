using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Service;

namespace AppiumMobileTestingGestures
{
    public class ScrollTests
    {
        private AndroidDriver _driver;
        private AppiumLocalService _appiumLocalService;

        [OneTimeSetUp]
        public void SetUp()
        {
            _appiumLocalService = new AppiumServiceBuilder()
                .WithIPAddress("127.0.0.1")
                .UsingPort(4723)
                .Build();
            _appiumLocalService.Start();

            var androidOptions = new AppiumOptions
            {
                PlatformName = "Android",
                AutomationName = "UIAutomator2",
                DeviceName = "Test Phone",
                App = @"C:\Users\Daniela\Desktop\Front-End Test Automation\Front-End Test Automation\10. Appium Mobile Part 2 EXERCISE\ApiDemos-debug.apk"
            };


            _driver = new AndroidDriver(_appiumLocalService, androidOptions);
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _driver?.Quit();
            _driver?.Dispose();
            _appiumLocalService?.Dispose();
        }


        [Test]
        public void ScrollTest()
        {
            ScrollToText("Views");

            IWebElement viewsBtn = _driver.FindElement(MobileBy.AccessibilityId("Views"));
            viewsBtn.Click();

            ScrollToText("Lists");

            IWebElement listsBtn = _driver.FindElement(MobileBy.AccessibilityId("Lists"));

            Assert.That(listsBtn, Is.Not.Null, "The Lists element is not present.");

            listsBtn.Click();

            IWebElement photosBtn = _driver.FindElement(MobileBy.AccessibilityId("08. Photos"));

            Assert.That(photosBtn, Is.Not.Null, "The Photos element is not present.");

        }


        private void ScrollToText(string text)
        {
            _driver.FindElement(MobileBy.AndroidUIAutomator("new UiScrollable(new UiSelector().scrollable(true)).scrollIntoView(new UiSelector().text(\"" + text + "\"))"));
        }
    }
}