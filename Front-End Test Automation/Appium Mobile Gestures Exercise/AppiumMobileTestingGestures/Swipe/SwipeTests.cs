using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Interactions;

namespace Swipe
{
    public class SwipeTests
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
        public void SwipeTest()
        {
            ScrollToText("Views");

            IWebElement viewBtn = _driver.FindElement(MobileBy.AccessibilityId("Views"));
            viewBtn.Click();

            ScrollToText("Gallery");

            IWebElement gelleryBtn = _driver.FindElement(MobileBy.AccessibilityId("Gallery"));
            gelleryBtn.Click();

            IWebElement photosBtn = _driver.FindElement(MobileBy.AccessibilityId("1. Photos"));
            photosBtn.Click();

            var firstImg = _driver.FindElements(MobileBy.ClassName("android.widget.ImageView"))[0];

            var actions = new Actions(_driver);
            var swipe = actions.ClickAndHold(firstImg)
                .MoveByOffset(-200, 0)
                .Release()
                .Build();
            swipe.Perform();

            var thirdImg = _driver.FindElements(MobileBy.ClassName("android.widget.ImageView"))[2];

            Assert.That(thirdImg, Is.Not.Null, "Third image is not visible.");
        }

        private void ScrollToText(string text)
        {
            _driver.FindElement(MobileBy.AndroidUIAutomator("new UiScrollable(new UiSelector().scrollable(true)).scrollIntoView(new UiSelector().text(\"" + text + "\"))"));
        }
    }
}