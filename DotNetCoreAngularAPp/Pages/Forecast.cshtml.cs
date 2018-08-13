using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreAngularAPp;
using DotNetCoreAngularAPp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DotNetCoreAngularApp.Pages
{
    public class ForecastModel : PageModel
    {
        private readonly WeatherDbContext weatherDbContext;
        private static bool isContextInitialized = false;

        public IList<WeatherForecast> Forecasts { get; private set; }

        public ForecastModel(WeatherDbContext dbContext)
        {
            this.weatherDbContext = dbContext;

            if (!isContextInitialized)

                InitializeForecast();
        }

        private void InitializeForecast()
        {
            isContextInitialized = true;
            this.weatherDbContext.Weathers.Add(new WeatherForecast() { Name = "Alicante", Summary = "Soleado", TemperatureC = 31 });
            this.weatherDbContext.Weathers.Add(new WeatherForecast() { Name = "Zaragoza", Summary = "Nublado", TemperatureC = 25 });

            this.weatherDbContext.SaveChangesAsync();


        }

        public async Task OnGetAsync()
        {
            //NO Tracking is good for readonly purposes
            Forecasts = await weatherDbContext.Weathers.AsNoTracking().ToListAsync();
        }

        //https://www.mikesdotnetting.com/Article/308/razor-pages-understanding-handler-methods
        public async Task<IActionResult> OnPostDeleteAsync(string name)
        {

            var currentForecast = await this.weatherDbContext.Weathers.FindAsync(name);
            if (currentForecast != null)
            {
                this.weatherDbContext.Weathers.Remove(currentForecast);
                await this.weatherDbContext.SaveChangesAsync();
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostAsync(string name)
        {

            var currentForecast = await this.weatherDbContext.Weathers.FindAsync(name);
            if (currentForecast != null)
            {
                this.weatherDbContext.Weathers.Remove(currentForecast);
                await this.weatherDbContext.SaveChangesAsync();
            }
            return RedirectToPage();
        }

        //public void OnPostDelete(string name)
        //{

        //}


    }
}