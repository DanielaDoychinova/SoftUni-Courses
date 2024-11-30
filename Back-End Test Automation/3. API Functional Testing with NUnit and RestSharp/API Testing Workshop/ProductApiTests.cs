using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace API_Testing_Workshop
{
    [TestFixture]

    public class ProductApiTests : IDisposable
    {
        private RestClient client;
        private string token;

        [SetUp] public void SetUp() {
            client = new RestClient(GlobalConstants.BaseUrl);
            token = GlobalConstants.AuthenticateUser("admin@gmail.com", "admin@gmail.com");

            Assert.That(token, Is.Not.Null.Or.Empty, "Token is null or empty.");
        }
        public void Dispose()
        {
            client?.Dispose();
        }

        [Test, Order(1)]
        public void Test_GetAllProducts()
        {
            var request = new RestRequest("product", Method.Get);
            var response = client.Execute(request);

            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Status code is not OK.");
                Assert.That(response.Content, Is.Not.Empty, "Response content is empty.");

                var content = JArray.Parse(response.Content);

                var productTitle = new[]
                {
                    "Smartphone Alfa", "Wireless Headphones", "Gaming Laptop", "4K Ultra HD TV", "Smartwatch Pro"
                };

                foreach (var title in productTitle)
                {
                    Assert.That(content.ToString(), Does.Contain(title));
                }

                var expectedPrices = new Dictionary<string, decimal>
                {
                    { "Smartphone Alfa", 999 },
                    { "Wireless Headphones", 199},
                    { "Gaming Laptop", 1499},
                    { "4K Ultra HD TV", 899},
                    { "Smartwatch Pro", 299}
                };

                foreach (var product in content)
                {
                    var title = product["title"].ToString();

                    if (expectedPrices.ContainsKey(title))
                    {
                        Assert.That(product["price"].Value<decimal>(), Is.EqualTo(expectedPrices[title]));

                    }
                }
            });
        }

        [Test, Order(2)]
        public void Test_AddProduct()
        {
            var request = new RestRequest("product", Method.Post);
            request.AddHeader("Authorization", $"Bearer {token}");
            request.AddJsonBody(new
            {
                title = "New Test Product",
                slug = "new-test-product",
                description = "New description",
                price = 99.99,
                category = "test",
                brand = "test",
                quantity = 50
            });

            var response = client.Execute(request);

            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(response.Content, Is.Not.Empty);

                var content = JObject.Parse(response.Content);

                Assert.That(content["title"].ToString(), Is.EqualTo("New Test Product"));
                Assert.That(content["slug"].ToString(), Is.EqualTo("new-test-product"));
                Assert.That(content["description"].ToString(), Is.EqualTo("New description"));
                Assert.That(content["price"].Value<decimal>(), Is.EqualTo(99.99));
                Assert.That(content["category"].ToString(), Is.EqualTo("test"));
                Assert.That(content["brand"].ToString(), Is.EqualTo("test"));
                Assert.That(content["quantity"].Value<decimal>(), Is.EqualTo(50));
            });
        }

        [Test, Order(3)]
        public void Test_UpdateProduct_InvalidProductId()
        {
            var invalidId = "InvalidID";

            var updateRequest = new RestRequest("product/{id}", Method.Put);
            updateRequest.AddUrlSegment("id", invalidId);
            updateRequest.AddHeader("Authorization", $"Bearer {token}");
            updateRequest.AddJsonBody(new
            {
                title = "Updated Test Product",
                slug = "updated-test-product",
                description = "updated description",
                price = 99.99,
                category = "test",
                brand = "test",
                quantity = 50
            });

            var updatedResponse = client.Execute(updateRequest);

            Assert.Multiple(() =>
            {
                Assert.That(updatedResponse.StatusCode, Is.EqualTo(HttpStatusCode.InternalServerError).Or.EqualTo(HttpStatusCode.BadRequest));
                Assert.That(updatedResponse.Content, Does.Contain("This id is not valid or not Found").Or.Contain("Invalid ID"));
            });
        }

        [Test, Order(4)]
        public void Test_DeleteProduct()
        {
            var getRequest = new RestRequest("product", Method.Get);
            var getResponse = client.Execute(getRequest);

            Assert.That(getResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Status code is not OK.");
            Assert.That(getResponse.Content, Is.Not.Empty, "Response content is empty.");

            var products = JArray.Parse(getResponse.Content);
            var productToDelete = products.FirstOrDefault(p => p["slug"]?.ToString() == "electric-bike");

            Assert.That(productToDelete, Is.Not.Null);

            var productId = productToDelete["_id"]?.ToString();

            var deleteRequest = new RestRequest("product/{id}", Method.Delete);
            deleteRequest.AddUrlSegment("id", productId);
            deleteRequest.AddHeader("Authorization", $"Bearer {token}");

            var deleteResponse = client.Execute(deleteRequest);

            Assert.That(deleteResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var verifyRequest = new RestRequest("product/{id}", Method.Get);
            verifyRequest.AddUrlSegment("id", productId);
            var verifyResponse = client.Execute(verifyRequest);

            Assert.That(verifyResponse.Content, Is.EqualTo("null"));

        }
    }
}
