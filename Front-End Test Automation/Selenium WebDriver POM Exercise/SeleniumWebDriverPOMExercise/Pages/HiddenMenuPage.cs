

using OpenQA.Selenium;

namespace SeleniumWebDriverPOMExercise.Pages
{
    public class HiddenMenuPage : BasePage
    {
        private readonly By menuBtn = By.CssSelector(".bm-burger-button");
        private readonly By logoutBtn = By.Id("logout_sidebar_link");
        public HiddenMenuPage(IWebDriver driver) : base(driver)
        {
            
        }

        public void ClickMenuButton()
        {
            Click(menuBtn);
        }

        public void ClickLogoutButton()
        {
            Click(logoutBtn);
        }

        public bool IsMenuOpen()
        {
            return FindElement(logoutBtn).Displayed;
        }

    }
}
