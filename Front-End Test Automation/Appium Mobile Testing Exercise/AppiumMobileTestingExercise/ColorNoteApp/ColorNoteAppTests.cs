using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Service;

namespace ColorNoteApp
{
    public class ColorNoteAppTests
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

            var androidOptions = new AppiumOptions()
            {
                PlatformName = "Android",
                AutomationName = "UiAutomator2",
                DeviceName = "Test Phone",
                App = @"C:\\Users\\Daniela\\Desktop\\Front-End Test Automation\\Front-End Test Automation\\9. Appium Mobile Part 1 EXERCISE\\Notepad.apk",
                PlatformVersion = "7.0"
            };
            androidOptions.AddAdditionalAppiumOption("autoGrantPermissions", true);

            _driver = new AndroidDriver(_appiumLocalService, androidOptions);

            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            try
            {
                var skipTutorialBtn = _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/btn_start_skip"));
                skipTutorialBtn.Click();
            }
            catch (NoSuchElementException)
            {

            }
        }

        [OneTimeTearDown]
        public void Teardown()
        {
            _driver.Quit();
            _driver.Dispose();
            _appiumLocalService.Dispose();
        }

        public void PressBackButton()
        {
            _driver.Navigate().Back();
            _driver.Navigate().Back();
            _driver.Navigate().Back();
        }
            [Test, Order (1)]
        public void Test_CreateNewNote()
        {
            IWebElement AddNote = _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/main_btn1"));
            AddNote.Click();

            IWebElement TextNoteBtn = _driver.FindElement(MobileBy.AndroidUIAutomator("new UiSelector().text(\"Text\")"));
            TextNoteBtn.Click();

            IWebElement TextField = _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/edit_note"));
            TextField.SendKeys("Test_1");

            PressBackButton();

            IWebElement createdNote = _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/title"));

            Assert.That(createdNote, Is.Not.Null, "Note was not created.");
            Assert.That(createdNote.Text, Is.EqualTo("Test_1"));
        }

        [Test, Order (2)]
        public void Test_UpdateNote()
        {
            IWebElement AddNote = _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/main_btn1"));
            AddNote.Click();

            IWebElement TextNoteBtn = _driver.FindElement(MobileBy.AndroidUIAutomator("new UiSelector().text(\"Text\")"));
            TextNoteBtn.Click();

            IWebElement TextField = _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/edit_note"));
            TextField.SendKeys("Test_2");

            PressBackButton();

            IWebElement note = _driver.FindElement(MobileBy.AndroidUIAutomator("new UiSelector().text(\"Test_2\")"));

            note.Click();

            IWebElement editBtn = _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/edit_btn"));
            editBtn.Click();

            IWebElement editNote = _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/edit_note"));
            editNote.Clear();
            editNote.SendKeys("Edited note");

            PressBackButton();

            IWebElement editedNote = _driver.FindElement(MobileBy.AndroidUIAutomator("new UiSelector().text(\"Edited note\")"));

            Assert.That(editedNote.Text, Is.EqualTo("Edited note"));
        }

        [Test, Order(3)]
        public void Test_DeleteNote()
        {
            IWebElement AddNote = _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/main_btn1"));
            AddNote.Click();

            IWebElement TextNoteBtn = _driver.FindElement(MobileBy.AndroidUIAutomator("new UiSelector().text(\"Text\")"));
            TextNoteBtn.Click();

            IWebElement TextField = _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/edit_note"));
            TextField.SendKeys("Note for delete");

            PressBackButton();

            IWebElement note = _driver.FindElement(MobileBy.AndroidUIAutomator("new UiSelector().text(\"Note for delete\")"));
            note.Click();

            IWebElement menuBtn = _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/menu_btn"));
            menuBtn.Click();

            IWebElement deleteBtn = _driver.FindElement(MobileBy.AndroidUIAutomator("new UiSelector().text(\"Delete\")"));
            deleteBtn.Click();

            IWebElement confirmDelete = _driver.FindElement(MobileBy.Id("android:id/button1"));
            confirmDelete.Click();

            var deletedNote = _driver.FindElements(By.XPath("//android.widget.TextView[@text='Note for delete']"));
            Assert.That(deletedNote, Is.Empty, "The note was not deleted successfully.");
        }
    }
}