
using OpenQA.Selenium;

namespace SeleniumWebDriverPOMExercise.Pages
{
    public class InventoryPage : BasePage
    {
        protected readonly By cartLink = By.CssSelector(".shopping_cart_link");
        private readonly By productsPageTitle = By.CssSelector(".title");
        private readonly By inventoryItems = By.CssSelector(".inventory_item");

        public InventoryPage(IWebDriver driver) : base(driver)
        {
            
        }

        public void AddToCartByIndex(int itemIndex)
        {
            var itemByIndexButton = By.XPath($"//div[@class='inventory_item'][{itemIndex}]//button");

            Click(itemByIndexButton);
        }

        public void AddToCartByName(string name)
        {
            var itemNameButton = By.XPath($"//div[text()='{name}']" +
                $"/ancestor::div[@class='inventory_item']//button[contains(@class, 'btn_inventory')]");

            Click(itemNameButton);
        }

        public void ClickCartLink()
        {
            Click(cartLink);    
        }

        public bool IsInventoryPageDisplayed()
        {
            return FindElements(inventoryItems).Any();
        }

        public bool IsPageLoaded()
        {
            return GetText(productsPageTitle) == "Products" && driver.Url.Contains("inventory.html");
        }
    }
}
