using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreAngularApp.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DotNetCoreAngularApp.Pages
{
    [Authorize]
    public class ForecastModel : PageModel
    {
        private readonly ILogger<ForecastModel> log;

        //private readonly WeatherDbContext weatherDbContext;
        private readonly ForeCastContext weatherDbContext;

        private static bool isContextInitialized = false;

        [BindProperty]
        //public LocalView<WeatherForecast> Forecasts { get; private set; }
        public IList<WeatherForecast> Forecasts { get; private set; }

        [TempData]
        public string Message { get; set; }

        //public ForecastModel(WeatherDbContext dbContext, ILogger<ForecastModel> log)
        public ForecastModel(ForeCastContext dbContext, ILogger<ForecastModel> log)
        {
            this.weatherDbContext = dbContext;
            this.log = log;

            if (!isContextInitialized)
                InitializeForecast();
        }

        private void InitializeForecast()
        {
            isContextInitialized = true;

            //In memory
            //this.weatherDbContext.Weathers.Add(new WeatherForecast() { Name = "Alicante", Summary = "Soleado", TemperatureC = 31 });
            //this.weatherDbContext.Weathers.Add(new WeatherForecast() { Name = "Zaragoza", Summary = "Nublado", TemperatureC = 25 });
            //this.weatherDbContext.SaveChangesAsync();
        }


        public async Task OnGetAsync()
        {
            //NO Tracking is good for readonly purposes
            //Forecasts = await weatherDbContext.Weathers.AsNoTracking().ToListAsync();
            try
            {
                //Real DB
                await this.weatherDbContext.WeatherForecast.Select(w => w).LoadAsync();
                //TODO: Find out how to work with LocalView
                Forecasts = this.weatherDbContext.WeatherForecast.Local.ToList();

            }
            catch (Exception ex)
            {
                log.LogError(ex.Message);
            }

        }

        //https://www.mikesdotnetting.com/Article/308/razor-pages-understanding-handler-methods
        public async Task<IActionResult> OnPostDeleteAsync(string name)
        {
            var currentForecast = await weatherDbContext.WeatherForecast.FindAsync(name);            
            if (currentForecast != null)
            {
                weatherDbContext.WeatherForecast.Remove(currentForecast);

                await this.weatherDbContext.SaveChangesAsync();
                //TODO: figure out this!
                var msg = $"The forecast for {currentForecast.Name} has been deleted";
                Message = msg;
                log.LogWarning(msg);
            }
            return RedirectToPage();
        }
    }
}