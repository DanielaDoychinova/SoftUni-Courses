using OpenQA.Selenium;

namespace SumTwoNumbers
{
    public class SumNumberPage
    {
        private readonly IWebDriver driver;

        public SumNumberPage(IWebDriver driver)
        {
            this.driver = driver;
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
        }

        public const string PageUrl = "https://5b99d1a3-894b-4d97-aeba-0ea0a0ae1cbc-00-8l47u88hnr7z.janeway.replit.dev/";

        public IWebElement FieldNum1 => driver.FindElement(By.XPath("//input[@id='number1']"));

        public IWebElement FieldNum2 => driver.FindElement(By.XPath("//input[@id='number2']"));

        public IWebElement ButtonCalc => driver.FindElement(By.XPath("//input[@type='button']"));

        public IWebElement ButtonReset => driver.FindElement(By.XPath("//input[@type='Reset']"));

        public IWebElement ElementResult => driver.FindElement(By.XPath("//div[@id='result']"));

        public void openPage()
        {
            driver.Navigate().GoToUrl(PageUrl);
        }



        public void OpenPage()
        {
            driver.Navigate().GoToUrl(PageUrl);
        }

        public void ResetForm()
        {
            ButtonReset.Click();
        }

        public bool IsFormEmpty()
        {
            return FieldNum1.Text + FieldNum2.Text + ElementResult.Text == "";
        }

        public string AddNumbers(string num1, string num2)
        {
            FieldNum1.SendKeys(num1);
            FieldNum2.SendKeys(num2);
            ButtonCalc.Click();
            string result = ElementResult.Text;
            return result;
        }
    }
}