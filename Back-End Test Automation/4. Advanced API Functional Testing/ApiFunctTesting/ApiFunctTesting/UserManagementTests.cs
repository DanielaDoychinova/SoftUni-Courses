using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiFunctTesting
{
    [TestFixture]
    public class UserManagementTests
    {

        private RestClient client;
        private string token;
        private Random random;

        [TearDown]
        public void Dispose()
        {
            client.Dispose();
        }

        [SetUp]
        public void Setup()
        {
            client = new RestClient(GlobalConstants.BaseUrl);
            token = GlobalConstants.AuthenticateUser("admin@gmail.com", "admin@gmail.com");
            random = new Random();
        }

        [Test]
        public void UserLoginTest()
        {
            var loginRequest = new RestRequest("/user/login", Method.Post);
            loginRequest.AddJsonBody(new
            {
                email = "john.doe@example.com",
                password = "password123"
            });

            var loginResponse = client.Execute(loginRequest);
            Assert.True(loginResponse.IsSuccessful, "Login failed.");
            Assert.That(loginResponse.Content, Is.Not.Null.Or.Empty, "Login response is null or empty.");

            string userToken = JObject.Parse(loginResponse.Content)["token"]?.ToString();
            Assert.That(userToken, Is.Not.Null.Or.Empty, "User token is null or empty.");

            var randomUserEmail = $"test{random.Next(999, 9999)}@email.com";
            var signUpRequest = new RestRequest("/user/register", Method.Post);
            signUpRequest.AddJsonBody(new
            {
                firstname = "Name" + random.Next(999, 9999),
                lastname = "Name",
                password = "Test@123",
                mobile = $"+1234567{random.Next(999, 9999)}",
                email = randomUserEmail
            });

            var signUpResponse = client.Execute(signUpRequest);
            Assert.True(signUpResponse.IsSuccessful, "Sing up failed.");

            var forgotPasswordRequest = new RestRequest("/user/forgot-password-token", Method.Post);
            forgotPasswordRequest.AddJsonBody(new
            {
                Email = randomUserEmail
            });

            var forgotPasswordResponse = client.Execute(forgotPasswordRequest);
            Assert.True(forgotPasswordResponse.IsSuccessful, "Forgot password request failed.");
            Assert.That(forgotPasswordResponse.Content, Is.Not.Null, "Forgot password response is null.");
        }

        [Test]
        public void UserSignupLoginUpdateAndDeleteTest()
        {
            var randomUserEmail = $"newtest{random.Next(999, 9999)}@email.com";
            var signUpRequest = new RestRequest("/user/register", Method.Post);
            signUpRequest.AddJsonBody(new
            {
                firstname = "Name" + random.Next(999, 9999),
                lastname = "Name",
                password = "newTest@123",
                mobile = $"+1234567{random.Next(999, 9999)}",
                email = randomUserEmail
            });

            var signUpResponse = client.Execute(signUpRequest);
            Assert.True(signUpResponse.IsSuccessful, "Sing up failed.");
            Assert.That(signUpResponse.Content, Is.Not.Null, "Sign up response is null.");

            var loginRequest = new RestRequest("/user/login", Method.Post);
            loginRequest.AddJsonBody(new
            {
                email = randomUserEmail,
                password = "newTest@123"
            });

            var loginResponse = client.Execute(loginRequest);
            Assert.True(loginResponse.IsSuccessful, "Login failed.");
            Assert.That(loginResponse.Content, Is.Not.Null.Or.Empty, "Login response is null or empty.");

            string userToken = JObject.Parse(loginResponse.Content)["token"]?.ToString();
            Assert.That(userToken, Is.Not.Null.Or.Empty, "User token is null or empty.");

            var userId = JObject.Parse(loginResponse.Content)["_id"]?.ToString();
            Assert.That(userId, Is.Not.Null.Or.Empty, "User ID is null or empty");

            var updateUserEmail = $"updated{random.Next(999, 9999)}@example.com";
            var updateUserRequest = new RestRequest("/user/edit-user", Method.Put);
            updateUserRequest.AddHeader("Authorization", $"Bearer {userToken}");
            updateUserRequest.AddJsonBody(new
            {
                firstname = "Test",
                lastname = "Test",
                mobile = "+1234567998",
                email = updateUserEmail
            });

            var updateUserResponse = client.Execute(updateUserRequest);
            Assert.True(updateUserResponse.IsSuccessful, "Updating user failed.");
            Assert.That(updateUserResponse.Content, Is.Not.Null,
                "Update response data is null");

            var deleteUserRequest = new RestRequest($"/user/{userId}", Method.Delete);
            deleteUserRequest.AddHeader("Authorization", $"Bearer {token}");

            var deleteUserResponse = client.Execute(deleteUserRequest);
            Assert.True(deleteUserResponse.IsSuccessful, "User deletion failed");
        }

        [Test]
        public void ProductAndUserCartTest()
        {
            var productTitle = $"Title {random.Next(999, 9999)}";
            var createProductRequest = new RestRequest("/product", Method.Post);
            createProductRequest.AddHeader("Authorization", $"Bearer {token}");
            createProductRequest.AddJsonBody(new
            {
                title = productTitle,
                description = "Some description.",
                slug = "test-product",
                price = 9.99,
                category = "Electronics",
                brand = "Apple",
                quantity = 10
            });

            var createProductResponse = client.Execute(createProductRequest);
            Assert.True(createProductResponse.IsSuccessful, "Product creation failed");

            var productContent = JObject.Parse(createProductResponse.Content);
            var productId = productContent["_id"]?.ToString();
            Assert.That(productId, Is.Not.Null.Or.Empty, "Product ID should not be null or empty");

            var loginRequest = new RestRequest("/user/login", Method.Post);
            loginRequest.AddJsonBody(new
            {
                email = "john.doe@example.com",
                password = "password123"
            });

            var loginResponse = client.Execute(loginRequest);
            Assert.True(loginResponse.IsSuccessful, "Login failed.");
            Assert.That(loginResponse.Content, Is.Not.Null.Or.Empty, "Login response is null or empty.");

            string userToken = JObject.Parse(loginResponse.Content)["token"]?.ToString();
            Assert.That(userToken, Is.Not.Null.Or.Empty, "User token is null or empty.");

            var addToCartRequest = new RestRequest($"/user/cart", Method.Post);
            addToCartRequest.AddHeader("Authorization", $"Bearer {userToken}");
            addToCartRequest.AddJsonBody(new
            {
                cart = new[]
                {
                    new { _id = productId, count = 1, color = "Red" }
                }
            });

            var addToCartResponse = client.Execute(addToCartRequest);
            Assert.That(addToCartResponse.IsSuccessful, Is.True, "Adding product to cart failed");

            var addCouponRequest = new RestRequest($"/user/cart/applycoupon", Method.Post);
            addCouponRequest.AddHeader("Authorization", $"Bearer {userToken}");
            addCouponRequest.AddJsonBody(new
            {
                Coupon = "BLACKFRIDAY"
            });

            var addCouponResponse = client.Execute(addCouponRequest);
            Assert.That(addCouponResponse.IsSuccessful, Is.True, "Adding coupon to cart failed");

            var deleteProductRequest = new RestRequest($"/product/{productId}", Method.Delete);
            deleteProductRequest.AddHeader("Authorization", $"Bearer {token}");
            var deleteProductResponse = client.Execute(deleteProductRequest);
            Assert.That(deleteProductResponse.IsSuccessful, Is.True, "Product deletion failed");
        }
    }
}
