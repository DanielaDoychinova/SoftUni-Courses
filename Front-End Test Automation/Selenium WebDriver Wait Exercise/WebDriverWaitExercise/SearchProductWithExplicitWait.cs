using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace WaitExercises
{
    public class SearchProductWithExplicitWait

    {
        WebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();

            driver.Navigate().GoToUrl("http://practice.bpbonline.com/");

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }

        [Test]
        public void SearchProduct_Keyboard_ShouldAddToCart()
        {
            driver.FindElement(By.XPath("//input[@name='keywords']")).SendKeys("keyboard");

            driver.FindElement(By.XPath("//input[@title=' Quick Find ']")).Click();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);

            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

                IWebElement buyNowLink = wait.Until(ExpectedConditions.ElementIsVisible(By.LinkText("Buy Now")));

                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

                buyNowLink.Click();

                Assert.True(driver.PageSource.Contains("keyboard"), "The product 'keyboard' was not found in the cart page.");
                Console.WriteLine("Scenarion completed!");
            }

            catch (Exception ex)
            {
                Assert.Fail("Unexpected exeption: " + ex.Message);
            }
        }

        [Test]
        public void SearchProduct_Junk_SholdThrowNoSuchElementExeption()
        {
            driver.FindElement(By.XPath("//input[@name='keywords']")).SendKeys("junk");

            driver.FindElement(By.XPath("//input[@title=' Quick Find ']")).Click();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);

            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

                IWebElement buyNowLink = wait.Until(ExpectedConditions.ElementIsVisible(By.LinkText("Buy Now")));
                buyNowLink.Click();

                Assert.Fail("The 'Buy Now' link was found for a non-existing product");
            }

            catch (WebDriverTimeoutException)
            {
                Assert.Pass("Expected WebDriverTimeoutExeption was thrown");
            }

            catch (Exception ex)
            {
                Assert.Fail("Unexpected exeption: " + ex.Message);
            }

            finally
            {
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            }
        }
    }
}