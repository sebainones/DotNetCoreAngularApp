using System.ComponentModel.DataAnnotations;

namespace DotNetCoreAngularApp.Model
{    
    public class WeatherForecast
    {
        [Required]
        [Key]
        public string Name { get; set; }

        public City City { get; set; }

        public string DateFormatted { get; set; }

        public int TemperatureC { get; set; }

        public string Summary { get; set; }

        public int TemperatureF
        {
            get
            {
                return 32 + (int)(TemperatureC / 0.5556);
            }
        }
    }
}