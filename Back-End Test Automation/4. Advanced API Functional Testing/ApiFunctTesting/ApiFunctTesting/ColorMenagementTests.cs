
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Drawing;
using System.Net;

namespace ApiFunctTesting
{
    [TestFixture]
    public class ColorMenagementTests
    {
        private RestClient client;
        private string token;
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
            token = GlobalConstants.AuthenticateUser("admin@gmail.com", "admin@gmail.com");
            random = new Random();
        }

        [Test]
        public void ColorLifecycleTest()
        {
            //Add
            
            var addColorRequest = new RestRequest("/color/", Method.Post);
            addColorRequest.AddHeader("Authorization", $"Bearer {token}");
            addColorRequest.AddJsonBody(new
            {
                title = $"Color_{random.Next(999, 9999)}",
            });

            var addResponse = client.Execute(addColorRequest);

            Assert.That(addResponse.IsSuccessful, Is.True, "Adding color failed.");


            //Get request by id
            var colorId = JObject.Parse(addResponse.Content)["_id"]?.ToString();

            Assert.That(colorId, Is.Not.Null.Or.Empty, "Color ID is null or empty.");

            var getColorRequest = new RestRequest($"/color/{colorId}", Method.Get);

            var getColorResponse = client.Execute(getColorRequest);

            Assert.IsTrue(getColorResponse.IsSuccessful, "Failed getting color.");

            //Delete

            var deleteColorRequest = new RestRequest($"/color/{colorId}", Method.Delete);
            deleteColorRequest.AddHeader("Authorization", $"Bearer {token}");

            var deleteResponse = client.Execute(deleteColorRequest);

            Assert.IsTrue(deleteResponse.IsSuccessful, "Color deletion failed.");

            //Verify the color is deleted
            var verifyRequest = new RestRequest($"/color/{colorId}", Method.Get);

            var verifyResponse = client.Execute(verifyRequest);

            Assert.That(verifyResponse.Content, Is.Null.Or.EqualTo("null"), "Color is not deleted.");
        }

        [Test]
        public void ColorLifecycleNegativeTes()
        {
            //Create color with invalid token

            var invalidToken = "InvalidToken";

            var addColorRequest = new RestRequest("/color/", Method.Post);
            addColorRequest.AddHeader("Authorization", $"Bearer {invalidToken}");
            addColorRequest.AddJsonBody(new
            {
                title = $"Color_{random.Next(999, 9999)}",
            });

            var addResponse = client.Execute(addColorRequest);

            Assert.IsFalse(addResponse.IsSuccessful, "Color is added.");
            Assert.That(addResponse.StatusCode, Is.EqualTo(HttpStatusCode.InternalServerError), "Status code is not error.");

            //Get with invalid id
            var invalidId = "invalidId";

            var getColorRequest = new RestRequest($"/color/{invalidId}", Method.Get);

            var getColorResponse = client.Execute(getColorRequest);

            Assert.IsFalse(getColorResponse.IsSuccessful, "Color with invalid ID is found.");
            Assert.That(getColorResponse.StatusCode, Is.EqualTo(HttpStatusCode.InternalServerError), "Status code is not error.");

            //Delete with invalid id
            var deleteColorRequest = new RestRequest($"/color/{invalidId}", Method.Delete);
            deleteColorRequest.AddHeader("Authorization", $"Bearer {token}");

            var deleteResponse = client.Execute(deleteColorRequest);

            Assert.IsFalse(deleteResponse.IsSuccessful, "Deletion is successful.");
            Assert.That(deleteResponse.StatusCode, Is.EqualTo(HttpStatusCode.InternalServerError), "Status code is not error.");
        }

    }
}
