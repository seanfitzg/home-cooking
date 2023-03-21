using System;
using HomeCooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace HomeCooking.Data
{
    public class RecipeContext : DbContext
    {
        public RecipeContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private readonly IConfiguration _configuration;
        
        public DbSet<User> Users { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }

        private static readonly ILoggerFactory ConsoleLoggerFactory
            = LoggerFactory.Create(builder =>
            {
                builder.AddFilter((category, level) =>
                            category == DbLoggerCategory.Database.Command.Name && 
                            level == LogLevel.Information)
                    .AddConsole();
            });
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = GetConnectionString();
            var pgSqlConnectionString = connectionString;
            optionsBuilder
                .UseLoggerFactory(ConsoleLoggerFactory)
                .UseNpgsql(pgSqlConnectionString);
        }

        private string GetConnectionString()
        {
            //var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            
            var host = Environment.GetEnvironmentVariable("DBHOST");
            var port = Environment.GetEnvironmentVariable("DBPORT");
            var db = Environment.GetEnvironmentVariable("DBNAME");
            var username = Environment.GetEnvironmentVariable("DBUSERNAME");
            var password = Environment.GetEnvironmentVariable("DBPASSWORD");
            
            //var connectionString = _configuration.GetSection($"ConnectionStrings:{env}").Value;
            var connectionString = $"Host={host};Port={port};Database={db};Username={username};Password={password}";
            return connectionString;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ingredient>().ToTable("Ingredients");
        }
    }
}