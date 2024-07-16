using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaitExercises
{
    public class WorkingWithAlerts
    {
        WebDriver driver;


        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();

            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/javascript_alerts");

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }

        [Test]
        public void HandleBasicAlert()
        {
            driver.FindElement(By.XPath("//ul//li//button[contains(text(), 'Click for JS Alert')]")).Click();

            IAlert alert = driver.SwitchTo().Alert();
            Assert.That(alert.Text, Is.EqualTo("I am a JS Alert"), "Alert text is not as expected.");

            alert.Accept();

            IWebElement resultElement = driver.FindElement(By.Id("result"));
            Assert.That(resultElement.Text, Is.EqualTo("You successfully clicked an alert"),
                "Result message is not as expected.");
        }

        [Test]
        public void HandleConfirmAlert()
        {
            driver.FindElement(By.XPath("//ul//li//button[contains(text(), 'Click for JS Confirm')]")).Click();

            IAlert alert = driver.SwitchTo().Alert();

            Assert.That(alert.Text, Is.EqualTo("I am a JS Confirm"), "Alert text is not as expected");

            alert.Accept();

            IWebElement resultElement = driver.FindElement(By.Id("result"));
            Assert.That(resultElement.Text, Is.EqualTo("You clicked: Ok"),
                "Result message is not as expected after accepting the alert.");

            driver.FindElement(By.XPath("//ul//li//button[contains(text(), 'Click for JS Confirm')]")).Click();

            alert = driver.SwitchTo().Alert();

            alert.Dismiss();

            resultElement = driver.FindElement(By.Id("result"));
            Assert.That(resultElement.Text, Is.EqualTo("You clicked: Cancel"),
                "Result message is not as expected after dismissing the alert.");
        }

        [Test]
        public void HandlePromptAlert()
        {
            driver.FindElement(By.XPath("//ul//li//button[contains(text(), 'Click for JS Prompt')]")).Click();

            IAlert alert = driver.SwitchTo().Alert();

            Assert.That(alert.Text, Is.EqualTo("I am a JS prompt"),
                "Alert text is not as expected.");

            string inputTtext = "Hello there!";
            alert.SendKeys(inputTtext);

            alert.Accept();

            IWebElement resultElement = driver.FindElement(By.Id("result"));
            Assert.That(resultElement.Text, Is.EqualTo("You entered: " + inputTtext),
                "Result message is not as expected after entering text in the prompt.");
        }
    }
}
