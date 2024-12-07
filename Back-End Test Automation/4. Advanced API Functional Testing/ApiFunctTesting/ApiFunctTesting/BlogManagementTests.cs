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
    public class BlogManagementTests
    {
        private RestClient client;
        private string token;
        private Random random;

        [TearDown]
        public void TearDown()
        {
            client.Dispose();
        }

        [SetUp] public void Setup()
        {
            client = new RestClient(GlobalConstants.BaseUrl);
            token = GlobalConstants.AuthenticateUser("admin@gmail.com", "admin@gmail.com");
            random = new Random();
        }

        [Test]
        public void BlogPostLifecycleTes()
        {
            var createBlogPostRequest = new RestRequest("/blog/", Method.Post);
            createBlogPostRequest.AddHeader("Authorization", "Bearer " + token);
            createBlogPostRequest.AddJsonBody(new
            {
                title = $"Title_{random.Next(999, 9999)}",
                description = "Some descr",
                category = "Some"
            });

            //Create

            var createBlogResponse = client.Execute(createBlogPostRequest);

            string blogId = JObject.Parse(createBlogResponse.Content)["_id"]?.ToString();


            Assert.That(createBlogResponse.IsSuccessful, Is.True, "Blog creation failed.");
            Assert.That(blogId, Is.Not.Null.Or.Empty, "Blog ID is null or empty.");

            //Update
            var updateRequest = new RestRequest($"/blog/{blogId}", Method.Put);
            updateRequest.AddHeader("Authorization", $"Bearer {token}");
            updateRequest.AddJsonBody(new
            {
                title = $"UpdatedTitle_{random.Next(999, 9999)}",
                description = "Updated descr",
            });

            var updateResponse = client.Execute(updateRequest);

            Assert.That(updateResponse.IsSuccessful, Is.True, "Updating blog failed.");


            //Delete
            var deleteRequest = new RestRequest($"/blog/{blogId}", Method.Delete);
            deleteRequest.AddHeader("Authorization", $"Bearer {token}");

            var deleteResponse = client.Execute(deleteRequest);

            Assert.That(deleteResponse.IsSuccessful, Is.True, "Blog deletion failed.");

            var verifyRequest = new RestRequest($"/blog/{blogId}", Method.Get);

            var verifyResponse = client.Execute(verifyRequest);

            Assert.That(verifyResponse.Content, Is.Null.Or.EqualTo("null"), "Blog is not deleted.");
        }




    }
}
