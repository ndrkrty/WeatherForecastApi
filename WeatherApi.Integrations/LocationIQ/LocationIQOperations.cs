using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using WeatherApi.Helpers.HttpRequestHelper;
using WeatherApi.Infrastructure.DTO;

namespace WeatherApi.Integrations.LocationIQ
{
    public class LocationIQOperations : ILocationIQOperations
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<LocationIQOperations> _logger;

        public LocationIQOperations(ILogger<LocationIQOperations> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _clientFactory = clientFactory;
        }

        private HttpClient GetHttpClient()
        {
            return _clientFactory.CreateClient("locationiq");
        }

        public List<LocationIQServiceResultDto> GetLocationCoordinatesByCityName(string cityName)
        {
            _logger.LogInformation("Request made");

            try
            {
                var result = string.Empty;
                var path = string.Format("?key=<insertkeyhere>&q={0}&format=json", cityName);

                var client = GetHttpClient();
                var callTask = HttpClientHelper.GetAsync(client, path);
                callTask.Wait();
                result = callTask.Result;

                var serviceResult = JsonConvert.DeserializeObject<List<LocationIQServiceResultDto>>(result);

                return serviceResult;
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occured during locationiq service request! ExMessage: " + ex.Message + " innerEx: " + ex.InnerException);
            }

            return null;
        }
    }

    public interface ILocationIQOperations
    {
        List<LocationIQServiceResultDto> GetLocationCoordinatesByCityName(string cityName);
    }
}
