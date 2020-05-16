using System;
using DotNetCoreAngularApp.Configuration;
using DotNetCoreAngularApp.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DotNetCoreAngularApp
{
    public class Startup
    {
        private readonly string connectionString;
        private readonly string dbPassword;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            dbPassword = Configuration.GetValue<string>("dbPwd");

            connectionString = $"Server=tcp:sebasserver.database.windows.net,1433;Initial Catalog=SebaDataBase;Persist Security Info=False;User ID=sebainones;Password={dbPassword};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //TODO: extract all this logic for instance in a BootStrap class
            //because this clas has 3 different responsabilities

            //In MemoryDataBase
            services.AddDbContext<WeatherDbContext>(options => options.UseInMemoryDatabase("name"));

            //TODO: I have no longer an Azure DB
            //services.AddDbContext<ForeCastContext>(options => options.UseSqlServer(connectionString));


            //TODO: Identiy ISSUE!
            //services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<ForeCastContext>();
            //services.AddDefaultIdentity<ForeCastUser>().AddEntityFrameworkStores<ForeCastContext>();
            //services.AddDefaultIdentity<ForeCastUser>().AddEntityFrameworkStores<ForeCastContext>().AddDefaultTokenProviders();

            //I don't want to have roles for the moment.
            //services.AddIdentity<ForeCastUser, IdentityRole>().AddEntityFrameworkStores<ForeCastContext>();
            //services.AddIdentity<IdentityUser<Guid>, IdentityRole<Guid>>().AddEntityFrameworkStores<ForeCastContext>();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });

            services.ConfigureApplicationCookie(options =>
           {
               // Cookie settings
               options.Cookie.HttpOnly = true;
               options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

               options.LoginPath = "/Identity/Account/Login";
               options.AccessDeniedPath = "/Identity/Account/AccessDenied";
               options.SlidingExpiration = true;
           });


            services.AddLogging();

            //TODO: ISSUE with HTTPS!!
            //services.AddMvc(
            //    options =>
            //    {
            //        options.Filters.Add(new RequireHttpsAttribute());
            //    }
            //).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddMvc();


            services.Configure<MyConfiguration>(Configuration.GetSection("MyConfig"));

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
        //The order that middleware components are added in the Configure method defines the order in which they're invoked on requests, 
        //and the reverse order for the response. This ordering is critical for security, performance, and functionality.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // The Configure method (shown below) adds the following middleware components:

            //1 - Exception/error handling
            //2- Static file server
            //3- Authentication
            //4-  MVC
            //5 - Angular

            loggerFactory.AddConsole();

            if (env.IsDevelopment() || env.IsEnvironment("PreProd"))
            {
                loggerFactory.AddDebug();

                //1 - Exception/error handling
                app.UseDeveloperExceptionPage();
                //app.UseBrowserLink();
            }
            else
            {
                //1 - Exception/error handling
                app.UseExceptionHandler("/Error"); //500
                app.UseHsts();
            }


            //TODO:ISSUE
            //HttpsConnectionAdapter:Debug: Failed to authenticate HTTPS connection.!!!!!
            //app.UseHttpsRedirection();


            //2- Static file server
            ////Would serve the file if it find it on the disk even without the @ from razor pages, but wouldn't render it.
            //For instance the picture in the contact page
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseCookiePolicy();
            //Before HttpContext.User is needed. Terminal for OAuth callbacks.

            //3- Authentication
            //To use ASP.NET Core Identity you also need to enable authentication.
            //Identity is enabled by calling UseAuthentication. This adds authentication middleware to the request pipeline.
            app.UseAuthentication();

            //app.UseResponseCompression();

            var _log = loggerFactory.CreateLogger("Sebass");

            //Custom middleware  per each request comming!
            app.Use(async (context, next) =>
            {
                _log.LogWarning("====>Incoming Request");
                await next();
            });

            //4-  MVC
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            //5 - Angular
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

        //Executes this when the environment variable is Development
        //TODO: check this out!! --->This Generates an ISSUE!!!!<---
        //public void ConfigureDevelopment()
        //{
        //    //Only Development Stufff
        //    //Exceptions
        //    //InMemmoryData
        //    //etc.

        //}
    }
}