using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherApi.Infrastructure.DAL;
using WeatherApi.Infrastructure.DAL.Models;
using WeatherApi.Infrastructure.DAL.Repositories;
using WeatherApi.Infrastructure.DAL.Repositories.Interfaces;
using WeatherApi.Infrastructure.DTO;

namespace MonitoringClient
{
    class Program
    {
        public static ServiceProvider ServiceProvider;

        static void Main(string[] args)
        {
            Console.WriteLine("Service started");

            InitializeIoc();
            var requestResults = GetCurrentRequest();

            foreach (var result in requestResults)
            {
                var info = new WeatherForecastDto()
                {
                    City = result.RequestedCity,
                    DailyTemperature = new Temperature()
                    {
                        Highest = result.DailyHighestTemp,
                        Lowest = result.DailyLowestTemp
                    },
                    WeeklyTemperature = new Temperature()
                    {
                        Highest = result.WeeklyHighestTemp,
                        Lowest = result.WeeklyLowestTemp
                    }
                };

                var serializedResult = JsonConvert.SerializeObject(info);
                Console.WriteLine("PreviousRequests:");
                Console.WriteLine(serializedResult);
            }

            HubConnection connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:64368/serverhub")
                .WithAutomaticReconnect()
                .Build();

            connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync();
            };

            connection.StartAsync();
           
            connection.On<string, string>("ReceiveMessage",
                            (string elapsedTime, string message) =>
                            {
                                Console.WriteLine("New Service Request: ");
                                Console.WriteLine($"Total Time Elapsed: {elapsedTime} - Service Message : {message}");
                            });

            Console.ReadLine();
        }

        private static void InitializeIoc()
        {
            ServiceProvider = new ServiceCollection()
                .AddDbContext<ApplicationDbContext>(options => options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB; database=WeatherForecast; Trusted_Connection=true; MultipleActiveResultSets=true", x => x.MigrationsAssembly("WeatherApi.Infrastructure")))
                .AddTransient<IUnitOfWork, UnitOfWork>()
                .AddTransient<IWeatherForecastRepository, WeatherForecastRepository>()
                .BuildServiceProvider();
        }

        private static List<WeatherForecast> GetCurrentRequest()
        {
            using (var scope = ServiceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetService<IWeatherForecastRepository>();
                return service.GetWeatherForecastRequests();
            }
        }
    }
}
