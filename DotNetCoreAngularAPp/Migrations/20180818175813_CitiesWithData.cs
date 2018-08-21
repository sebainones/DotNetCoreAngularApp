using Microsoft.EntityFrameworkCore.Migrations;

namespace DotNetCoreAngularApp.Migrations
{
    public partial class CitiesWithData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Cities",
                newName: "CityId");

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "CityId", "Name", "ZipCode" },
                values: new object[] { 1, "MDZ", 5500 });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "CityId", "Name", "ZipCode" },
                values: new object[] { 2, "BCN", 8001 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "CityId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "CityId",
                keyValue: 2);

            migrationBuilder.RenameColumn(
                name: "CityId",
                table: "Cities",
                newName: "Id");
        }
    }
}
