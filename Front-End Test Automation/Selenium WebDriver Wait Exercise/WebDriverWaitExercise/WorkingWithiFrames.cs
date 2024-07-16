using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumExtras.WaitHelpers;
using System.Security.Cryptography.X509Certificates;

namespace WaitExercises
{
    public class WorkingWithiFrames
    {

        WebDriver driver;
        WebDriverWait wait;


        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();

            driver.Navigate().GoToUrl("https://codepen.io/pervillalva/full/abPoNLd");

            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }

        [Test]
        public void HandlingiFramesByIndex()
        {
            wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(By.TagName("iframe")));

            var dropDownButton = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".dropbtn")));
            dropDownButton.Click();

            var dropdownLinks = wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.CssSelector(".dropdown-content a")));

            foreach (var link in dropdownLinks)
            {
                Console.WriteLine(link.Text);
                Assert.True(link.Displayed, "Link inside the dropdown is not displayed as expected.");
            }

            driver.SwitchTo().DefaultContent();
        }

        [Test]
        public void HandlingiFramesById()
        {
            wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt("result"));

            var dropdownButton = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".dropbtn")));
            dropdownButton.Click();

            var dropdownLinks = wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.CssSelector(".dropdown-content a")));

            foreach (var link in dropdownLinks)
            {
                Console.WriteLine(link.Text);
                Assert.True(link.Displayed, "Link inside the dropdown is not displayed as expected.");
            }

            driver.SwitchTo().DefaultContent();
        }

        [Test]
        public void HandlingiFramesByWebElement()
        {
            var frameElement = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#result")));

            driver.SwitchTo().Frame(frameElement);

            var dropdownButton = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".dropbtn")));
            dropdownButton.Click();

            var dropdownLinks = wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.CssSelector(".dropdown-content a")));

            foreach (var link in dropdownLinks)
            {
                Console.WriteLine(link.Text);
                Assert.True(link.Displayed, "Link inside the dropdown is not displayed as expected.");
            }


        }

    }
}