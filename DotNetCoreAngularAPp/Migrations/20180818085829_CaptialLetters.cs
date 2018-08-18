using Microsoft.EntityFrameworkCore.Migrations;

namespace DotNetCoreAngularApp.Migrations
{
    public partial class CaptialLetters : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "WeatherForecast",
                keyColumn: "Name",
                keyValue: "Bcn");

            migrationBuilder.DeleteData(
                table: "WeatherForecast",
                keyColumn: "Name",
                keyValue: "Mdz");

            migrationBuilder.InsertData(
                table: "WeatherForecast",
                columns: new[] { "Name", "DateFormatted", "Summary", "TemperatureC" },
                values: new object[] { "MDZ", null, "Templado", 25 });

            migrationBuilder.InsertData(
                table: "WeatherForecast",
                columns: new[] { "Name", "DateFormatted", "Summary", "TemperatureC" },
                values: new object[] { "BCN", null, "Fresco", 15 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "WeatherForecast",
                keyColumn: "Name",
                keyValue: "BCN");

            migrationBuilder.DeleteData(
                table: "WeatherForecast",
                keyColumn: "Name",
                keyValue: "MDZ");

            migrationBuilder.InsertData(
                table: "WeatherForecast",
                columns: new[] { "Name", "DateFormatted", "Summary", "TemperatureC" },
                values: new object[] { "Mdz", null, "Templado", 25 });

            migrationBuilder.InsertData(
                table: "WeatherForecast",
                columns: new[] { "Name", "DateFormatted", "Summary", "TemperatureC" },
                values: new object[] { "Bcn", null, "Frescoscr", 15 });
        }
    }
}
