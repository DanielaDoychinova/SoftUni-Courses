using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace CalculatorProject
{
    public class Tests
    {
        WebDriver driver;
        IWebElement textBoxNumber1;
        IWebElement textBoxNumber2;
        IWebElement dropDownOperations;
        IWebElement calculateButton;
        IWebElement resetButton;
        IWebElement divResult;


        [OneTimeSetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://softuni-qa-loadbalancer-2137572849.eu-north-1.elb.amazonaws.com/number-calculator/");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            textBoxNumber1 = driver.FindElement(By.Id("number1"));
            textBoxNumber2 = driver.FindElement(By.Id("number2"));
            dropDownOperations = driver.FindElement(By.XPath("//label[@for='operation']" +
                "//following-sibling::select"));
            calculateButton = driver.FindElement(By.Id("calcButton"));
            resetButton = driver.FindElement(By.Id("resetButton"));
            divResult = driver.FindElement(By.Id("result"));
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }

        public void PerformTestLogic(string firstNumber, string secondNumber,
            string operation, string expected)
        {
            resetButton.Click();

            if (!string.IsNullOrEmpty(firstNumber))
            {
                textBoxNumber1.SendKeys(firstNumber);
            }

            if (!string.IsNullOrEmpty(secondNumber))
            {
                textBoxNumber2.SendKeys(secondNumber);
            }

            if (!string.IsNullOrEmpty(operation))
            {
                new SelectElement(dropDownOperations).SelectByText(operation);

                calculateButton.Click();

                Assert.That(divResult.Text, Is.EqualTo(expected));

            }
        }

        [Test]
        [TestCase("5", "+ (sum)", "10", "Result: 15")]
        [TestCase("5", "- (subtract)", "10", "Result: -5")]
        [TestCase("5", "* (multiply)", "2", "Result: 10")]
        [TestCase("10", "/ (divide)", "2", "Result: 5")]
        public void Test1(string firstNumber, string operation, string secondNumber, string expected)
        {
            PerformTestLogic(firstNumber, secondNumber, operation, expected);
        }
    }
}