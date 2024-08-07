using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamPrepIdeaCenter.Pages
{
    public class LoginPage : BasePage
    {
        public LoginPage(IWebDriver driver) : base(driver)
        {
            

        }

        public string Url = BaseUrl + "/Users/Login";

        public IWebElement EmailField => driver.FindElement(By.XPath("//input[@id='typeEmailX-2']"));
        public IWebElement PassField => driver.FindElement(By.XPath("//input[@id='typePasswordX-2']"));
        public IWebElement SigninBtn => driver.FindElement(By.XPath("//button[text()='Sign in']"));

        public void Login(string email, string pass)
        {
            EmailField.SendKeys(email);
            PassField.SendKeys(pass);
            SigninBtn.Click();
        }

        public void OpenPage()
        {
            driver.Navigate().GoToUrl(Url);
        }
    }
}
