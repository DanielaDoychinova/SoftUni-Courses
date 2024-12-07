using Newtonsoft.Json;
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
    public class CouponMenagementTest
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
        public void CouponLifecycleTest()
        {
            //Get all products 
            var getAllProductsRequest = new RestRequest("/product", Method.Get);
            
            var getAllProductsResponse = client.Execute(getAllProductsRequest);

            Assert.IsTrue(getAllProductsResponse.IsSuccessful, "Getting all products failed.");

            var products = JArray.Parse(getAllProductsResponse.Content);

            Assert.That(products.Count, Is.GreaterThanOrEqualTo(2), "There are less than two products.");

            //Get 2 random products 
            var productIds = products.Select(p => p["_id"].ToString()).ToList();

            var firstProductId = productIds[random.Next(productIds.Count)];
            var secondProductId = productIds[random.Next(productIds.Count)];

            while (firstProductId == secondProductId)
            {
                secondProductId = productIds[random.Next(productIds.Count)];
            }

            //Create coupon
            string couponName = $"DISCOUNT_{random.Next(999, 9999)}";

            var createCouponRequest = new RestRequest("/coupon", Method.Post);
            createCouponRequest.AddHeader("Authorization", $"Bearer {adminToken}");
            createCouponRequest.AddJsonBody(new
            {
                name = couponName,
                discount = 20,
                expiry = "2024-09-09",
                
            });

            var createCouponResponse = client.Execute(createCouponRequest);

            Assert.IsTrue(createCouponResponse.IsSuccessful, "Coupon creation failed.");

            var couponId = JObject.Parse(createCouponResponse.Content)["_id"]?.ToString();

            Assert.That(couponId, Is.Not.Null.Or.Empty, "Coupon ID is null or empty.");

            //Create shopping cart and add 2 products
            var createCartRequest = new RestRequest("/user/cart", Method.Post);
            createCartRequest.AddHeader("Authorization", $"Bearer {adminToken}");
            createCartRequest.AddJsonBody(new
            {
                cart = new[]
                {
                    new { _id = firstProductId, count = 1, color = "red" },
                    new { _id = secondProductId, count = 2, color = "blue" }
                }
            });

            var createCartResponse = client.Execute(createCartRequest);
            Assert.That(createCartResponse.IsSuccessful, Is.True, "Cart creation failed");

            var applyCouponRequest = new RestRequest("/user/cart/applycoupon", Method.Post);
            applyCouponRequest.AddHeader("Authorization", $"Bearer {adminToken}");
            applyCouponRequest.AddJsonBody(new
            {
                coupon = couponName
            });

            var applyCouponResponse  = client.Execute(applyCouponRequest);

            Assert.IsTrue(applyCouponResponse.IsSuccessful, "Applying coupon failed.");

            //Delete

            var deleteCouponRequest = new RestRequest($"/coupon/{couponId}", Method.Delete);
            deleteCouponRequest.AddHeader("Authorization", $"Bearer {adminToken}");

            var deleteResponse = client.Execute(deleteCouponRequest);

            Assert.IsTrue(deleteResponse.IsSuccessful, "Coupon deletion failed.");

            var verifyRequest = new RestRequest($"/coupon/{couponId}", Method.Get);
            verifyRequest.AddHeader("Authorization", $"Bearer {adminToken}");

            var verifyResponse = client.Execute(verifyRequest);

            Assert.That(verifyResponse.Content, Is.Null.Or.EqualTo("null"), "Coupon was not deleted.");
        }

        [Test]
        public void CouponApplicationToOrderTest()
        {
            var getAllProductsRequest = new RestRequest("/product/", Method.Get);
            getAllProductsRequest.AddHeader("Authorization", $"Bearer {adminToken}");

            var getAllProductsResponse = client.Execute(getAllProductsRequest);

            Assert.IsTrue(getAllProductsResponse.IsSuccessful, "Getting all products failed.");

            var products = JArray.Parse(getAllProductsResponse.Content);

            Assert.That(products.Count, Is.GreaterThan(0), "No products found.");

            string productId = products.First()["_id"]?.ToString();

            Assert.That(productId, Is.Not.Null.And.Not.Empty, "Product ID is null or empty.");

            string couponName = $"DISCOUNT_{random.Next(999, 9999)}";

            var createCouponRequest = new RestRequest("/coupon/", Method.Post);
            createCouponRequest.AddHeader("Authorization", $"Bearer {adminToken}");
            createCouponRequest.AddJsonBody(new
            {
                name = couponName,
                expiry = "2024-09-09T23:59:59Z",
                discount = 20
            });

            var createCouponResponse = client.Execute(createCouponRequest);

            Assert.IsTrue(createCouponResponse.IsSuccessful, "Coupon creation failed.");

            var addToCartRequest = new RestRequest("/user/cart", Method.Post);
            addToCartRequest.AddHeader("Authorization", $"Bearer {userToken}");
            addToCartRequest.AddJsonBody(new
            {
                cart = new[]
                {
                    new { _id = productId, count = 2, color = "Red"},
                }
            });

            var addToCartResponse = client.Execute(addToCartRequest);

            Assert.That(addToCartResponse.IsSuccessful, Is.True, "Adding product to cart failed");

            var applyCouponRequest = new RestRequest("/user/cart/applycoupon", Method.Post);
            applyCouponRequest.AddHeader("Authorization", $"Bearer {userToken}");
            applyCouponRequest.AddJsonBody(new
            {
                coupon = couponName
            });

            var applyCouponResponse = client.Execute(applyCouponRequest);

            Assert.IsTrue(applyCouponResponse.IsSuccessful, "Applying coupon failed.");

            var placeOrderRequest = new RestRequest("/user/cart/cash-order", Method.Post);
            placeOrderRequest.AddHeader("Authorization", $"Bearer {userToken}");
            placeOrderRequest.AddJsonBody(JsonConvert.SerializeObject( new
            {
                COD = true,
                couponApplied = true
            }));

            var placeOrderResponse = client.Execute(placeOrderRequest);

            Assert.That(placeOrderResponse.IsSuccessful, Is.True, "Placing order failed.");
        }
    }
}
