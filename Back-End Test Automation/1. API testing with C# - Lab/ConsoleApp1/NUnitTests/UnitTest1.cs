using RestSharp;
using RestSharp.Authenticators;

namespace NUnitTests
{
    public class UnitTest
    {

        private RestClient client;

        [SetUp]
        public void Setup()
        {
            var options = new RestClientOptions("https://api.github.com")
            {
                MaxTimeout = 3000, 
            };
            this.client = new RestClient(options);

        }
        [TearDown]
        public void Teardown()
        {
            this.client.Dispose();
        }

        [Test]
        public void Test_GitHubApiRequest()
        {

            var request = new RestRequest("/repos/testnakov/test-nakov-repo/issues", Method.Get);

            var response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
        }
    }
}