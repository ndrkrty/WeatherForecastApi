using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WeatherApi.Infrastructure.DAL.Models;

namespace WeatherApi.Infrastructure.DAL.Configuration
{
    public class WeatherForecastConfiguration : IEntityTypeConfiguration<WeatherForecast>
    {
        public void Configure(EntityTypeBuilder<WeatherForecast> builder)
        {
            builder
                   .HasKey(m => m.Id);

            builder
                   .Property(m => m.Id).ValueGeneratedOnAdd();
        }
    }
}
