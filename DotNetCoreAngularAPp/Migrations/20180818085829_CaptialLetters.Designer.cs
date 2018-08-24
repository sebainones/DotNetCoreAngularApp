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
    [Migration("20180818085829_CaptialLetters")]
    partial class CaptialLetters
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DotNetCoreAngularAPp.Model.WeatherForecast", b =>
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
#pragma warning restore 612, 618
        }
    }
}