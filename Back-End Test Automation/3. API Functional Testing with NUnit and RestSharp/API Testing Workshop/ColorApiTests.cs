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
    public class ColorApiTests : IDisposable
    {
        private RestClient client;
        private string token;

        [SetUp]
        public void SetUp()
        {
            client = new RestClient(GlobalConstants.BaseUrl);
            token = GlobalConstants.AuthenticateUser("admin@gmail.com", "admin@gmail.com");

            Assert.That(token, Is.Not.Null.Or.Empty, "Token is null or empty.");
        }
        public void Dispose()
        {
            client?.Dispose();
        }

        [Test, Order(1)]
        public void Test_GetAllColors()
        {
            var request = new RestRequest("color", Method.Get);

            var response = client.Execute(request);

            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Status code is not OK.");
                Assert.That(response.Content, Is.Not.Empty, "Response content is empty.");

                var colors = JArray.Parse(response.Content);


                Assert.That(colors.Type, Is.EqualTo(JTokenType.Array));
                Assert.That(colors.Count, Is.GreaterThan(0));


                var colorTitles = colors.Select(c => c["title"]?.ToString()).ToList();
                Assert.That(colorTitles, Does.Contain("Black"));
                Assert.That(colorTitles, Does.Contain("White"));
                Assert.That(colorTitles, Does.Contain("Red"));

                foreach (var color in colors)
                {
                    Assert.That(color["title"]?.ToString(), Is.Not.Null.Or.Empty);
                    Assert.That(color["_id"]?.ToString(), Is.Not.Null.Or.Empty);

                }

                Assert.That(colors.Count, Is.EqualTo(10));
            });
        }

        [Test, Order(2)]
        public void Test_AddColor()
        {
            var request = new RestRequest("color", Method.Post);
            request.AddHeader("Authorization", $"Bearer {token}");
            request.AddJsonBody(new
            {
                title = "New Color"

            });

            var response = client.Execute(request);

            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(response.Content, Is.Not.Empty);

                var content = JObject.Parse(response.Content);

                Assert.That(content["_id"].ToString(), Is.Not.Null.And.Not.Empty);
                Assert.That(content["title"].ToString(), Is.EqualTo("New Color"));

                Assert.That(content.ContainsKey("createdAt"), Is.True);
                Assert.That(content.ContainsKey("updatedAt"), Is.True);

                Assert.That(DateTime.TryParse(content["createAt"]?.ToString(), out _), Is.True);
                Assert.That(DateTime.TryParse(content["updatedAt"]?.ToString(), out _), Is.True);

                Assert.That(content["createdAt"]?.ToString(), Is.EqualTo(content["updatedAt"]?.ToString()));
            });
        }

        [Test, Order(3)]
        public void Test_UpdateColor()
        {
            var getRequest = new RestRequest("color", Method.Get);

            var getResponse = client.Execute(getRequest);


            Assert.That(getResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Status code is not OK.");
            Assert.That(getResponse.Content, Is.Not.Empty, "Response content is empty.");

            var colors = JArray.Parse(getResponse.Content);
            var colorToUpdate = colors.FirstOrDefault(c => c["title"].ToString() == "Red");

            Assert.That(colorToUpdate, Is.Not.Empty);

            var colorId = colorToUpdate["_id"].ToString();

            var updateRequest = new RestRequest("color/{id}", Method.Put);
            updateRequest.AddUrlSegment("id", colorId);
            updateRequest.AddJsonBody(new
            {
                title = "Updated Color",
            });
            var updateResponse = client.Execute(updateRequest);
            Assert.Multiple(() =>
            {

                Assert.That(updateResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Status code is not OK.");
                Assert.That(updateResponse.Content, Is.Not.Empty, "Response content is empty.");

                var content = JObject.Parse(updateResponse.Content);

                Assert.That(content["title"].ToString(), Is.EqualTo("Updated Color"));
                Assert.That(content["_id"].ToString(), Is.EqualTo(colorId));

                Assert.That(content.ContainsKey("createdAt"), Is.True);
                Assert.That(content.ContainsKey("updatedAt"), Is.True);

                Assert.That(DateTime.TryParse(content["createAt"]?.ToString(), out _), Is.True);
                Assert.That(DateTime.TryParse(content["updatedAt"]?.ToString(), out _), Is.True);

                Assert.That(content["createdAt"]?.ToString(), Is.Not.EqualTo(content["updatedAt"]?.ToString()));
            });
        }

        [Test, Order(4)]
        public void Test_DeleteColor()
        {
            var getRequest = new RestRequest("color", Method.Get);

            var getResponse = client.Execute(getRequest);


            Assert.That(getResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Status code is not OK.");
            Assert.That(getResponse.Content, Is.Not.Empty, "Response content is empty.");

            var colors = JArray.Parse(getResponse.Content);
            var colorToDelete = colors.FirstOrDefault(c => c["title"].ToString() == "Black");

            Assert.That(colorToDelete, Is.Not.Null);

            var colorId = colorToDelete["_id"].ToString();

            var deleteRequest = new RestRequest("color/{id}", Method.Delete);
            deleteRequest.AddUrlSegment("id", colorId);
            deleteRequest.AddHeader("Authorization", $"Bearer {token}");

            var deleteResponse = client.Execute(deleteRequest);

            Assert.Multiple(() =>
            {
                Assert.That(deleteResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));

                var verifyGetRequest = new RestRequest("color/{id}", Method.Get);
                verifyGetRequest.AddUrlSegment("id", colorId);

                var verifyResponse = client.Execute(verifyGetRequest);

                Assert.That(verifyResponse.Content, Is.Null.Or.EqualTo("null"));

                var refreshedGetResponse = client.Execute(getRequest);
                var refreshedColors = JArray.Parse(refreshedGetResponse.Content);
                var colorExists = refreshedColors.Any(c => c["title"].ToString() == "Black");

                Assert.That(colorExists, Is.False);
            });

        }
    }
}
