

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Collections.ObjectModel;

namespace SeleniumWebDriverPOMExercise.Pages
{
    public class BasePage
    {
        protected IWebDriver driver;

        protected WebDriverWait wait;

        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        protected IWebElement FindElement(By by)
        {
            return wait.Until(ExpectedConditions.ElementIsVisible(by));
        }

        protected ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            return driver.FindElements(by);
        }

        protected void Click(By by)
        {
            FindElement(by).Click();
        }

        protected void Type(By by, string text)
        {
            var element = FindElement(by);
            element.Clear();
            element.SendKeys(text);
        }

        protected string GetText(By by)
        {
            var element = FindElement(by);
            return element.Text;
        }
    }
}
