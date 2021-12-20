using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherApi.Infrastructure.DTO
{
    public class WeatherForecastDto
    {
        public string City { get; set; }
        public Temperature DailyTemperature { get; set; }
        public Temperature WeeklyTemperature { get; set; }
    }

    public class Temperature
    {
        public float Lowest { get; set; }
        public float Highest { get; set; }
    }
}
