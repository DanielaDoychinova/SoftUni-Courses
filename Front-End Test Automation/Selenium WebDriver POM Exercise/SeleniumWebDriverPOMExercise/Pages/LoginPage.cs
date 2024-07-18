using OpenQA.Selenium;


namespace SeleniumWebDriverPOMExercise.Pages
{
    public class LoginPage : BasePage
    {

        private readonly By userNameField = By.XPath("//input[@id='user-name']");
        private readonly By passwordField = By.XPath("//input[@id='password']");
        private readonly By loginBtn = By.XPath("//input[@id='login-button']");
        private readonly By errorMsg = By.XPath("//div[@class='error-message-container error']//h3");

        public LoginPage(IWebDriver driver) : base(driver)
        {

        }

        public void FillUserName(string username)
        {
            Type(userNameField, username);
        }

        public void FillPassword(string pass)
        {
            Type(passwordField, pass);
        }

        public void ClickLoginBtn()
        {
            Click(loginBtn);
        }

        public string GetErrorMsg()
        {
            return GetText(errorMsg);
        }

        public void LoginUser(string username, string pass)
        {
            FillUserName(username);
            FillPassword(pass);
            ClickLoginBtn(); 
        }
    }
}
