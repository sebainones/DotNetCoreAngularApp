using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DotNetCoreAngularApp.Migrations
{
    public partial class IdentityChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_IdentityUser<Guid>",
                table: "IdentityUser<Guid>");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "IdentityUser<Guid>");

            migrationBuilder.RenameTable(
                name: "IdentityUser<Guid>",
                newName: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Users",
                nullable: false,
                oldClrType: typeof(Guid));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "IdentityUser<Guid>");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "IdentityUser<Guid>",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "IdentityUser<Guid>",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IdentityUser<Guid>",
                table: "IdentityUser<Guid>",
                column: "Id");
        }
    }
}
