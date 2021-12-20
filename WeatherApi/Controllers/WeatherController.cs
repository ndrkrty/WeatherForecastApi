using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Diagnostics;
using WeatherApi.Hubs;
using WeatherApi.Infrastructure.DTO;
using WeatherApi.Infrastructure.ServiceInterfaces;
using WeatherApi.Logic.BusinessLogicInterfaces;


namespace WeatherApi.Controllers
{
    [Route("api/[controller]/[action]")]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherLogic _weatherLogic;
        private readonly IHubContext<ServerHub> _hub;

        private readonly ILogger<WeatherController> _logger;

        public WeatherController(IWeatherLogic weatherLogic, IHubContext<ServerHub> hub, ILogger<WeatherController> logger)
        {
            _weatherLogic = weatherLogic;
            _hub = hub;
            _logger = logger;
        }


        [HttpGet]
        [ActionName("GetWeatherInfo")]
        public ServiceResult<WeatherForecastDto> GetWeatherInfo(string cityName)
        {
            _logger.LogInformation("Requested to GetWeatherInfo service with city name: " + cityName);

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var res = _weatherLogic.GetWeatherInfoByCityName(cityName);
            stopwatch.Stop();

            var timeElapsed = stopwatch.ElapsedMilliseconds.ToString();

            var serializedResult = JsonConvert.SerializeObject(res);
            _hub.Clients.All.SendAsync("ReceiveMessage", timeElapsed, serializedResult).GetAwaiter().GetResult();

            return res;
        }
    }
}
