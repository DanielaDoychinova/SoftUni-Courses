using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System.Text.Json;

namespace SerializeAndDesrialize

{
    internal class Program
    {

        //built-in system.text.json nugget package
        static void Main(string[] args)
        {
            WeatherForecast forecast = new WeatherForecast();

            string weatherInfo = System.Text.Json.JsonSerializer.Serialize(forecast);

            Console.WriteLine(weatherInfo);

            string jsonString = File.ReadAllText("C:\\Users\\Daniela\\Documents\\Back-End Test Automation\\Back-End Test Automation\\1. API testing with C#\\APITesting\\file.json");

            WeatherForecast forecastFromJson = System.Text.Json.JsonSerializer.Deserialize<WeatherForecast>(jsonString);

            //newtonsoft json package
            WeatherForecast forecastNS = new WeatherForecast();

            string weatherForecastNs = JsonConvert.SerializeObject(forecastNS, Formatting.Indented);

            Console.WriteLine(weatherForecastNs);

            jsonString = File.ReadAllText("C:\\Users\\Daniela\\Documents\\Back-End Test Automation\\Back-End Test Automation\\1. API testing with C#\\APITesting\\file.json");

            WeatherForecast weatherInfoNS = JsonConvert.DeserializeObject<WeatherForecast>(jsonString);

            //working with annonymous objects
            var json = @"{ 'firstName':'Name',
                'lastName': 'Name',
                'jobTitle': 'Job'}";

            var template = new
            {
                FirstName = string.Empty,
                LastName = string.Empty,
                JobTitle = string.Empty,
            };

            var person = JsonConvert.DeserializeAnonymousType(json, template);


            //applying naming convention to the class properties
            WeatherForecast weatherForecastResolver = new WeatherForecast();
            DefaultContractResolver contractResolver = new DefaultContractResolver()
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            };

            string snakeCaseJson = JsonConvert.SerializeObject(weatherForecastResolver, new JsonSerializerSettings()
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented,
            });
            Console.WriteLine(snakeCaseJson);

            //Jobject
            var jsonAsString = JObject.Parse(@"{'products': [
{'name': 'Fruits', 'products': ['apple', 'banana']},
{'name': 'Vegetables', 'products': ['cucumber']}]}");
            var products = jsonAsString["products"].Select(t =>
            string.Format("{0} ({1})",
            t["name"],
            string.Join(", ", t["products"])
            ));

        }


    }
}
    

