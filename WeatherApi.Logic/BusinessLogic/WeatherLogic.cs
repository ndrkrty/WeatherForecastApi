using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeatherApi.Infrastructure.DAL;
using WeatherApi.Infrastructure.DAL.Models;
using WeatherApi.Infrastructure.DAL.Repositories.Interfaces;
using WeatherApi.Infrastructure.DTO;
using WeatherApi.Infrastructure.ServiceInterfaces;
using WeatherApi.Integrations.DarkSKY;
using WeatherApi.Integrations.LocationIQ;
using WeatherApi.Logic.BusinessLogicInterfaces;

namespace WeatherApi.Logic.BusinessLogic
{
    public class WeatherLogic : BaseLogic, IWeatherLogic
    {
        private readonly ILocationIQOperations _locationIQ;
        private readonly IDarkSKYOperations _darkSky;

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        private readonly IWeatherForecastRepository _weatherForecastRepository;

        private readonly IDistributedCache _distributedCache;

        public WeatherLogic(ILocationIQOperations locationIQ, IDarkSKYOperations darkSky,
            IUnitOfWork unitOfWork, IMapper mapper, IWeatherForecastRepository weatherForecastRepository, 
            IDistributedCache distributedCache)
        {
            _locationIQ = locationIQ;
            _darkSky = darkSky;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _weatherForecastRepository = weatherForecastRepository;
            _distributedCache = distributedCache;
        }

        public  ServiceResult<WeatherForecastDto> GetWeatherInfoByCityName(string cityName)
        {
            try
            {
                var cacheKey = cityName + DateTime.Now.Date.ToString("yyyyMMdd");
                var checkCache = _distributedCache.GetAsync(cacheKey).GetAwaiter().GetResult();

                if (checkCache != null)
                {
                    var serializedResult = Encoding.UTF8.GetString(checkCache);
                    var cacheResult = JsonConvert.DeserializeObject<WeatherForecastDto>(serializedResult);

                    return new ServiceResult<WeatherForecastDto>(cacheResult, ServiceResultStatusEnum.Success);
                }
                else
                {
                    var checkDb = _weatherForecastRepository.GetWeatherForecast(cityName);

                    if (checkDb != null)
                    {
                        var serviceResult = new WeatherForecastDto()
                        {
                            City = cityName,
                            DailyTemperature = new Temperature()
                            {
                                Highest = checkDb.DailyHighestTemp,
                                Lowest = checkDb.DailyLowestTemp
                            },
                            WeeklyTemperature = new Temperature()
                            {
                                Highest = checkDb.WeeklyHighestTemp,
                                Lowest = checkDb.WeeklyLowestTemp
                            }
                        };

                        return new ServiceResult<WeatherForecastDto>(serviceResult, ServiceResultStatusEnum.Success);
                    }
                    else
                    {
                        var coordinates = _locationIQ.GetLocationCoordinatesByCityName(cityName);

                        if (coordinates != null)
                        {
                            var firstResult = coordinates.FirstOrDefault();

                            var weatherDetails = _darkSky.GetWeatherDetailsByCoordinates(firstResult.lat, firstResult.lon);

                            if (weatherDetails != null)
                            {
                                var weeklyHighest = new List<float>();
                                var weeklyLowest = new List<float>();

                                var currentDay = new List<float>();

                                foreach (var item in weatherDetails.Daily.Data)
                                {
                                    weeklyHighest.Add(item.TemperatureHigh);
                                    weeklyLowest.Add(item.TemperatureLow);
                                }

                                foreach (var item in weatherDetails.Hourly.Data)
                                {
                                    currentDay.Add(item.Temperature);
                                }

                                var serviceResult = new WeatherForecastDto()
                                {
                                    City = cityName,
                                    DailyTemperature = new Temperature()
                                    {
                                        Highest = currentDay.Max(),
                                        Lowest = currentDay.Min()
                                    },
                                    WeeklyTemperature = new Temperature()
                                    {
                                        Highest = weeklyHighest.Max(),
                                        Lowest = weeklyLowest.Min()
                                    }
                                };
                                
                                var serializedResult = JsonConvert.SerializeObject(serviceResult);
                                var encodedResult = Encoding.UTF8.GetBytes(serializedResult);
                                var options = new DistributedCacheEntryOptions()
                                                .SetAbsoluteExpiration(DateTime.Now.AddHours(1));
                                _distributedCache.SetAsync(cacheKey, encodedResult, options);

                                var mappedResult = _mapper.Map<WeatherForecastDto, WeatherForecast>(serviceResult);
                                mappedResult.CreatedOn = DateTime.Now;

                                _unitOfWork.WeatherForecastRepository.Add(mappedResult);
                                _unitOfWork.SaveChanges();

                                return new ServiceResult<WeatherForecastDto>(serviceResult, ServiceResultStatusEnum.Success);
                            }
                        }
                    }
                }

                return new ServiceResult<WeatherForecastDto>(null, ServiceResultStatusEnum.Fail, "No location can be found.");
            }
            catch (Exception ex)
            {
                return new ServiceResult<WeatherForecastDto>(null, ServiceResultStatusEnum.Error, "An unexpected error occured during service operations.");
            }
        }
    }
}
