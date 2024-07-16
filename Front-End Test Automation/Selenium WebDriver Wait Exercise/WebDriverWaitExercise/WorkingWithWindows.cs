using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Support.UI;


namespace WaitExercises
{
    public class WorkingWithWindows
    {
        WebDriver driver;


        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();

            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/windows");

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }

        [Test]
        public void HandleMultipleWindows()
        {
            driver.FindElement(By.XPath("//div[@class='example']//a")).Click();

            ReadOnlyCollection<string> windowHandles = driver.WindowHandles;

            Assert.That(windowHandles.Count, Is.EqualTo(2), "There should be two windows opened.");

            driver.SwitchTo().Window(windowHandles[1]);

            string newWindowContent = driver.PageSource;
            Assert.True(newWindowContent.Contains("New Window"),
                "The content of the window is not as expected.");

            string path = Path.Combine(Directory.GetCurrentDirectory(), "windows.txt");
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            File.AppendAllText(path, "Window hadle for new widow: " + driver.CurrentWindowHandle + "\n\n");
            File.AppendAllText(path, "The page content: " + newWindowContent + "\n\n");

            driver.Close();

            driver.SwitchTo().Window(windowHandles[0]);

            string originalWindowContent = driver.PageSource;
            Assert.True(originalWindowContent.Contains("Opening a new window"),
                "The content of the original window is not as expected.");

            File.AppendAllText(path, "Window handle for original window: " + driver.CurrentWindowHandle + "\n\n");
            File.AppendAllText(path, "The page content: " + originalWindowContent + "\n\n");
        }

        [Test]
        public void HandleNoSuchWindowExeption()
        {
            driver.FindElement(By.XPath("//div[@class='example']//a")).Click();

            ReadOnlyCollection<string> windowHandles = driver.WindowHandles;

            driver.SwitchTo().Window(windowHandles[1]);

            driver.Close();

            try
            {
                driver.SwitchTo().Window(windowHandles[1]);
            }
            catch (NoSuchWindowException ex)
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "windows.txt");
                File.AppendAllText(path, "NoSuchWindowExeption caught: " + ex.Message + "\n\n");
                Assert.Pass("NoSuchWindowExeption was correctly handled.");
            }
            catch (Exception ex)
            {
                Assert.Fail("An unexpected exeption was thrown: " + ex.Message);
            }
            finally
            {
                driver.SwitchTo().Window(windowHandles[0]);
            }
        }
    }
}