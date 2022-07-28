using Microsoft.AspNetCore.Mvc;

namespace JSONProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost(Name = "ConvertJsonToText")]
        public IActionResult ConvertJsonToText(User userModel)
        {
            var dir = Directory.GetCurrentDirectory();
            var data = $"FirstName = {userModel?.FirstName}\nLastName = {userModel?.LastName}\nAddress = Streetaddress : {userModel?.Address.Streetaddress}, City : {userModel?.Address.City}, State: {userModel?.Address.State}, Postal Code: {userModel?.Address.PostalCode}\n";

            foreach (var phone in userModel.Phonenumber)
            {
                data += $"Phone = Type:  {phone.Type}, PhoneNumber : {phone.Number}";
            }

            System.IO.File.WriteAllText(dir + @"\Result\JsonToText.txt", data);

            //Console.WriteLine("Written to the Text File\n------------------------\n" + data + "\n-----------------------------------");

            var filename = dir + @"\Result\JsonToText.txt";

            var lines = System.IO.File.ReadAllLines(filename);

            var model = new User();
            model.FirstName = lines[0].Split("=")[1];
            model.LastName = lines[1].Split("=")[1];

            var address = lines[2].Split("=")[1];
            var phoneNo = lines[3].Split("=")[1];
            model.Address = new Address();

            model.Address.Streetaddress = address.Split(",")[0].Split(":")[1].Trim();
            model.Address.City = address.Split(",")[1].Split(":")[1].Trim();
            model.Address.State = address.Split(",")[2].Split(":")[1].Trim();
            model.Address.PostalCode = address.Split(",")[3].Split(":")[1].Trim();

            model.Phonenumber = new List<Phonenumber>();
            var phoneNumber = new Phonenumber();
            phoneNumber.Type = phoneNo.Split(",")[0].Split(":")[1].Trim();
            phoneNumber.Number = phoneNo.Split(",")[1].Split(":")[1].Trim();
            model.Phonenumber.Add(phoneNumber);
            var json = System.Text.Json.JsonSerializer.Serialize(model);

            Console.WriteLine("JSON Data\n");
            Console.WriteLine(json);

            System.IO.File.WriteAllText(dir+@"\Result\TextToJson.txt", json);
            return Ok();
        }
    }
}