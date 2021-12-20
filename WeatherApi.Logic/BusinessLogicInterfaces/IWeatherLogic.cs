using System;
using System.Collections.Generic;
using System.Text;
using WeatherApi.Infrastructure.DTO;
using WeatherApi.Infrastructure.ServiceInterfaces;

namespace WeatherApi.Logic.BusinessLogicInterfaces
{
    public interface IWeatherLogic : IService
    {
        ServiceResult<WeatherForecastDto> GetWeatherInfoByCityName(string cityName);
    }
}
