using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System.Text;

namespace RevueCrafterExamPrem
{
    public class RevueCrafterTests
    {
        private readonly static string BaseUrl = "https://d3s5nxhwblsjbi.cloudfront.net/";
        private WebDriver driver;
        private Actions actions;

        private string? lastCreatedRevueTitle;
        private string? lastCreatedRevueDescription;


        [OneTimeSetUp]
        public void Setup()
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("--no-first-run");
            chromeOptions.AddArguments("--disable-search-engine-choice-screen");

            chromeOptions.AddUserProfilePreference("profile.password_manager_enabled", false);
            string chromeDriverPath = @"C:\Users\Daniela\Desktop\Front-End Test Automation\Front-End Test Automation\15. EP II - RevueCrafter\RevueCrafterExamPrem\RevueCrafterExamPrem\bin\Debug\net8.0";
            driver = new ChromeDriver(chromeDriverPath, chromeOptions);

            actions = new Actions(driver);
            driver.Navigate().GoToUrl(BaseUrl);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            driver.Navigate().GoToUrl($"{BaseUrl}Users/Login#loginForm");
            var loginForm = driver.FindElement(By.XPath("//div[@class='col-md-4']"));
            actions.MoveToElement(loginForm).Perform();
            driver.FindElement(By.XPath("//input[@type='email'][@id='form3Example3']")).SendKeys("test@a.a");
            driver.FindElement(By.XPath("//input[@type='password'][@id='form3Example4']")).SendKeys("123456");
            driver.FindElement(By.XPath("//button[@type='submit'][@class='btn btn-primary btn-block mb-4']")).Click();


        }

        [OneTimeTearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }

        [Test, Order(1)]
        public void CreateRevueWithInvalidData()
        {
            driver.Navigate().GoToUrl($"{BaseUrl}Revue/Create#createRevue");
            var createForm = driver.FindElement(By.XPath("//div[@class='col-md-10 col-lg-6 col-xl-5 order-2 order-lg-1']"));
            actions.MoveToElement(createForm).Perform();

            var titleField = driver.FindElement(By.XPath("//input[@type='text'][@id='form3Example1c']"));
            var descriptionField = driver.FindElement(By.XPath("//textarea[@type='text'][@id='form3Example4cd']"));
            var createBtn = driver.FindElement(By.XPath("//button[@type='submit'][@class='btn btn-primary btn-lg']"));

            titleField.SendKeys("");
            descriptionField.SendKeys("");
            createBtn.Click();

            var errorMsg = driver.FindElement(By.XPath("//div[@class='text-danger validation-summary-errors']//ul/li"));

            var currentUrl = driver.Url;

            Assert.That(currentUrl, Is.EqualTo($"{BaseUrl}Revue/Create#createRevue"), "Url is not as expected.");
            Assert.That(errorMsg.Text, Is.EqualTo("Unable to create new Revue!"), "Error message is not as expected.");

        }

        [Test, Order(2)]
        public void CreateRandomRevue()
        {
            driver.Navigate().GoToUrl($"{BaseUrl}Revue/Create#createRevue");
            var createForm = driver.FindElement(By.XPath("//div[@class='col-md-10 col-lg-6 col-xl-5 order-2 order-lg-1']"));
            actions.MoveToElement(createForm).Perform();

            var titleField = driver.FindElement(By.XPath("//input[@type='text'][@id='form3Example1c']"));
            var descriptionField = driver.FindElement(By.XPath("//textarea[@type='text'][@id='form3Example4cd']"));
            var createBtn = driver.FindElement(By.XPath("//button[@type='submit'][@class='btn btn-primary btn-lg']"));

            lastCreatedRevueTitle = "Title " + GenerateRandomString(3);
            lastCreatedRevueDescription = "Description " + GenerateRandomString(10);

            titleField.SendKeys(lastCreatedRevueTitle);
            descriptionField.SendKeys(lastCreatedRevueDescription);
            createBtn.Click();

            var currentUrl = driver.Url;
            var revues = driver.FindElements(By.XPath("//div[@class='col-md-4']"));
            var lastRevueTitle = revues.Last().FindElement(By.XPath("//div[@class='text-muted text-center']")).Text;

            Assert.That(currentUrl, Is.EqualTo($"{BaseUrl}Revue/MyRevues#createRevue"), "Url is not as expected.");
            Assert.That(lastRevueTitle, Is.EqualTo(lastCreatedRevueTitle), "Title is not as expected.");

        }

        [Test, Order(3)]
        public void SearchForRevueTitle()
        {
            driver.Navigate().GoToUrl($"{BaseUrl}Revue/MyRevues");
            var searchField = driver.FindElement(By.XPath("//input[@type='search'][@id='keyword']"));
            var serchBtn = driver.FindElement(By.XPath("//button[@id='search-button']"));
            actions.MoveToElement(searchField).Perform();

            searchField.SendKeys(lastCreatedRevueTitle);
            serchBtn.Click();

            var revueTitle = driver.FindElement(By.XPath("//div[@class='text-muted text-center']")).Text;

            Assert.That(revueTitle, Is.EqualTo(lastCreatedRevueTitle), "Title is not as expected.");

        }

