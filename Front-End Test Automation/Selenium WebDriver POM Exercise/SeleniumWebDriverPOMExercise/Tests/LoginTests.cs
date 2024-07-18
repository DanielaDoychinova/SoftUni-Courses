

using SeleniumWebDriverPOMExercise.Pages;

namespace SeleniumWebDriverPOMExercise.Tests
{
    public class LoginTests : BaseTests
    {
        [Test]
        public void TestLoginWithValidCredentials()
        {
            Login("standard_user", "secret_sauce");

            Assert.That(inventoryPage.IsPageLoaded, Is.True, "Inventory page is not loaded after successfull login");
        }

        [Test]
        public void TestLoginWithInvalidCredentials()
        {
            Login("invalid_user", "secret_sauce");

            string error = loginPage.GetErrorMsg();

            Assert.That(error.Contains("Username and password do not match any user in this service"));
        }

        [Test]
        public void TestLoginWithLockedOutUser()
        {
            Login("locked_out_user", "secret_sauce");

            string error = loginPage.GetErrorMsg();

            Assert.That(error.Contains("Sorry, this user has been locked out."));
        }
    }
}
