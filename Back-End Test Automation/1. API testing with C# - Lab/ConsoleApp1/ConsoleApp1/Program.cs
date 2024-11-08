using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System.Text.Json.Serialization;

namespace HTTPRequests

{
    public class Program
    {

        static void Main(string[] args)
        {
            //Executing simple HTTPget request

            var client = new RestClient("https://api.github.com");

            var request = new RestRequest("/users/softuni/repos", Method.Get);

            var response = client.Execute(request);

            //Console.WriteLine(response.StatusCode);
           // Console.WriteLine(response.Content);


            //using URL segments
            var requestUrlSegments = new RestRequest("/repos/{user}/{repo}/issues/{id}", Method.Get);
            requestUrlSegments.AddUrlSegment("user", "testnakov");
            requestUrlSegments.AddUrlSegment("repo", "test-nakov-repo");
            requestUrlSegments.AddUrlSegment("id", 1);

            var responseUrlSegments = client.Execute(requestUrlSegments);

            Console.WriteLine(responseUrlSegments.StatusCode);
            Console.WriteLine(responseUrlSegments.Content);

            //deserializing json response
            var requestDeserializing = new RestRequest("/users/softuni/repos", Method.Get);

            var resposeDeserialiizing = client.Execute(requestDeserializing);

            var repos = JsonConvert.DeserializeObject<List<Repo>>(resposeDeserialiizing.Content);

            //http post with authentication

            var clientWithAuthentication = new RestClient(new RestClientOptions("https://api.github.com")
            {
                Authenticator = new HttpBasicAuthenticator("userName", "api-Token")
            });

            var postRequest = new RestRequest("/repos/testnakov/test-nakov-repo/issues", Method.Post);
            postRequest.AddHeader("Content-type", "application/json");
            postRequest.AddJsonBody(new { title = "SomeTittle", body = "SomeBody" });

            var responsePost = clientWithAuthentication.Execute(postRequest);

            Console.WriteLine(responsePost.StatusCode);
            Console.WriteLine(responsePost.Content);


        }
    }
}
