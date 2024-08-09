using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System.Text;

namespace IdeaCenterExamPrep
{
    public class IdeaCenterTests
    {
        private readonly static string BaseUrl = "http://softuni-qa-loadbalancer-2137572849.eu-north-1.elb.amazonaws.com:83";
        private WebDriver driver;
        private Actions actions;
        private string? LastCreatedTitle;
        private string? lastCreatedDescription;


        [OneTimeSetUp]
        public void Setup()
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("--no-first-run");
            chromeOptions.AddArguments("--disable-search-engine-choice-screen");
            chromeOptions.AddUserProfilePreference("profile.password_manager_enabled", false);

            string chromeDriverPath = @"C:\Users\Daniela\Desktop\Front-End Test Automation\Front-End Test Automation\14. EP I - IdeaCenter\ExamPrepIdeaCenter\ExamPrepIdeaCenter\bin\Debug\net8.0";
            driver = new ChromeDriver(chromeDriverPath, chromeOptions);
            actions = new Actions(driver);
            driver.Navigate().GoToUrl(BaseUrl);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            driver.Navigate().GoToUrl($"{BaseUrl}/Users/Login");
            var loginForm = driver.FindElement(By.XPath("//form[@class='card-body p-5 text-center']"));
            actions.MoveToElement(loginForm).Perform();

            driver.FindElement(By.Id("typeEmailX-2")).SendKeys("test@ab.cd");
            driver.FindElement(By.Id("typePasswordX-2")).SendKeys("123456");
            driver.FindElement(By.XPath("//button[@class='btn btn-primary btn-lg btn-block']")).Click();

        }

        [OneTimeTearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();

        }

        [Test, Order(1)]
        public void CreateIdeaWithInvalidData()
        {
            driver.Navigate().GoToUrl($"{BaseUrl}/Ideas/Create");
            //var createForm = driver.FindElement(By.CssSelector(".card-body p-md-5"));
            //actions.MoveToElement(createForm).Perform();

            var titleInput = driver.FindElement(By.XPath("//input[@id='form3Example1c']"));
            titleInput.Clear();
            titleInput.SendKeys("");

            var describeInput = driver.FindElement(By.XPath("//textarea[@id='form3Example4cd']"));
            describeInput.Clear();
            describeInput.SendKeys("");

            var createBtn = driver.FindElement(By.XPath("//button[@type='submit']"));
            createBtn.Click();

            var currentUrl = driver.Url;

            var errorMsg = driver.FindElement(By.XPath("//ul//li[contains(.,'Unable')]"));

            Assert.That(currentUrl, Is.EqualTo($"{BaseUrl}/Ideas/Create"));
            Assert.That(errorMsg.Text, Is.EqualTo("Unable to create new Idea!"));

        }

        [Test, Order(2)]
        public void CreateRandomIdea()
        {
            driver.Navigate().GoToUrl($"{BaseUrl}/Ideas/Create");

            var titleInput = driver.FindElement(By.XPath("//input[@id='form3Example1c']"));
            titleInput.Clear();
            LastCreatedTitle = GenerateRandomString(6);
            titleInput.SendKeys(LastCreatedTitle);

            var describeInput = driver.FindElement(By.XPath("//textarea[@id='form3Example4cd']"));
            describeInput.Clear();
            lastCreatedDescription = GenerateRandomString(40);
            describeInput.SendKeys(lastCreatedDescription);

            var createBtn = driver.FindElement(By.XPath("//button[@type='submit']"));
            createBtn.Click();

            var currentUrl = driver.Url;
            var ideas = driver.FindElements(By.CssSelector(".col-md-4"));
            var lastIdeaDescription = ideas.Last().FindElement(By.XPath("//p[@class='card-text']")).Text;

            Assert.That(currentUrl, Is.EqualTo($"{BaseUrl}/Ideas/MyIdeas"), "Url is not as expected.");
            Assert.That(lastIdeaDescription, Is.EqualTo(lastCreatedDescription), "Description is not as expected.");
        }

