using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreAngularApp.Model
{
    public class WeatherDbContext : DbContext
    {
        public WeatherDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<WeatherForecast> Weathers { get; set; }
    }
}

