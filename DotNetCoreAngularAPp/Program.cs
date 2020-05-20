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

            try
            {
                IConfigurationRoot config = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddEnvironmentVariables()
               .AddJsonFile("certificate.json", optional: true, reloadOnChange: true)
               .AddJsonFile($"certificate.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true)
               .Build();

                string dbPassword = config.GetValue<string>("dbPwd");

                X509Certificate2 certificate = GenrateCertificate(config);

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
            //TODO: handle this properly!
            catch (Exception)
            {

                throw;
            }
        }

        private static X509Certificate2 GenrateCertificate(IConfigurationRoot config)
        {
            X509Certificate2 certificate;
            //TODO: Re-generate the certificate AGAIN!
            var certificateSettings = config.GetSection("certificateSettings");
            string certificateFileName = certificateSettings.GetValue<string>("filename");
            string certificatePassword = certificateSettings.GetValue<string>("password");
            certificate = new X509Certificate2(certificateFileName, certificatePassword);
            return certificate;
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
