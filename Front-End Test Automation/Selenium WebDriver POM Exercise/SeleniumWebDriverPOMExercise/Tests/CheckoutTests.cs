
namespace SeleniumWebDriverPOMExercise.Tests
{
    public class CheckoutTests : BaseTests
    {
        [SetUp]
        public void Setup()
        {
            Login("standard_user", "secret_sauce");

            inventoryPage.AddToCartByIndex(1);
            inventoryPage.ClickCartLink();

            cartPage.ClickCheckout();
        }

        [Test]
        public void TestCheckoutPageLoaded()
        {
            Assert.That(driver.Url.Contains("checkout-step-one.html"), Is.True, "Checkout page is not loaded.");
        }

        [Test]
        public void TestContinueToNextStep()
        {
            checkoutPage.EnterFirstName("Ivan");
            checkoutPage.EnterLastName("Ivanov");
            checkoutPage.EnterPostalCode("4000");
            checkoutPage.ClickContinue();

            Assert.That(driver.Url.Contains("checkout-step-two.html"), Is.True, "Checkout page step two is not loaded.");
        }

        [Test]
        public void TestCompleteOrder()
        {
            checkoutPage.EnterFirstName("Ivan");
            checkoutPage.EnterLastName("Ivanov");
            checkoutPage.EnterPostalCode("4000");
            checkoutPage.ClickContinue();
            checkoutPage.ClickFinish();

            Assert.That(checkoutPage.IsCheckoutComplete(), Is.True, "Order is not completed.");

        }
    }
}
