using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ApiFunctTesting
{
    [TestFixture]
    public class ProductManagementTests
    {
        private RestClient client;
        private string adminToken;
        private string userToken;
        private Random random;

        [TearDown]
        public void TearDown()
        {
            client.Dispose();
        }

        [SetUp]
        public void Setup()
        {
            client = new RestClient(GlobalConstants.BaseUrl);
            adminToken = GlobalConstants.AuthenticateUser("admin@gmail.com", "admin@gmail.com");
            userToken = GlobalConstants.AuthenticateUser("john.doe@example.com", "password123");
            random = new Random();
        }

        [Test]
        public void ProductLifecycleTest()
        {
            var productTitle = $"Test {random.Next(999, 9999)}";

            var createProductRequest = new RestRequest("/product", Method.Post);
            createProductRequest.AddHeader("Authorization", $"Bearer {adminToken}");
            createProductRequest.AddJsonBody(new
            {
                title = productTitle,
                description = "Some description",
                slug = "test-product",
                price = 9.99,
                category = "Electronics",
                brand = "Apple",
                quantity = 10
            });

            var createProductResponse = client.Execute(createProductRequest);
            Assert.IsTrue(createProductResponse.IsSuccessful, "Product creation failed.");

            var productId = JObject.Parse(createProductResponse.Content)["_id"]?.ToString();
            Assert.That(productId, Is.Not.Null.Or.Empty, "Product ID is null or empty.");

            var getProductRequest = new RestRequest($"/product/{productId}", Method.Get);
            var getProductResponse = client.Execute(getProductRequest);
            Assert.IsTrue(getProductResponse.IsSuccessful, "Failed to get product.");
            Assert.That(getProductResponse.Content, Is.Not.Null.Or.Empty, "Product is null or empty.");

            var updateProductTitle = $"Updated {random.Next(999, 9999)}";
            var updateProductRequest = new RestRequest($"/product/{productId}", Method.Put);
            updateProductRequest.AddHeader("Authorization", $"Bearer {adminToken}");
            updateProductRequest.AddJsonBody(new
            {
                title = updateProductTitle,
                description = "Updated description",
                price = 39.99
            });

            var updateProductResponse = client.Execute(updateProductRequest);
            Assert.True(updateProductResponse.IsSuccessful, "Product update failed.");

            var deleteProductRequest = new RestRequest($"/product/{productId}", Method.Delete);
            deleteProductRequest.AddHeader("Authorization", $"Bearer {adminToken}");
            var deleteProductResponse = client.Execute(deleteProductRequest);
            Assert.True(deleteProductResponse.IsSuccessful, "Product deletion failed.");

            var verifyDeleteRequest = new RestRequest($"/product/{productId}", Method.Get);
            var verifyDeleteResponse = client.Execute(verifyDeleteRequest);
            Assert.That(verifyDeleteResponse.Content, Is.Null.Or.EqualTo("null"), "Product is not deleted.");
        }

        [Test]
        public void ProductRatingsLifecycleTest()
        {
            var getProductsListRequest = new RestRequest("/product", Method.Get);
            var getProductsListResponse = client.Execute(getProductsListRequest);
            Assert.True(getProductsListResponse.IsSuccessful, "Failed to get products list.");

            var products = JArray.Parse(getProductsListResponse.Content);
            Assert.That(products.Count, Is.GreaterThan(0), "No products found.");

            var randomProduct = products[new Random().Next(products.Count)];
            var productId = randomProduct["_id"]?.ToString();
            Assert.That(productId, Is.Not.Null.Or.Empty, "Product ID is null or empty.");

            var addReviewRequest = new RestRequest("/product/rating", Method.Put);
            addReviewRequest.AddHeader("Authorization", $"Bearer {userToken}");
            addReviewRequest.AddJsonBody(new
            {
                star = 5,
                prodId = productId,
                comment = "Some comment here."
            });

            var addReviewResponse = client.Execute(addReviewRequest);
            Assert.That(addReviewResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK), "addReviewResponse status code is not ok.");

            var addToWishListRequest = new RestRequest("/product/wishlist", Method.Put);
            addToWishListRequest.AddHeader("Authorization", $"Bearer {userToken}");
            addToWishListRequest.AddJsonBody(new
            {
                prodId = productId
            });

            var addToWishListResponse = client.Execute(addToWishListRequest);
            Assert.That(addToWishListResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Adding to product to wishlist failed.");
        }

        [Test]
        public void ComplexProductInteractionTest()
        {
            var getProductsListRequest = new RestRequest("/product", Method.Get);
            var getProductsListResponse = client.Execute(getProductsListRequest);
            Assert.True(getProductsListResponse.IsSuccessful, "Failed to get products list.");

            var products = JArray.Parse(getProductsListResponse.Content);
            Assert.That(products.Count, Is.GreaterThan(0), "No products found.");

            var randomProduct = products[new Random().Next(products.Count)];
            var productId = randomProduct["_id"]?.ToString();
            Assert.That(productId, Is.Not.Null.Or.Empty, "Product ID is null or empty.");

            var addToWishListRequest = new RestRequest("/product/wishlist", Method.Put);
            addToWishListRequest.AddHeader("Authorization", $"Bearer {userToken}");
            addToWishListRequest.AddJsonBody(new
            {
                prodId = productId
            });

            var addToWishListResponse = client.Execute(addToWishListRequest);
            Assert.That(addToWishListResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Adding to product to wishlist failed.");

            var uploadPhotoRequest = new RestRequest($"/product/upload/{productId}", Method.Put);
            uploadPhotoRequest.AddHeader("Authorization", $"Bearer {adminToken}");
            uploadPhotoRequest.AddJsonBody(new
            {
                images = new[]
                {
                    "https://example.com/image1.jpg",
                    "https://example.com/image2.jpg"
                }
            });

            var uploadPhotoResponse = client.Execute(uploadPhotoRequest);
            Assert.True(uploadPhotoResponse.IsSuccessful, "Uploading photo failed.");

            var addReviewRequest = new RestRequest("/product/rating", Method.Put);
            addReviewRequest.AddHeader("Authorization", $"Bearer {userToken}");
            addReviewRequest.AddJsonBody(new
            {
                star = 5,
                prodId = productId,
                comment = "Some comment here."
            });

            var addReviewResponse = client.Execute(addReviewRequest);
            Assert.That(addReviewResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK), "addReviewResponse status code is not ok.");

            var removeFromWishlistRequest = new RestRequest($"/product/wishlist", Method.Put);
            removeFromWishlistRequest.AddHeader("Authorization", $"Bearer {userToken}");
            removeFromWishlistRequest.AddJsonBody(new
            {
                prodId = productId
            });
            var removeFromWishlistResponse = client.Execute(removeFromWishlistRequest);
            Assert.True(removeFromWishlistResponse.IsSuccessful,
                "Removing product from wishlist failed");
        }
    }
}
