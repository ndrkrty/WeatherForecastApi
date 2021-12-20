using System;
using System.Collections.Generic;
using System.Text;
using WeatherApi.Infrastructure.DAL.Models;

namespace WeatherApi.Infrastructure.DAL.Repositories.Interfaces
{
    public interface IWeatherForecastRepository : IRepository<WeatherForecast>
    {
        WeatherForecast GetWeatherForecast(string cityName);
        List<WeatherForecast> GetWeatherForecastRequests();
    }
}
