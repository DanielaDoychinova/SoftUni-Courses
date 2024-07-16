using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegistryPOM.Pages
{
    public class HomePage : BasePage
    {
        public HomePage(IWebDriver driver) : base(driver)
        {
        }

        public override string PageUrl => "http://softuni-qa-loadbalancer-2137572849.eu-north-1.elb.amazonaws.com:82/";

        public IWebElement StudentsCountElement => driver.FindElement(By.XPath("//p//b"));

        public int StudentsCount()
        {
            string studentsCountString = this.StudentsCountElement.Text;
            return int.Parse(studentsCountString);
        }
    }
}



