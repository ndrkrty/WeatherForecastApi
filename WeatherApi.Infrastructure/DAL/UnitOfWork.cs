using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WeatherApi.Infrastructure.DAL.Repositories;

namespace WeatherApi.Infrastructure.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private WeatherForecastRepository _weatherForecastRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public WeatherForecastRepository WeatherForecastRepository
        {
            get
            {
                if (this._weatherForecastRepository == null)
                    this._weatherForecastRepository = new WeatherForecastRepository(_context);

                return _weatherForecastRepository;
            }
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
