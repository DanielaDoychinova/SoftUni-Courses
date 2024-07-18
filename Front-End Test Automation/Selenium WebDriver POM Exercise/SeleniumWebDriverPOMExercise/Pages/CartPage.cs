
using OpenQA.Selenium;

namespace SeleniumWebDriverPOMExercise.Pages
{
    public class CartPage : BasePage
    {
        private readonly By cartItem = By.CssSelector(".cart_item");
        private readonly By checkoutBtn = By.CssSelector("#checkout");

        public CartPage(IWebDriver driver) : base(driver)
        {

        }

        public bool IsCartItemDisplayed()
        {
            return FindElement(cartItem).Displayed;
        }

        public void ClickCheckout()
        {
            Click(checkoutBtn);
        }
    }
}