        [Test, Order(4)]
        public void EditLastCreatedRevueTitle()
        {
            driver.Navigate().GoToUrl($"{BaseUrl}Revue/MyRevues");
            var revues = driver.FindElements(By.XPath("//div[@class='col-md-4']"));
            var lastCreatedRevueCard = revues.Last().FindElement(By.XPath("//div[@class='col-md-4']"));
            actions.MoveToElement(lastCreatedRevueCard).Perform();

            var cardEditBtn = lastCreatedRevueCard.FindElement(By.XPath("//a[@class='btn btn-sm btn-outline-secondary'][2]"));
            cardEditBtn.Click();

            var editForm = driver.FindElement(By.XPath("//div[@class='col-md-10 col-lg-6 col-xl-5 order-2 order-lg-1']"));
            actions.MoveToElement(editForm).Perform();

            lastCreatedRevueTitle = "Edited title";

            var titleField = driver.FindElement(By.XPath("//input[@type='text'][@id='form3Example1c']"));
            var editBtn = driver.FindElement(By.XPath("//button[@type='submit'][@class='btn btn-primary btn-lg']"));

            titleField.Clear();
            titleField.SendKeys(lastCreatedRevueTitle);
            editBtn.Click();

            var currentUrl = driver.Url;

            var revuesNew = driver.FindElements(By.XPath("//div[@class='col-md-4']"));
            var editedRevueCard = revuesNew.Last().FindElement(By.XPath("//div[@class='col-md-4']"));
            actions.MoveToElement(editedRevueCard).Perform();

            var editedRevueTitle = revuesNew.Last().FindElement(By.XPath("//div[@class='text-muted text-center']")).Text;

            Assert.That(currentUrl, Is.EqualTo($"{BaseUrl}Revue/MyRevues"), "Url is not as expected.");
            Assert.That(editedRevueTitle, Is.EqualTo(lastCreatedRevueTitle), "Title is not as expected.");

        }

        [Test, Order(5)]
        public void DeleteLastCreatedRevue()
        {
            driver.Navigate().GoToUrl($"{BaseUrl}Revue/MyRevues");
            var revues = driver.FindElements(By.XPath("//div[@class='col-md-4']"));
            Assert.That(revues.Count(), Is.AtLeast(1), "No revues found.");
            var lastCreatedRevueCard = revues.Last().FindElement(By.XPath("//div[@class='col-md-4']"));
            
            actions.MoveToElement(lastCreatedRevueCard).Perform();

            var deleteBtn = driver.FindElement(By.XPath($"//div[text()='{lastCreatedRevueTitle}']/..//a[text()='Delete']"));
            deleteBtn.Click();

            var currentUrl = driver.Url;

            Assert.That(currentUrl, Is.EqualTo($"{BaseUrl}Revue/MyRevues"), "Url is not as expected");

            var revuesNew = driver.FindElements(By.XPath("//div[@class='col-md-4']"));
            Assert.That(revuesNew.Count(), Is.LessThan(revues.Count), "Revues count has not decreased.");
            
            if(revuesNew.Count() > 0)
            {
                var revueTitle = revuesNew.Last().FindElement(By.XPath("//div[@class='text-muted text-center']")).Text;

                Assert.That(revueTitle, !Is.EqualTo(lastCreatedRevueTitle), "The title is the same as in last created revue.");
            } else
            {
                Assert.Pass("The revue was deleted.");
            }

        }

        [Test, Order(6)]
        public void SearchForDeletedRevue()
        {
            driver.Navigate().GoToUrl($"{BaseUrl}Revue/MyRevues");
            var searchField = driver.FindElement(By.XPath("//input[@type='search'][@id='keyword']"));
            var serchBtn = driver.FindElement(By.XPath("//button[@id='search-button']"));
            actions.MoveToElement(searchField).Perform();

            searchField.SendKeys(lastCreatedRevueTitle);
            serchBtn.Click();

            var noRevuesMsg = driver.FindElement(By.XPath("//span[@class='col-12 text-muted']")).Text;

            Assert.That(noRevuesMsg, Is.EqualTo("No Revues yet!"), "The message is not as expected.");
        }


        public static string GenerateRandomString(int lenght)
        {
            char[] chars = "ashncmzjdueowqkdhf".ToCharArray();
            if (lenght <= 0)
            {
                throw new ArgumentException("Lenght must be greater than 0.", nameof(lenght));
            }

            var random = new Random();
            var result = new StringBuilder(lenght);
            for (int i = 0; i < lenght; i++)
            {
                result.Append(chars[random.Next(chars.Length)]);
            }
            return result.ToString();
        }
    }
}