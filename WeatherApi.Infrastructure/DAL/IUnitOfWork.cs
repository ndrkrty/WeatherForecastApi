using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WeatherApi.Infrastructure.DAL.Repositories;

namespace WeatherApi.Infrastructure.DAL
{
    public interface IUnitOfWork
    {
        WeatherForecastRepository WeatherForecastRepository { get; }
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
