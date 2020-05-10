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

            var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddEnvironmentVariables()
            .AddJsonFile("certificate.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"certificate.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true)
            .Build();

            var certificateSettings = config.GetSection("certificateSettings");
            string certificateFileName = certificateSettings.GetValue<string>("filename");
            string certificatePassword = certificateSettings.GetValue<string>("password");

            //string dbPassword = config.GetValue<string>("dbPwd");

            var certificate = new X509Certificate2(certificateFileName, certificatePassword);

            //IPAddress localhostAddress = IPAddress.Parse("127.0.0.1");  //127.0.0.1 as an example

            CreateWebHostBuilder(args)
            .UseKestrel(
                options =>
                {
                    options.AddServerHeader = false;
                    //This is commented out in order to use LaunchSettings
                    //options.Listen(IPAddress.Loopback, 44321, listenOptions =>
                    //{
                    //    listenOptions.UseHttps(certificate);
                    //});
                    //options.Listen(localhostAddress, 44321, listenOptions =>
                    //{
                    //    listenOptions.UseHttps(certificate);
                    //});
                }
            )
            .UseConfiguration(config)
            .UseContentRoot(Directory.GetCurrentDirectory())
            //.UseStartup<Startup>()  //Defined on the  CreateWebHostBuilder method
            //Defined by the Default or on the LaunchSettings
            //.UseUrls("https://127.0.0.1:44321")
            // .UseUrls("https://localhost:44321")            
            .Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
