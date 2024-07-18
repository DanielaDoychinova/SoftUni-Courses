
namespace SeleniumWebDriverPOMExercise.Tests
{
    public class CartTests : BaseTests
    {
        [SetUp]
        public void Setup()
        {
            Login("standard_user", "secret_sauce");

            inventoryPage.AddToCartByIndex(1);
            inventoryPage.ClickCartLink();
        }

        [Test]
        public void TestCartItemDisplayed()
        {
            Assert.That(cartPage.IsCartItemDisplayed(), Is.True, "Item is not displayed in the cart.");
        }

        [Test]
        public void TestClickCHeckout()
        {
            cartPage.ClickCheckout();

            Assert.That(checkoutPage.IsPageLoaded(), Is.True, "Checkout page is not loaded.");
        }
    }
}
