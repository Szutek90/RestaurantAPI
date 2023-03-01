/*
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace RestaurantAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherForecastService _weatherForecastService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastService weatherForecastService)
        {
            _logger = logger;
            _weatherForecastService = weatherForecastService;
        }

        /*[HttpGet]
        public ActionResult<IEnumerable<WeatherForecast>> Get()
        {
            return StatusCode(401, _weatherForecastService.Get());
        }
        

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return _weatherForecastService.Get();
        }
        /*
         * to jest coś źle
        [HttpGet("currentDay/{max}")]
        public IEnumerable<WeatherForecast> Get2([FromQuery] int count,[FromRoute] int min, [FromRoute] int max)
        {
            return _weatherForecastService.Get2(count, min, max);
        }
        
        [HttpPost]
        public ActionResult<string> Hello([FromBody] string name)
        {

            //return StatusCode(401,$"Hello {name}");
            return NotFound($"Hello {name}");
        }

        [HttpPost("generate")]
        public ActionResult<IEnumerable<WeatherForecast>> Get2([FromQuery] int count,
            [FromBody] TemperatureRequest request)
        {
            if (count < 0 || request.Min > request.Max)
                return BadRequest();
            else
                return Ok(_weatherForecastService.Get2(count, request.Min, request.Max));
        }

    }
}
*/
