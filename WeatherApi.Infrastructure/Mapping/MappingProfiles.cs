using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using WeatherApi.Infrastructure.DAL.Models;
using WeatherApi.Infrastructure.DTO;

namespace WeatherApi.Infrastructure.Mapping
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<WeatherForecastDto, WeatherForecast>()
                .ForMember(x => x.RequestedCity, x => x.MapFrom(y => y.City))
                .ForPath(x => x.DailyLowestTemp, x => x.MapFrom(y => y.DailyTemperature.Lowest))
                .ForPath(x => x.DailyHighestTemp, x => x.MapFrom(y => y.DailyTemperature.Highest))
                .ForPath(x => x.WeeklyHighestTemp, x => x.MapFrom(y => y.WeeklyTemperature.Highest))
                .ForPath(x => x.WeeklyLowestTemp, x => x.MapFrom(y => y.WeeklyTemperature.Lowest));
        }
    }
}
