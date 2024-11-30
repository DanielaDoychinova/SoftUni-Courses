
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Net;

namespace API_Testing_Workshop
{
    public class BrandApiTests : IDisposable
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
        public void Test_GetAllBrands()
        {
            var request = new RestRequest("brand", Method.Get);

            var response = client.Execute(request);

            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Status code is not OK.");
                Assert.That(response.Content, Is.Not.Empty, "Response content is empty.");

                var brands = JArray.Parse(response.Content);


                Assert.That(brands.Type, Is.EqualTo(JTokenType.Array));
                Assert.That(brands.Count, Is.GreaterThan(0));

                var firstBrand = brands.FirstOrDefault();
                Assert.That(firstBrand, Is.Not.Null);

                var brandNames = brands.Select(b => b["title"]?.ToString()).ToList();
                Assert.That(brandNames, Does.Contain("TechCorp"));
                Assert.That(brandNames, Does.Contain("GameMaster"));

                foreach (var brand in brands)
                {
                    Assert.That(brand["title"]?.ToString(), Is.Not.Null.Or.Empty);
                    Assert.That(brand["_id"]?.ToString(), Is.Not.Null.Or.Empty);

                }

                Assert.That(brands.Count, Is.GreaterThan(5));
            });
        }


        [Test, Order(2)]
        public void Test_AddBrand()
        {
            var request = new RestRequest("brand", Method.Post);
            request.AddHeader("Authorization", $"Bearer {token}");
            request.AddJsonBody(new
            {
                title = "New Brand"

            });

            var response = client.Execute(request);

            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(response.Content, Is.Not.Empty);

                var content = JObject.Parse(response.Content);

                Assert.That(content["_id"].ToString(), Is.Not.Null.And.Not.Empty);
                Assert.That(content["title"].ToString(), Is.EqualTo("New Blog Title"));

            });
        }

        [Test, Order(3)]
        public void Test_UpdateBrand()
        {
            var getRequest = new RestRequest("brand", Method.Get);

            var getResponse = client.Execute(getRequest);


            Assert.That(getResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Status code is not OK.");
            Assert.That(getResponse.Content, Is.Not.Empty, "Response content is empty.");

            var brands = JArray.Parse(getResponse.Content);
            var brandToUpdate = brands.FirstOrDefault(b => b["title"].ToString() == "GameMaster");

            Assert.That(brandToUpdate, Is.Not.Empty);

            var brandId = brandToUpdate["_id"].ToString();

            var updateRequest = new RestRequest("brand/{id}", Method.Put);
            updateRequest.AddUrlSegment("id", brandId);
            updateRequest.AddJsonBody(new
            {
                title = "Updated Brand Title",
            });
            var updateResponse = client.Execute(updateRequest);
            Assert.Multiple(() =>
            {

                Assert.That(updateResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Status code is not OK.");
                Assert.That(updateResponse.Content, Is.Not.Empty, "Response content is empty.");

                var content = JObject.Parse(updateResponse.Content);

                Assert.That(content["title"].ToString(), Is.EqualTo("Updated Brand Title"));
                Assert.That(content["_id"].ToString(), Is.EqualTo(brandId));

                Assert.That(content.ContainsKey("createdAt"), Is.True);
                Assert.That(content.ContainsKey("updatedAt"), Is.True);

                Assert.That(content["updatedAt"]?.ToString(), Is.Not.EqualTo(content["createdAt"]?.ToString()));
            });
        }

        [Test, Order(4)]
        public void Test_DeleteBrand()
        {
            var getRequest = new RestRequest("brand", Method.Get);

            var getResponse = client.Execute(getRequest);


            Assert.That(getResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Status code is not OK.");
            Assert.That(getResponse.Content, Is.Not.Empty, "Response content is empty.");

            var brands = JArray.Parse(getResponse.Content);
            var brandToDelete = brands.FirstOrDefault(b => b["title"].ToString() == "ViewTech");

            Assert.That(brandToDelete, Is.Not.Null);

            var brandId = brandToDelete["_id"].ToString();

            var deleteRequest = new RestRequest("brand/{id}", Method.Delete);
            deleteRequest.AddUrlSegment("id", brandId);
            deleteRequest.AddHeader("Authorization", $"Bearer {token}");

            var deleteResponse = client.Execute(deleteRequest);

            Assert.Multiple(() =>
            {
                Assert.That(deleteResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));

                var verifyGetRequest = new RestRequest("brand/{id}", Method.Get);
                verifyGetRequest.AddUrlSegment("id", brandId);

                var verifyResponse = client.Execute(verifyGetRequest);

                Assert.That(verifyResponse.Content, Is.Null.Or.EqualTo("null"));
            });

        }
    }
}
