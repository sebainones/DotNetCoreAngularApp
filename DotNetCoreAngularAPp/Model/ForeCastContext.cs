using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace DotNetCoreAngularApp.Model
{
    public class ForeCastContext : DbContext
    {
        private string connectionString;
        private string dbPassword;

        public DbSet<WeatherForecast> WeatherForecast { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<ForeCastUser> Users { get; set; }

        //public DbSet<IdentityUserClaim<Guid>> IdentityUserClaims { get; set; }

        //https://github.com/aspnet/Identity/issues/1802
        public DbSet<IdentityUserClaim<string>> IdentityUserClaim { get; set; }


        //D.I
        public IConfiguration Configuration { get; }

        public ForeCastContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {

            Configuration = configuration;

            dbPassword = Configuration.GetValue<string>("dbPwd");

            connectionString = $"Server=tcp:sebasserver.database.windows.net,1433;Initial Catalog=SebaDataBase;Persist Security Info=False;User ID=sebainones;Password={dbPassword};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Remember to have the real username and pwd!           
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SeedData(modelBuilder);

            //modelBuilder.Entity<ForeCastUser>().HasKey(p => new { p.Id });
            //modelBuilder.Entity<IdentityUser<Guid>>().HasKey(p => new { p.Id});

            modelBuilder.Entity<IdentityUserClaim<string>>().HasKey(p => new { p.Id });
            //modelBuilder.Ignore<IdentityUserClaim<string>>();
        }

        private static void SeedData(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<WeatherForecast>()
            // .SeedData(            //     new WeatherForecast {}            // );

            modelBuilder.Entity<WeatherForecast>().HasData(
                                                                new WeatherForecast
                                                                {
                                                                    Name = "MDZ",
                                                                    TemperatureC = 25,
                                                                    Summary = "Templado"
                                                                },
                                                                new WeatherForecast
                                                                {
                                                                    Name = "BCN",
                                                                    TemperatureC = 15,
                                                                    Summary = "Fresco"
                                                                }
                                                            );

            modelBuilder.Entity<City>().
                HasData(
                        new City() { CityId = 1, Name = "MDZ", ZipCode = 5500 },
                        new City() { CityId = 2, Name = "BCN", ZipCode = 08001 }
                );
        }
    }
}