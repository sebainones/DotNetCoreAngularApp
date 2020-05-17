using Microsoft.EntityFrameworkCore;

namespace DotNetCoreAngularApp.Model
{
    /// <summary>
    /// This class will be used to hold inmemory data!
    /// </summary>
    public class WeatherDbContext : DbContext
    {
        public WeatherDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<WeatherForecast> Weathers { get; set; }
    }
}
