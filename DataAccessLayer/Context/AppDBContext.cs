using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace DataAccessLayer.Context
{
    public class AppDBContext : DbContext, IAppDBContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserProfile> UserProfiles { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<Municipality> Municipalities { get; set; }

        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Suburbs> Suburbs { get; set; }

        public virtual DbSet<Motorcycle> Motorcycles { get; set; }
        public virtual DbSet<MotorcycleImage> MotorcycleImages { get; set; }


    }

    public class DesignTimeDbContextFactory  : IDesignTimeDbContextFactory<AppDBContext>
    {
        public AppDBContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(@Directory.GetCurrentDirectory() + "/../MCApiV/appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<AppDBContext>();
            var connectionString = configuration.GetConnectionString("DatabaseConnection");
            builder.UseSqlServer(connectionString, b => b.MigrationsAssembly("MCApiV")); 

            return new AppDBContext(builder.Options);
        }
    }
}
