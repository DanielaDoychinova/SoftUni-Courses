using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace DragAndDrop
{
    public class DragAndDropTests
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
        public void DragAndDropTest()
        {
            ScrollToText("Views");

            IWebElement viewBtn = _driver.FindElement(MobileBy.AccessibilityId("Views"));
            viewBtn.Click();

            IWebElement dragAndDropBtn = _driver.FindElement(MobileBy.AccessibilityId("Drag and Drop"));
            dragAndDropBtn.Click();

            AppiumElement dotDrag = _driver.FindElement(MobileBy.Id("drag_dot_1"));

            AppiumElement dotDrop = _driver.FindElement(MobileBy.Id("drag_dot_2"));

            var scriptArgs = new Dictionary<string, object>
            {
                {"elementId", dotDrag.Id},
                {"endX", dotDrop.Location.X + (dotDrop.Size.Width/2)},
                {"endY", dotDrop.Location.X + (dotDrop.Size.Height/2) },
                {"speed", 2500 }
            };

            _driver.ExecuteScript("mobile: dragGesture", scriptArgs);

            var message = _driver.FindElement(By.Id("drag_result_text"));

            Assert.That(message.Text, Is.EqualTo("Dropped!"), "The element was not dropped.");
        }

        private void ScrollToText(string text)
        {
            _driver.FindElement(MobileBy.AndroidUIAutomator("new UiScrollable(new UiSelector().scrollable(true)).scrollIntoView(new UiSelector().text(\"" + text + "\"))"));
        }
    }

}