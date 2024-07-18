
using System.Security.Cryptography.X509Certificates;

namespace SeleniumWebDriverPOMExercise.Tests
{
    public class InventoryTests : BaseTests
    {
        
        [SetUp]
        public void Setup()
        {
            Login("standard_user", "secret_sauce");
        }

        [Test]
        public void TestInventoryDisplay()
        {
            Assert.That(inventoryPage.IsInventoryPageDisplayed(), Is.True, "Inventory Page has no items displayed.");
        }

        [Test]
        public void TestAddToCartByIndex()
        {
            inventoryPage.AddToCartByIndex(2);

            inventoryPage.ClickCartLink();

            Assert.That(cartPage.IsCartItemDisplayed, Is.True, "Item is not displayed in the cart.");

        }

        [Test]
        public void TestAddToCartByName()
        {
            inventoryPage.AddToCartByName("Sauce Labs Backpack");

            inventoryPage.ClickCartLink();

            Assert.That(cartPage.IsCartItemDisplayed, Is.True, "Item is not displayed in the cart.");

        }

        [Test]
        public void TestPageTitle()
        {
            Assert.That(inventoryPage.IsPageLoaded(), Is.True, "Page title is not shown correctly");
        }
    }
}
