using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NJsonSchema;
using NSwag.AspNetCore;
using WebApiUnsecure.Model;



namespace WebApiUnsecure
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //For REST Api Documentation
            services.AddSwaggerGen(
                                    c => c.SwaggerDoc("V1", new Swashbuckle.AspNetCore.Swagger.Info() { Title = "Docum" })
                                   );

            //services.AddDbContext<ForeCastContext>(options => options.UseSqlServer(connectionString));

            services.AddDbContext<TicketContext>(options => options.UseSqlServer(connectionString));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //For REST Api Documentation
            //https://docs.microsoft.com/en-us/aspnet/core/tutorials/web-api-help-pages-using-swagger?view=aspnetcore-2.1
            //https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-nswag?view=aspnetcore-2.1&tabs=visual-studio%2Cvisual-studio-xml


            // Enable the Swagger UI middleware and the Swagger generator
            app.UseSwaggerUi(typeof(Startup).GetTypeInfo().Assembly, settings =>
            {
                settings.GeneratorSettings.DefaultPropertyNameHandling = PropertyNameHandling.CamelCase;
            });

            app.UseMvc();
        }
    }
}
