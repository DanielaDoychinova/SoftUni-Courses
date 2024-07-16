using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegistryPOM.Pages
{
    public class BasePage
    {
        protected IWebDriver driver;

        public virtual string PageUrl { get; }

        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        public IWebElement HomeLink => driver.FindElement(By.XPath("//a[@href='/']"));

        public IWebElement ViewStudentsLink => driver.FindElement(By.XPath("//a[@href='/students']"));

        public IWebElement AddStudentLink => driver.FindElement(By.XPath("//a[@href='/add-student']"));

        public IWebElement PageHeading => driver.FindElement(By.CssSelector("body > h1"));
        public void OpenPage()
        {
            driver.Navigate().GoToUrl(PageUrl);
        }

        public bool IsPageOpen()
        {
            return driver.Url == this.PageUrl;
        }

        public string GetPageTitle()
        {
            return driver.Title;
        }

        public string GetPageHeading()
        {
            return PageHeading.Text;
        }
    }
}
