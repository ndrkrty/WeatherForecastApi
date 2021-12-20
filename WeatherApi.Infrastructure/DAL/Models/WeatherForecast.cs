using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherApi.Infrastructure.DAL.Models
{
    public class WeatherForecast
    {
        public int Id { get; set; }
        public string RequestedCity { get; set; }
        public float DailyHighestTemp { get; set; }
        public float DailyLowestTemp { get; set; }
        public float WeeklyHighestTemp { get; set; }
        public float WeeklyLowestTemp { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
