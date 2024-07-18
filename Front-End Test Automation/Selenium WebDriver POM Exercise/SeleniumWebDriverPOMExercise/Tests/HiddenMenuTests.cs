

namespace SeleniumWebDriverPOMExercise.Tests
{
    public class HiddenMenuTests : BaseTests
    {
        [SetUp]
        public void Setup()
        {
            Login("standard_user", "secret_sauce");
        }

        [Test]
        public void TestOpenMenu()
        {
            hiddenMenuPage.ClickMenuButton();

            Assert.That(hiddenMenuPage.IsMenuOpen, Is.True, "Menu is not opened.");
        }

        [Test]
        public void TestLogout()
        {
            hiddenMenuPage.ClickMenuButton();
            hiddenMenuPage.ClickLogoutButton();

            Assert.That(driver.Url, Is.EqualTo("https://www.saucedemo.com/"), "User is not logged out.");
        }
    }
}
