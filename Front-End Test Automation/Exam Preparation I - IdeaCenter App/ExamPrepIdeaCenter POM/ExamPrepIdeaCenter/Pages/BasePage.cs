using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ExamPrepIdeaCenter.Pages
{
    public class BasePage
    {
        protected IWebDriver driver;
        protected WebDriverWait wait;
        protected static readonly string BaseUrl = "http://softuni-qa-loadbalancer-2137572849.eu-north-1.elb.amazonaws.com:83";
        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        public IWebElement HomeLink => driver.FindElement(By.XPath("//img[@class='rounded-circle']"));
        public IWebElement MyProfileLink => driver.FindElement(By.XPath("//nav//a[@href='/Profile']"));
        public IWebElement MyIdeasLink => driver.FindElement(By.XPath("//nav//a[@href='/Ideas/MyIdeas']"));
        public IWebElement CreateIdeaLink => driver.FindElement(By.XPath("//nav//a[@href='/Ideas/CreateIdea']"));
        public IWebElement LogoutLink => driver.FindElement(By.XPath("//nav//a[@href='/Users/Logout']"));

    }
}
