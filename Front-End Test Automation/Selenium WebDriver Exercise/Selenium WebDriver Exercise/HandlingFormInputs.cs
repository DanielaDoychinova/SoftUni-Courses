using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace HandlingFormInputs
{
    public class HandlingFormInputsTests
    {
        WebDriver driver;
        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://practice.bpbonline.com/");
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }

        [Test]
        public void HandlingFormInputs()
        {
            var myAccountButton = driver.FindElements(By.XPath("//span" +
                "[@class='ui-button-text']"))[2];
            myAccountButton.Click();

            driver.FindElement(By.LinkText("Continue")).Click();

            driver.FindElement(By.XPath("//input[@type='radio'][@value='m']")).Click();

            driver.FindElement(By.XPath("//td[@class='fieldValue']//input" +
                "[@name='firstname']")).SendKeys("John");

            driver.FindElement(By.XPath("//td[@class='fieldValue']//input" +
                "[@name='lastname']")).SendKeys("Doe");

            driver.FindElement(By.Id("dob")).SendKeys("07/01/1997");

            Random random = new Random();
            int randomNumber = random.Next(1000, 9999);
            string email = "test" + randomNumber.ToString() + "@gmail.com";

            driver.FindElement(By.Name("email_address")).SendKeys(email);

            driver.FindElement(By.XPath("//td[@class='fieldValue']" +
                "//input[@name='company']")).SendKeys("Some Company");

            driver.FindElement(By.Name("street_address")).SendKeys("Gladston Blvd. 15");

            driver.FindElement(By.Name("suburb")).SendKeys("Plovdiv");

            driver.FindElement(By.Name("postcode")).SendKeys("4000");

            driver.FindElement(By.Name("city")).SendKeys("Plovdiv");

            driver.FindElement(By.Name("state")).SendKeys("Plovdiv");

            new SelectElement(driver.FindElement(By.Name("country"))).SelectByText("Bulgaria");

            driver.FindElement(By.Name("telephone")).SendKeys("088325698");

            driver.FindElement(By.Name("newsletter")).Click();

            driver.FindElement(By.Name("password")).SendKeys("testPass123");

            driver.FindElement(By.Name("confirmation")).SendKeys("testPass123");


            driver.FindElement(By.XPath("//*[@id=\"tdb4\"]/span[2]")).Click();

            Assert.AreEqual(driver.FindElement(By.XPath("//div[@id='bodyContent']//h1")).Text, "Your Account Has Been Created!");

            driver.FindElement(By.LinkText("Log Off")).Click();

            driver.FindElement(By.LinkText("Continue")).Click();

            Console.WriteLine("User created successfully!");
        }
    }
}