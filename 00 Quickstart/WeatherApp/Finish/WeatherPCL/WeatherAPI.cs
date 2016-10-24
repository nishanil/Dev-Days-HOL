using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WeatherPCL
{
    public class WeatherAPI
    {
        public async Task<string> GetWeather(double latitude, double longitude)
        {
            string serviceUrl =
                $"http://api.openweathermap.org/data/2.5/weather?lat={latitude}&lon={longitude}&appid=aa0c788a0a6debea2c5da1d89d7f79ea&mode=xml&units=metric";

            var client = new HttpClient();
            var response = await client.GetStringAsync(serviceUrl);

            var data = XElement.Parse(response);
            string temperature = data.Elements("temperature").FirstOrDefault()?
                .Attribute("value")?.Value;

            string city = data.Elements("city").FirstOrDefault()?
                .Attribute("name")?.Value;

            return $"City {city}, {temperature}°C";
        }

    }
}
