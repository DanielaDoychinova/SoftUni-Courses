using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamPrepIdeaCenter.Pages
{
    public class CreateIdeaPage : BasePage
    {
        public CreateIdeaPage(IWebDriver driver) : base(driver)
        {
            
        }

        public string Url = BaseUrl + "/Ideas/Create";

        public IWebElement TitleField => driver.FindElement(By.XPath("//input[@name='Title']"));
        public IWebElement AddPictureField => driver.FindElement(By.XPath("//input[@name='Url']"));
        public IWebElement DescriptionField => driver.FindElement(By.XPath("//textarea[@name='Description']"));
        public IWebElement CreateBtn => driver.FindElement(By.XPath("//button[text()='Create']"));
        public IWebElement MainMessage => driver.FindElement(By.XPath("//div[@class='text-danger validation-summary-errors']//ul//li"));
        public IWebElement TitleErrorMessage => driver.FindElement(By.XPath("//span[@data-valmsg-for='Title']"));
        public IWebElement DescriptionErrorMessage => driver.FindElement(By.XPath("//span[@data-valmsg-for='Description']"));

        public void OpenPage()
        {
            driver.Navigate().GoToUrl(Url);
        }
        public void CreateIdea(string title, string url, string description)
        {
            TitleField.SendKeys(title);
            AddPictureField.SendKeys(url);
            DescriptionField.SendKeys(description);
            CreateBtn.Click();
        }

        public void AssertErrorMessages()
        {
            Assert.True(MainMessage.Text.Equals("Unable to create new Idea!"), "Main message is not as expected.");
            Assert.True(TitleErrorMessage.Text.Equals("The Title field is required."), "Title error message is not as expected.");
            Assert.True(DescriptionErrorMessage.Text.Equals("The Description field is required."), "Description error message is not as expeted.");

        }
    }
}
