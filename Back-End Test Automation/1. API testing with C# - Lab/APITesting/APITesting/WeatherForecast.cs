

using Newtonsoft.Json;


namespace SerializeAndDesrialize
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; } = DateTime.Now;

        //[JsonProperty("temperature_c")]

        public int TemperatureC { get; set; } = 30;

        //[JsonIgnore]

        public string Summary { get; set; } = "Hot summer days";

    }
}
