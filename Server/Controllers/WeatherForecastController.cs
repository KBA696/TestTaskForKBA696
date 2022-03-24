using Client.Model;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        List<Message> Messages = new List<Message>();

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;

            try
            {
                Messages = new List<Message>().FileToClass("./DataRecovery", SerializerFormat.JSON);
            }
            catch { }

            Messages.Add(new Message() { Nickname="1", Text = "222", Date=DateTime.Now });
        }

        [HttpGet]
        public IEnumerable<Message> Get()
        {
            return Messages.ToArray();
        }


        [HttpPost]
        public async Task<ActionResult<Message>> Post(Message user)
        {
            try
            {
                Messages.Add(user);
                Messages.ClassToFile("./DataRecovery", SerializerFormat.JSON);

                return Ok(user);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}