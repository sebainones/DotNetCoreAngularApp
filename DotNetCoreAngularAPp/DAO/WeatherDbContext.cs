using DotNetCoreAngularAPp.Model;
using Microsoft.EntityFrameworkCore;

namespace DotNetCoreAngularAPp
{
    public  class WeatherDbContext :DbContext
    {
        public WeatherDbContext(DbContextOptions options): base(options)
        {

        }

        public DbSet<WeatherForecast> Weathers { get; set; }
    }
}