using Microsoft.EntityFrameworkCore.Migrations;

namespace DotNetCoreAngularApp.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WeatherForecast",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: false),
                    DateFormatted = table.Column<string>(nullable: true),
                    TemperatureC = table.Column<int>(nullable: false),
                    Summary = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherForecast", x => x.Name);
                });

            migrationBuilder.InsertData(
                table: "WeatherForecast",
                columns: new[] { "Name", "DateFormatted", "Summary", "TemperatureC" },
                values: new object[] { "Mdz", null, "Templado", 25 });

            migrationBuilder.InsertData(
                table: "WeatherForecast",
                columns: new[] { "Name", "DateFormatted", "Summary", "TemperatureC" },
                values: new object[] { "Bcn", null, "Frescoscr", 15 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeatherForecast");
        }
    }
}
