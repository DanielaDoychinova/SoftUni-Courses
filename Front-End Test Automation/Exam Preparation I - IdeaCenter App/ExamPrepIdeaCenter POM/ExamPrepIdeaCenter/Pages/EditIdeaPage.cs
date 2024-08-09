using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamPrepIdeaCenter.Pages
{
    public class EditIdeaPage : BasePage
    {

        public EditIdeaPage(IWebDriver driver) : base(driver)
        {

        }

        public string Url = BaseUrl + "/Ideas/Edit";

        public IWebElement TitleField => driver.FindElement(By.XPath("//input[@name='Title']"));
        public IWebElement AddPictureField => driver.FindElement(By.XPath("//input[@name='Url']"));
        public IWebElement DescriptionField => driver.FindElement(By.XPath("//textarea[@name='Description']"));
        public IWebElement EditBtn => driver.FindElement(By.XPath("//button[text()='Edit']"));
    }
}
