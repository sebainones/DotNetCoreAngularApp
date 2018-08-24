using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace WebApiUnsecure.Model
{
    public class TicketContext : DbContext
    {
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly IConfiguration configuration;
        private readonly string dbPassword;
        private readonly string connectionString;

        public TicketContext(DbContextOptions<TicketContext> options, IHostingEnvironment env, IConfiguration configuration) : base(options)
        {
            hostingEnvironment = env;
            this.configuration = configuration;

            dbPassword = this.configuration.GetValue<string>("dbPwd");

            connectionString = $"Server=tcp:sebasserver.database.windows.net,1433;Initial Catalog=SebaDataBase;Persist Security Info=False;User ID=sebainones;Password={dbPassword};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Remember to have the real username and pwd!           
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            if (hostingEnvironment.IsDevelopment())
                SeedData(modelBuilder);
        }

        private static void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ticket>().HasData(
                            new Ticket() { Artist = "Zaz", Concert = "MadridPalau", Id = 1, Available = true },
                            new Ticket() { Artist = "Ciagala", Concert = "La carboneria", Id = 2, Available = true }
                            );
        }

        public DbSet<Ticket> Tickets { get; set; }
    }
}
