﻿// <auto-generated />
using DotNetCoreAngularApp.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DotNetCoreAngularApp.Migrations
{
    [DbContext(typeof(ForeCastContext))]
    [Migration("20180818175813_CitiesWithData")]
    partial class CitiesWithData
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DotNetCoreAngularApp.Model.City", b =>
                {
                    b.Property<int>("CityId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<int>("ZipCode");

                    b.HasKey("CityId");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("Cities");

                    b.HasData(
                        new { CityId = 1, Name = "MDZ", ZipCode = 5500 },
                        new { CityId = 2, Name = "BCN", ZipCode = 8001 }
                    );
                });

            modelBuilder.Entity("DotNetCoreAngularApp.Model.WeatherForecast", b =>
                {
                    b.Property<string>("Name")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DateFormatted");

                    b.Property<string>("Summary");

                    b.Property<int>("TemperatureC");

                    b.HasKey("Name");

                    b.ToTable("WeatherForecast");

                    b.HasData(
                        new { Name = "MDZ", Summary = "Templado", TemperatureC = 25 },
                        new { Name = "BCN", Summary = "Fresco", TemperatureC = 15 }
                    );
                });

            modelBuilder.Entity("DotNetCoreAngularApp.Model.City", b =>
                {
                    b.HasOne("DotNetCoreAngularApp.Model.WeatherForecast")
                        .WithOne("City")
                        .HasForeignKey("DotNetCoreAngularApp.Model.City", "Name");
                });
#pragma warning restore 612, 618
        }
    }
}
