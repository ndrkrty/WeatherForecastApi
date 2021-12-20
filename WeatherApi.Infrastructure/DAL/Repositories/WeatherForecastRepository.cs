using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeatherApi.Infrastructure.DAL.Models;
using WeatherApi.Infrastructure.DAL.Repositories.Interfaces;

namespace WeatherApi.Infrastructure.DAL.Repositories
{
    public class WeatherForecastRepository : Repository<WeatherForecast>, IWeatherForecastRepository
    {
        public WeatherForecastRepository(ApplicationDbContext context) : base(context)
        {
        }

        public WeatherForecast GetWeatherForecast(string cityName)
        {
            return _context.WeatherForecast
                .Where(m => m.RequestedCity == cityName 
                            && m.CreatedOn.Date == DateTime.Now.Date)
                .OrderByDescending(m => m.Id)
                .FirstOrDefault();
        }

        public List<WeatherForecast> GetWeatherForecastRequests()
        {
            return _context.WeatherForecast
                .Where(m => m.CreatedOn.Date == DateTime.Now.Date)
                .OrderByDescending(m => m.Id).ToList();
        }
    }
}
