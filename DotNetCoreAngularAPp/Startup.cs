using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DotNetCoreAngularAPp
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //TODO: extract all this logic for instance in a BootStrap class
            //because this clas has 3 different responsabilities

            services.AddDbContext<WeatherDbContext>(options =>
                                                        options.UseInMemoryDatabase("name")
                                                    //options.UseSqlServer(connectionString
                                                    );

            services.AddLogging();

            services.AddMvc(
                options =>
                {
                    options.SslPort = 44321;
                    options.Filters.Add(new RequireHttpsAttribute());
                }
            ).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);


            services.AddAntiforgery(
                options =>
                {
                    options.Cookie.Name = "_af";
                    options.Cookie.HttpOnly = true;
                    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                    options.HeaderName = "X-XSRF-TOKEN";
                }
            );

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            //For instance: a user defined Interface/Class troughout all the app lifecycle
            //services.AddSingleton<IEmailSeneder, EmailSender>();

            //For instance: a user defined Interface/Class troughout all the request lifecycle
            //services.AddScoped<IEmailSeneder, EmailSender>();

            //For instance: a user defined Interface/Class new each time is requested-
            //services.AddTransient<IEmailSeneder, EmailSender>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //Configure http pipeline
        //The order in which these statements appear is important
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                loggerFactory.AddConsole();
                loggerFactory.AddDebug();

                app.UseDeveloperExceptionPage();
                //app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Error"); //500
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            //Would serve the file if it find it on the disk even without the @ from razor pages, but wouldn't render it.
            //For instance the picture in the contact page
             app.UseStaticFiles();

            app.UseSpaStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
