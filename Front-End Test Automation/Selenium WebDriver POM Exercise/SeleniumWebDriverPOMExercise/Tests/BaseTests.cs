
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumWebDriverPOMExercise.Pages;

namespace SeleniumWebDriverPOMExercise.Tests
{
    public class BaseTests
    {
        protected IWebDriver driver;

        protected LoginPage loginPage;
        protected InventoryPage inventoryPage;
        protected CartPage cartPage;
        protected CheckoutPage checkoutPage;
        protected HiddenMenuPage hiddenMenuPage;

        [SetUp]
        public void Setup()
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddUserProfilePreference("profile.password_manager_enabled", false);

            driver = new ChromeDriver(chromeOptions);
            driver.Manage().Window.Maximize();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            loginPage = new LoginPage(driver);
            inventoryPage = new InventoryPage(driver);
            cartPage = new CartPage(driver);
            hiddenMenuPage = new HiddenMenuPage(driver);
            checkoutPage = new CheckoutPage(driver);
        }

        [TearDown]
        public void Teardown()
        {
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose();
            }

        }


        protected void Login(string username, string password)
        {
            driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            var loginPage = new LoginPage(driver);
            loginPage.LoginUser(username, password);
        }
    }
}
