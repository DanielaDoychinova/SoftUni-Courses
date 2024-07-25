using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Interactions;
using OpenQA.Selenium.Interactions;
using System.Drawing;

namespace Sliding
{
    public class SeekBarTests
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
        public void SeekBarTest()
        {
            ScrollToText("Views");

            IWebElement viewBtn = _driver.FindElement(MobileBy.AccessibilityId("Views"));
            viewBtn.Click();

            ScrollToText("Seek Bar");

            IWebElement seekBarBtn = _driver.FindElement(MobileBy.AccessibilityId("Seek Bar"));
            seekBarBtn.Click();

            MoveSeekBarWithInspectorCoordinates(360, 180, 720, 180);

            var resultElement = _driver.FindElement(By.Id("io.appium.android.apis:id/progress"));

            Assert.That(resultElement.Text, Is.EqualTo("100 from touch=true"));

        }

        private void ScrollToText(string text)
        {
            _driver.FindElement(MobileBy.AndroidUIAutomator("new UiScrollable(new UiSelector().scrollable(true)).scrollIntoView(new UiSelector().text(\"" + text + "\"))"));
        }

        private void MoveSeekBarWithInspectorCoordinates(int startX, int startY, int endX, int endY)
        {
            var finger = new OpenQA.Selenium.Appium.Interactions.PointerInputDevice(PointerKind.Touch);
            var start = new Point(startX, startY);
            var end = new Point(endX, endY);
            var swipe = new ActionSequence(finger);

            swipe.AddAction(finger.CreatePointerMove(CoordinateOrigin.Viewport, start.X, start.Y, TimeSpan.Zero));
            swipe.AddAction(finger.CreatePointerDown(MouseButton.Left));
            swipe.AddAction(finger.CreatePointerMove(CoordinateOrigin.Viewport, end.X, end.Y, TimeSpan.FromSeconds(1)));
            swipe.AddAction(finger.CreatePointerUp(MouseButton.Left));
            _driver.PerformActions(new List<ActionSequence> { swipe });
        }
    }
}