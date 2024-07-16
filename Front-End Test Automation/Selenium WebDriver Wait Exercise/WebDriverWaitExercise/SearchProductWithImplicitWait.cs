using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace WaitExercises
{
    public class SearchProductWithImplicitWaitTests
    {
        WebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();

            driver.Navigate().GoToUrl("http://practice.bpbonline.com/");

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }

        [Test, Order(1)]
        public void SearchProduct_Keyboad_ShouldAddToCart()
        {
            driver.FindElement(By.XPath("//input[@name='keywords']")).SendKeys("keyboard");

            driver.FindElement(By.XPath("//input[@title=' Quick Find ']")).Click();

            try
            {
                driver.FindElement(By.LinkText("Buy Now")).Click();

                Assert.True(driver.PageSource.Contains("keyboard"),
                    "The product 'keyboard' was not found in the cart page.");
                Console.WriteLine("Scenario completed!");
            }

            catch (Exception ex)
            {
                Assert.Fail("Unexpected exeption: " + ex.Message);
            }
        }

        [Test, Order(2)]
        public void SearchProduct_Junk_ShouldThrowNoSuchElementExeption()
        {
            driver.FindElement(By.XPath("//input[@name='keywords']")).SendKeys("junk");

            driver.FindElement(By.XPath("//input[@title=' Quick Find ']")).Click();

            try
            {
                driver.FindElement(By.LinkText("Buy Now")).Click();
            }

            catch (NoSuchElementException ex)
            {
                Assert.Pass("Expected NoSuchElementExeption was thrown.");
                Console.WriteLine("Timeout - " + ex.Message);
            }

            catch (Exception ex)
            {
                Assert.Fail("Unexpected exeption: " + ex.Message);
            }
        }
    }
}