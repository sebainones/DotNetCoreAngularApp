using System;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace DotNetCoreAngularApp
{
    public class Program
    {
        //https://www.humankode.com/asp-net-core/develop-locally-with-https-self-signed-certificates-and-asp-net-core
        public static void Main(string[] args)
        {

            IConfigurationRoot config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddEnvironmentVariables()
            .AddJsonFile("certificate.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"certificate.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true)
            .Build();

            var certificateSettings = config.GetSection("certificateSettings");
            string certificateFileName = certificateSettings.GetValue<string>("filename");
            string certificatePassword = certificateSettings.GetValue<string>("password");

            string dbPassword = config.GetValue<string>("dbPwd");

            var certificate = new X509Certificate2(certificateFileName, certificatePassword);


            CreateWebHostBuilder(args)
            //TODO: If i Uncomment this, then the Angular Client does not launch automatically and it NO longer works!!!
            //Need to sort this out!!!
            //.UseKestrel(
            //    options =>
            //    {
            //        options.AddServerHeader = false;
            //        options.ListenLocalhost(5001, listenOptions =>
            //        {
            //            listenOptions.UseHttps(certificate);
            //        });
            //    })
            .UseConfiguration(config)
            .UseContentRoot(Directory.GetCurrentDirectory())
            .Build()
            .Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
