using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WeatherApi.Hubs;
using WeatherApi.Infrastructure.DAL;
using WeatherApi.Infrastructure.DAL.Repositories;
using WeatherApi.Infrastructure.DAL.Repositories.Interfaces;
using WeatherApi.Infrastructure.Mapping;
using WeatherApi.Integrations.DarkSKY;
using WeatherApi.Integrations.LocationIQ;
using WeatherApi.Logic.BusinessLogic;
using WeatherApi.Logic.BusinessLogicInterfaces;

namespace WeatherApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private MapperConfiguration _mapperConfiguration { get; set; }
        public IMapper Mapper { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(options =>
               {
                   options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                   options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
               });

            #region httpclient factory
            services.AddHttpClient("locationiq", client =>
            {
                //client.BaseAddress = new Uri($"https://eu1.locationiq.com/v1/search.php");
                client.BaseAddress = new Uri(Configuration.GetSection("LocationIqServiceAdress").Value);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
            });

            services.AddHttpClient("darksky", client =>
            {
                //client.BaseAddress = new Uri($"https://api.darksky.net/forecast/");
                client.BaseAddress = new Uri(Configuration.GetSection("DarkSkyServiceAdress").Value);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
            })
            .ConfigurePrimaryHttpMessageHandler(messageHandler =>
            {
                var handler = new HttpClientHandler();

                if (handler.SupportsAutomaticDecompression)
                {
                    handler.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip | DecompressionMethods.Brotli;
                }
                return handler;
            });
            #endregion

            //added to use in-memory cache
            //services.AddMemoryCache();

            //added to use Redis cache
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = "localhost:6379";
            });

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default"), x => x.MigrationsAssembly("WeatherApi.Infrastructure")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IWeatherLogic, WeatherLogic>();
            services.AddTransient<ILocationIQOperations, LocationIQOperations>();
            services.AddTransient<IDarkSKYOperations, DarkSKYOperations>();
            services.AddTransient<IWeatherForecastRepository, WeatherForecastRepository>();

            services.AddResponseCompression();

            services.AddSingleton(sp => _mapperConfiguration.CreateMapper());

            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapHub<ServerHub>("/serverhub");
            });

            _mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfiles());
            });
        }
    }
}
