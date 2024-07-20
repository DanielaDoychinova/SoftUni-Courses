using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace WebDriverWaitLab
{
    public class WebDriverWaitTests
    {

        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.selenium.dev/selenium/web/dynamic.html");
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }

        [Test]
        public void AddBoxWithoutWaitsFails()
        {
            driver.FindElement(By.XPath("//input[@id='adder']")).Click();

            IWebElement newBox = driver.FindElement(By.XPath("//div[@id='box0']"));

            Assert.True(newBox.Displayed);
        }

        [Test]
        public void RevealInputWithoutWaitsFails()
        {
            driver.FindElement(By.XPath("//input[@id='reveal']")).Click();

            IWebElement revealed = driver.FindElement(By.XPath("//input[@id='revealed']"));

            Assert.True(revealed.Displayed);
        }

        [Test]
        public void AddBoxWithThreadSleep()
        {
            driver.FindElement(By.XPath("//input[@id='adder']")).Click();

            Thread.Sleep(3000);

            IWebElement newBox = driver.FindElement(By.XPath("//div[@id='box0']"));

            Assert.True(newBox.Displayed);
        }

        [Test]
        public void AddBoxImplicitWait()
        {
            driver.FindElement(By.XPath("//input[@id='adder']")).Click();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            IWebElement newBox = driver.FindElement(By.XPath("//div[@id='box0']"));

            Assert.True(newBox.Displayed);
        }

        [Test]
        public void RevealInputImplicitWait()
        {
            driver.FindElement(By.XPath("//input[@id='reveal']")).Click();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            IWebElement revealed = driver.FindElement(By.XPath("//input[@id='revealed']"));
        }

        [Test]
        public void AddBoxExplicitWait()
        {
            driver.FindElement(By.XPath("//input[@id='adder']")).Click();

            WebDriverWait wait = new(driver, TimeSpan.FromSeconds(3));
            IWebElement newBox = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@id='box0']")));
            
            Assert.True(newBox.Displayed);
        }

        [Test]
        public void RevealInputWithExplicitWait()
        {
            driver.FindElement(By.XPath("//input[@id='reveal']")).Click();
            WebDriverWait wait = new(driver, TimeSpan.FromSeconds(3));

            IWebElement revealed = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//input[@id='revealed']")));

            Assert.True(revealed.Displayed);
        }

        [Test]
        public void AddBoxWithFluentWaitExpectedConditionsAndIgnoredExceptions()
        {
            driver.FindElement(By.XPath("//input[@id='adder']")).Click();

            WebDriverWait wait = new(driver, TimeSpan.FromSeconds(10));
            wait.PollingInterval = TimeSpan.FromMilliseconds(500);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));

            IWebElement newBox = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@id='box0']")));

            Assert.True(newBox.Displayed);
        }

        [Test]
        public void AddBoxWithoutWaaitsThrowsNoSuchElement()
        {
            driver.FindElement(By.XPath("//input[@id='adder']")).Click();

            Assert.Throws<NoSuchElementException>(() =>
            {
                driver.FindElement(By.XPath("//div[@id='box0']"));
            });
        }
    }
}

