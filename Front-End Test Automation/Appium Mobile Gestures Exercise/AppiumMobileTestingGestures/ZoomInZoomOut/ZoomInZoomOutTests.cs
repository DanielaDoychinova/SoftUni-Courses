using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System.Drawing;

namespace ZoomInZoomOut
{
    public class ZoomInZoomOutTests
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
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _driver?.Quit();
            _driver?.Dispose();
            _appiumLocalService?.Dispose();
        }

        [Test]
        public void ZoomIn()
        {
            ScrollToText("Views");

            IWebElement viewBtn = _driver.FindElement(MobileBy.AccessibilityId("Views"));
            viewBtn.Click();

            ScrollToText("WebView");

            IWebElement webViewBtn = _driver.FindElement(MobileBy.AccessibilityId("WebView"));
            webViewBtn.Click();

            PerformZoom(350, 560, 230, 330, 390, 600, 480, 680);
        }

        [Test]
        public void ZoomOut()
        {
            ScrollToText("Views");

            IWebElement viewBtn = _driver.FindElement(MobileBy.AccessibilityId("Views"));
            viewBtn.Click();

            ScrollToText("WebView");

            IWebElement webViewBtn = _driver.FindElement(MobileBy.AccessibilityId("WebView"));
            webViewBtn.Click();

            PerformZoom(350, 560, 230, 330, 390, 600, 480, 680);

            PerformZoom(230, 330, 350, 560, 480, 680, 390, 600);
        }

        private void ScrollToText(string text)
        {
            _driver.FindElement(MobileBy.AndroidUIAutomator("new UiScrollable(new UiSelector().scrollable(true)).scrollIntoView(new UiSelector().text(\"" + text + "\"))"));
        }

        private void PerformZoom(int ffStartX, int ffStartY, int ffEndX, int ffEndY, int sfStartX, int sfStartY, int sfEndX, int sfEndY)
        {
            var finger1 = new OpenQA.Selenium.Appium.Interactions.PointerInputDevice(PointerKind.Touch);
            var finger2 = new OpenQA.Selenium.Appium.Interactions.PointerInputDevice(PointerKind.Touch);
            var zoomFinger1 = new ActionSequence(finger1, 0);
            var zoomFinger2 = new ActionSequence(finger2, 1);

            zoomFinger1.AddAction(finger1.CreatePointerMove(CoordinateOrigin.Viewport, ffStartX, ffStartY, TimeSpan.Zero));
            zoomFinger1.AddAction(finger1.CreatePointerDown(MouseButton.Left));
            zoomFinger1.AddAction(finger1.CreatePointerMove(CoordinateOrigin.Viewport, ffEndX, ffEndY, TimeSpan.FromSeconds(1)));
            zoomFinger1.AddAction(finger1.CreatePointerUp(MouseButton.Left));

            zoomFinger2.AddAction(finger2.CreatePointerMove(CoordinateOrigin.Viewport, sfStartX, sfStartY, TimeSpan.Zero));
            zoomFinger2.AddAction(finger2.CreatePointerDown(MouseButton.Left));
            zoomFinger2.AddAction(finger2.CreatePointerMove(CoordinateOrigin.Viewport, sfEndX, sfEndY, TimeSpan.FromSeconds(1)));
            zoomFinger2.AddAction(finger2.CreatePointerUp(MouseButton.Left));

            _driver.PerformActions(new List<ActionSequence> { zoomFinger1, zoomFinger2 });
        }
    }
}