        [Test, Order(3)]
        public void ViewLastCreatedIdea()
        {
            driver.Navigate().GoToUrl($"{BaseUrl}/Ideas/MyIdeas");

            var ideas = driver.FindElements(By.CssSelector(".col-md-4"));
            var lastIdeaViewBtn = ideas.Last().FindElement(By.XPath("//a[@class='btn btn-sm btn-outline-secondary'][@type='button']"));
            lastIdeaViewBtn.Click();

            var ideaTitle = driver.FindElement(By.XPath("//div[@id='intro']//h1")).Text;

            var currentUrl = driver.Url;

            Assert.That(currentUrl, Does.Contain($"{BaseUrl}/Ideas/Read"), "Url is not as expected.");
            Assert.That(ideaTitle, Is.EqualTo(LastCreatedTitle));

        }

        [Test, Order(4)]
        public void EditLastCreatedIdeaTitle()
        {
            driver.Navigate().GoToUrl($"{BaseUrl}/Ideas/MyIdeas");

            var ideas = driver.FindElements(By.CssSelector(".col-md-4"));
            var lastIdeaEditBtn = ideas.Last().FindElement(By.XPath("//a[contains(.,'Edit')]"));
            lastIdeaEditBtn.Click();

            var titleInput = driver.FindElement(By.XPath("//input[@id='form3Example1c']"));
            titleInput.Clear();
            var editedTitle = "Edited" + GenerateRandomString(6);
            titleInput.SendKeys(editedTitle);

            var editBtn = driver.FindElement(By.XPath("//button[@type='submit']"));
            editBtn.Click();

            ideas = driver.FindElements(By.CssSelector(".col-md-4"));

            var lastIdeaViewBtn = ideas.Last().FindElement(By.XPath("//a[@class='btn btn-sm btn-outline-secondary'][@type='button']"));
            lastIdeaViewBtn.Click();

            var ideaTitle = driver.FindElement(By.XPath("//div[@id='intro']//h1")).Text;

            Assert.That(ideaTitle, Is.EqualTo(editedTitle));
        }

        [Test, Order(5)]
        public void EditLastCreatedIdeaDescription()
        {
            driver.Navigate().GoToUrl($"{BaseUrl}/Ideas/MyIdeas");

            var ideas = driver.FindElements(By.CssSelector(".col-md-4"));
            var lastIdeaEditBtn = ideas.Last().FindElement(By.XPath("//a[contains(.,'Edit')]"));
            lastIdeaEditBtn.Click();

            var descriptionInput = driver.FindElement(By.XPath("//textarea[@id='form3Example4cd']"));
            descriptionInput.Clear();
            var editedDescription = "Edited" + GenerateRandomString(6);
            descriptionInput.SendKeys(editedDescription);

            var editBtn = driver.FindElement(By.XPath("//button[@type='submit']"));
            editBtn.Click();

            ideas = driver.FindElements(By.CssSelector(".col-md-4"));

            var lastIdeaViewBtn = ideas.Last().FindElement(By.XPath("//a[@class='btn btn-sm btn-outline-secondary'][@type='button']"));
            lastIdeaViewBtn.Click();

            var ideaDescr = driver.FindElement(By.XPath("//section[@class='row']//p")).Text;

            Assert.That(ideaDescr, Is.EqualTo(editedDescription));
        }

        [Test, Order(6)]
        public void DeleteLastCreatedIdea()
        {
            driver.Navigate().GoToUrl($"{BaseUrl}/Ideas/MyIdeas");

            var ideas = driver.FindElements(By.CssSelector(".col-md-4"));
            Assert.That(ideas.Count(), Is.AtLeast(1), "There are no ideas");

            var lastIdea = ideas.Last();
            actions.ScrollToElement(lastIdea).Perform();

            var lastIdeaDeleteBtn = ideas.Last().FindElement(By.XPath("//a[contains(.,'Delete')]"));
            lastIdeaDeleteBtn.Click();

            var currentUrl = driver.Url;

            Assert.That(currentUrl, Is.EqualTo($"{BaseUrl}/Ideas/MyIdeas"), "Url is not as expected.");

            var ideasResult = driver.FindElements(By.CssSelector(".col-md-4"));
            Assert.That(ideasResult.Count(), Is.LessThan(ideas.Count()), "Idea is not deleted.");

           // var lastIdeaDescr = ideasResult.Last().FindElement(By.XPath("//p[@class='card-text']")).Text;
           // Assert.That(lastIdeaDescr, !Is.EqualTo(lastCreatedDescription), "The ide is not deleted.");
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