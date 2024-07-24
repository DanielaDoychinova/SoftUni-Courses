using OpenQA.Selenium.Appium;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;

namespace AppiumMobileTestingSummator
{
    public class SummatorPage
    {
        private readonly AndroidDriver _driver;

        public SummatorPage(AndroidDriver driver)
        {
            _driver = driver;
        }

        public IWebElement Result => _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editTextSum"));
        public IWebElement SumBtn => _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/buttonCalcSum"));
        public IWebElement Field2 => _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText2"));
        public IWebElement Field1 => _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText1"));

        public string Calculate(string num1, string num2)
        {
            ClearFields();

            Field1.SendKeys(num1);
            Field2.SendKeys(num2);
            SumBtn.Click();

            return Result.Text;
        }

        public void ClearFields()
        {
            Field1.Clear();
            Field2.Clear();
        }
    }
}
