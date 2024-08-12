using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System.Text;

namespace ExamPrepMovieCatalog
{
    public class MovieCatalogTests
    {
        private readonly static string BaseUrl = "http://moviecatalog-env.eba-ubyppecf.eu-north-1.elasticbeanstalk.com/";
        private WebDriver driver;
        private Actions actions;

        private string? lastCreatedMovieTitle;
        private string? lastCreatedMovieDescription;


        [OneTimeSetUp]
        public void Setup()
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("--disable-search-engine-choice-screen");
            chromeOptions.AddUserProfilePreference("profile.password_manager_enabled", false);
            driver = new ChromeDriver(chromeOptions);
            actions = new Actions(driver);

            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);  

            driver.Navigate().GoToUrl($"{BaseUrl}User/Login");
            driver.FindElement(By.XPath("//input[@id='form2Example17']")).SendKeys("test7@a.a");
            driver.FindElement(By.XPath("//input[@id='form2Example27']")).SendKeys("123456");
            driver.FindElement(By.XPath("//button[@class='btn warning'][@type='submit']")).Click();

        }

        [OneTimeTearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();

        }

        [Test, Order(1)]
        public void AddMovieWithoutTitle()
        {
            driver.Navigate().GoToUrl($"{BaseUrl}Catalog/Add");
            var createForm = driver.FindElement(By.XPath("//div[@class='card-body p-4 p-lg-5 text-black']"));
            var titleField = driver.FindElement(By.XPath("//input[@type='text'][@id='form2Example17']"));
            var createBtn = driver.FindElement(By.CssSelector(".warning"));

            lastCreatedMovieTitle = "";
            titleField.SendKeys(lastCreatedMovieTitle);

            actions.ScrollToElement(createBtn).Perform();
            createBtn.Click();


            var errorMsg = driver.FindElement(By.XPath("//div[@class='toast-message']")).Text;
            var currentUrl = driver.Url;

            Assert.That(currentUrl, Is.EqualTo($"{BaseUrl}Catalog/Add"), "Url is not as expected.");
            Assert.That(errorMsg, Is.EqualTo("The Title field is required."), "Error message is not as expected");

        }

        [Test, Order(2)]
        public void AddMovieWithoutDescription()
        {
            driver.Navigate().GoToUrl($"{BaseUrl}Catalog/Add");
            var createForm = driver.FindElement(By.XPath("//div[@class='card-body p-4 p-lg-5 text-black']"));
            var titleField = driver.FindElement(By.XPath("//input[@type='text'][@id='form2Example17']"));
            var descrField = driver.FindElement(By.XPath("//textarea[@id='form2Example17']"));
            var createBtn = driver.FindElement(By.CssSelector(".warning"));

            lastCreatedMovieTitle = "Title";
            titleField.SendKeys(lastCreatedMovieTitle);

            lastCreatedMovieDescription = "";
            descrField.SendKeys(lastCreatedMovieDescription);

            actions.ScrollToElement(createBtn).Perform();
            createBtn.Click();

            var errorMsg = driver.FindElement(By.XPath("//div[@class='toast toast-error']")).Text;
            var currentUrl = driver.Url;

            Assert.That(currentUrl, Is.EqualTo($"{BaseUrl}Catalog/Add"), "Url is not as expected.");
            Assert.That(errorMsg, Is.EqualTo("The Description field is required."), "Error message is not as expected");
        }

        [Test, Order(3)]
        public void AddMovieWithRandomTitle()
        {
            driver.Navigate().GoToUrl($"{BaseUrl}Catalog/Add");
            var createForm = driver.FindElement(By.XPath("//div[@class='card-body p-4 p-lg-5 text-black']"));

            var titleField = driver.FindElement(By.XPath("//input[@type='text'][@id='form2Example17']"));
            lastCreatedMovieTitle = "title: " + GenerateRandomString(3);
            titleField.SendKeys(lastCreatedMovieTitle);

            var descrField = driver.FindElement(By.XPath("//textarea[@id='form2Example17']"));
            lastCreatedMovieDescription = GenerateRandomString(10);
            descrField.SendKeys(lastCreatedMovieDescription);

            var createBtn = driver.FindElement(By.CssSelector(".warning"));
            actions.ScrollToElement(createBtn).Perform();
            createBtn.Click();

            var currentUrl = driver.Url;
            Assert.That(currentUrl, Is.EqualTo($"{BaseUrl}Catalog/All#all"), "Url is not as expected.");

            var movies = driver.FindElements(By.XPath("//div[@class='col-lg-4']"));
            var lastMovieTitle = movies.Last().FindElement(By.XPath("//div[@class='col-lg-4']//h2")).Text;

            Assert.That(lastMovieTitle.ToLowerInvariant(), Is.EqualTo(lastCreatedMovieTitle), "Title is not as expected.");

        }

        [Test, Order(4)]
        public void EditLastAddedMovie()
        {
            driver.Navigate().GoToUrl($"{BaseUrl}Catalog/All");

            var movies = driver.FindElements(By.XPath("//div[@class='col-lg-4']"));
            var editBtn = movies.Last().FindElement(By.XPath("//a[contains(.,'Edit')]"));
            editBtn.Click();

            var editForm = driver.FindElement(By.XPath("//div[@class='card-body p-4 p-lg-5 text-black']"));
            var titleField = driver.FindElement(By.XPath("//input[@type='text'][@id='form2Example17']"));
            var editButton = driver.FindElement(By.CssSelector(".warning"));

            lastCreatedMovieTitle = "edited title";

            titleField.Clear();
            titleField.SendKeys(lastCreatedMovieTitle);

            actions.ScrollToElement(editButton).Perform() ;
            editButton.Click();

            var currentUrl = driver.Url;
            Assert.That(currentUrl, Is.EqualTo($"{BaseUrl}Catalog/All#all"), "Url is not as expected.");


            var moviesNew = driver.FindElements(By.XPath("//div[@class='col-lg-4']"));
            var lastMovieTitle = moviesNew.Last().FindElement(By.XPath("//div[@class='col-lg-4']//h2")).Text;

            Assert.That(lastMovieTitle.ToLowerInvariant(), Is.EqualTo(lastCreatedMovieTitle), "Title is not as expected.");
        }

        [Test, Order(5)]
        public void MarkLastAddedAsWatched()
        {
            driver.Navigate().GoToUrl($"{BaseUrl}Catalog/All");

            var movies = driver.FindElements(By.XPath("//div[@class='col-lg-4']"));
            var markAsWatchedBtn = movies.Last().FindElement(By.XPath("//a[@class='btn btn-info'][contains(.,'Mark as Watched')]"));
            markAsWatchedBtn.Click();

            driver.Navigate().GoToUrl($"{BaseUrl}Catalog/Watched#watched");

            var watchedMovies = driver.FindElements(By.XPath("//div[@class='col-lg-4']"));
            var lastWatchedMovieTitle = watchedMovies.Last().FindElement(By.XPath("//div[@class='col-lg-4']//h2")).Text;

            Assert.That(lastWatchedMovieTitle.ToLower(), Is.EqualTo(lastCreatedMovieTitle), "The movie is not marked as watched.");

        }

        [Test, Order(6)]
        public void DeleteLastCreatedMovie()
        {
            driver.Navigate().GoToUrl($"{BaseUrl}Catalog/All");

            var movies = driver.FindElements(By.XPath("//div[@class='col-lg-4']"));
            Assert.That(movies.Count(), Is.AtLeast(1), "There are no movies.");

            var lastMovieDeleteBtn = movies.Last().FindElement(By.XPath("(//a[@class='btn btn-danger'][contains(.,'Delete')])"));
            lastMovieDeleteBtn.Click();

            driver.FindElement(By.XPath("//button[@type='submit'][contains(.,'Yes')]")).Click();

            var currentUrl = driver.Url;
            Assert.That(currentUrl, Is.EqualTo($"{BaseUrl}Catalog/All#all"), "Url is not as expected.");


            var successMsg = driver.FindElement(By.XPath("//div[contains(@class,'toast-message')]")).Text;
            Assert.That(successMsg, Is.EqualTo("The Movie is deleted successfully!"), "Message is not as expected.");

            var moviesNew = driver.FindElements(By.XPath("//div[@class='col-lg-4']"));
            Assert.That(moviesNew.Count(), Is.LessThan(movies.Count()), "Movies count has not decreased.");
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