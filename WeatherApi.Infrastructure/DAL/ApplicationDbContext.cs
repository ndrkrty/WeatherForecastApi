using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WeatherApi.Infrastructure.DAL.Configuration;
using WeatherApi.Infrastructure.DAL.Models;

namespace WeatherApi.Infrastructure.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<WeatherForecast> WeatherForecast { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new WeatherForecastConfiguration());
        }
    }
}
