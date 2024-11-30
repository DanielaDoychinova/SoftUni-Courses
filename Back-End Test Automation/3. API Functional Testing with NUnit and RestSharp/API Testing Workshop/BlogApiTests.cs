
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Net;

namespace API_Testing_Workshop
{
    public class BlogApiTests : IDisposable
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
        public void Test_GetAllBlogs()
        {
            var request = new RestRequest("blog", Method.Get);

            var response = client.Execute(request);

            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Status code is not OK.");
                Assert.That(response.Content, Is.Not.Empty, "Response content is empty.");

                var blogs = JArray.Parse(response.Content);

                Assert.That(blogs.Count, Is.GreaterThan(0));

                foreach (var blog in blogs)
                {
                    Assert.That(blog["title"]?.ToString(), Is.Not.Null.Or.Empty);
                    Assert.That(blog["description"]?.ToString(), Is.Not.Null.Or.Empty);
                    Assert.That(blog["author"]?.ToString(), Is.Not.Null.Or.Empty);
                    Assert.That(blog["category"]?.ToString(), Is.Not.Null.Or.Empty);
                }
            });
        }

        [Test, Order(2)]
        public void Test_AddBlog()
        {
            var request = new RestRequest("blog", Method.Post);
            request.AddHeader("Authorization", $"Bearer {token}");
            request.AddJsonBody(new
            {
                title = "New Blog Title",
                description = "New description",
                category = "test",

            });

            var response = client.Execute(request);

            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(response.Content, Is.Not.Empty);

                var content = JObject.Parse(response.Content);

                Assert.That(content["title"].ToString(), Is.EqualTo("New Blog Title"));
                Assert.That(content["description"].ToString(), Is.EqualTo("New description"));
                Assert.That(content["category"].ToString(), Is.EqualTo("test"));
                Assert.That(content["author"].ToString(), Is.Not.Null.And.Not.Empty);
            });
        }

        [Test, Order(3)]
        public void Test_UpdateBlog()
        {
            var getRequest = new RestRequest("blog", Method.Get);

            var getResponse = client.Execute(getRequest);

            
                Assert.That(getResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Status code is not OK.");
                Assert.That(getResponse.Content, Is.Not.Empty, "Response content is empty.");

                var blogs = JArray.Parse(getResponse.Content);
                var blogToUpdate = blogs.FirstOrDefault(b => b["title"].ToString() == "New Blog Title");

                var blogId = blogToUpdate["_id"].ToString();

                var updateRequest = new RestRequest("blog/{id}", Method.Put);
                updateRequest.AddUrlSegment("id", blogId);
                updateRequest.AddJsonBody(new
                {
                    title = "Updated Blog Title",
                    description = "Updated Description",
                    category = "upadated"
                });
                var updateResponse = client.Execute(updateRequest);
            Assert.Multiple(() =>
            {

                Assert.That(updateResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Status code is not OK.");
                Assert.That(updateResponse.Content, Is.Not.Empty, "Response content is empty.");

                var content = JObject.Parse(updateResponse.Content);

                Assert.That(content["title"].ToString(), Is.EqualTo("Updated Blog Title"));
                Assert.That(content["description"].ToString(), Is.EqualTo("Updated description"));
                Assert.That(content["category"].ToString(), Is.EqualTo("updated"));
                Assert.That(content["author"].ToString(), Is.Not.Null.And.Not.Empty);
            });
        }

        [Test, Order(4)]
        public void Test_DeleteBlog()
        {
            var getRequest = new RestRequest("blog", Method.Get);

            var getResponse = client.Execute(getRequest);


            Assert.That(getResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Status code is not OK.");
            Assert.That(getResponse.Content, Is.Not.Empty, "Response content is empty.");

            var blogs = JArray.Parse(getResponse.Content);
            var blogToDelete = blogs.FirstOrDefault(b => b["title"].ToString() == "The Evolution of Entertainment in the Digital Age");

            Assert.That(blogToDelete, Is.Not.Null);

            var blogId = blogToDelete["_id"].ToString();

            var deleteRequest = new RestRequest("blog/{id}", Method.Delete);
            deleteRequest.AddUrlSegment("id", blogId);
            deleteRequest.AddHeader("Authorization", $"Bearer {token}");

            var deleteResponse = client.Execute(deleteRequest);

            Assert.Multiple(() =>
            {
                Assert.That(deleteResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));

                var verifyGetRequest = new RestRequest("blog/{id}", Method.Get);
                verifyGetRequest.AddUrlSegment("id", blogId);

                var verifyResponse = client.Execute(verifyGetRequest);

                Assert.That(verifyResponse.Content, Is.Null.Or.EqualTo("null"));
            });

        }

    }}
