using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumWebDriverPOMExercise.Pages
{
    public class CheckoutPage : BasePage
    {
        private readonly By firstNameField = By.Id("first-name");
        private readonly By lastNameField = By.Id("last-name");
        private readonly By postalCodeField = By.Id("postal-code");
        private readonly By continueBtn = By.CssSelector(".cart_button");
        private readonly By finishBtn = By.Id("finish");
        private readonly By completeHeader = By.CssSelector(".complete-header");
        public CheckoutPage(IWebDriver driver) : base(driver)
        {
            
        }

        public void EnterFirstName(string firstName)
        {
            Type(firstNameField, firstName);
        }

        public void EnterLastName(string lastName) 
        { 
            Type(lastNameField, lastName);
        }

        public void EnterPostalCode(string postalCode)
        {
            Type(postalCodeField, postalCode);
        }

        public void ClickContinue()
        {
            Click(continueBtn);
        }

        public void ClickFinish()
        {
            Click(finishBtn);
        }

        public bool IsPageLoaded()
        {
            return driver.Url.Contains("checkout-step-one.html") || driver.Url.Contains("checkout-step-two.html");
 
        }

        public bool IsCheckoutComplete()
        {
            return GetText(completeHeader) == "Thank you for your order!";
        }


    }
}
