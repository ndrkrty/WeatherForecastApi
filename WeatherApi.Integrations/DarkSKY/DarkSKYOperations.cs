using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using WeatherApi.Helpers;
using WeatherApi.Helpers.HttpRequestHelper;
using WeatherApi.Infrastructure.DTO;

namespace WeatherApi.Integrations.DarkSKY
{
    public class DarkSKYOperations : IDarkSKYOperations
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<DarkSKYOperations> _logger;

        public DarkSKYOperations(ILogger<DarkSKYOperations> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _clientFactory = clientFactory;
        }

        private HttpClient GetHttpClient()
        {
            return _clientFactory.CreateClient("darksky");
        }

        public DarkSkyServiceResultDto GetWeatherDetailsByCoordinates(double lat, double lon)
        {
            try
            {
                var result = string.Empty;
                var path = string.Format("<insertkeyhere>/{0},{1}?units=auto", ConvertHelper.ChangeDelimeter(lat), ConvertHelper.ChangeDelimeter(lon));

                var client = GetHttpClient();
                var callTask = HttpClientHelper.GetAsync(client, path);
                callTask.Wait();
                result = callTask.Result;

                var serviceResult = JsonConvert.DeserializeObject<DarkSkyServiceResultDto>(result, new JsonSerializerSettings
                {
                    Culture = new System.Globalization.CultureInfo("en-US")
                });

                return serviceResult;
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occured during darksky service request! ExMessage: " + ex.Message + " innerEx: " + ex.InnerException);
            }

            return null;
        }
    }

    public interface IDarkSKYOperations
    {
        DarkSkyServiceResultDto GetWeatherDetailsByCoordinates(double lat, double lon);
    }
}
