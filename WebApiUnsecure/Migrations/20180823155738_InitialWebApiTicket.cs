using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiUnsecure.Migrations
{
    public partial class InitialWebApiTicket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Concert = table.Column<string>(nullable: true),
                    Artist = table.Column<string>(nullable: true),
                    Available = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "Artist", "Available", "Concert" },
                values: new object[] { 1L, "Zaz", true, "MadridPalau" });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "Artist", "Available", "Concert" },
                values: new object[] { 2L, "Ciagala", true, "La carboneria" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tickets");
        }
    }
}
