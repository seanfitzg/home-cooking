using System;
using HomeCooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HomeCooking.Data
{
    public class RecipeContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        
        public static readonly ILoggerFactory ConsoleLoggerFactory
            = LoggerFactory.Create(builder =>
            {
                builder.AddFilter((category, level) =>
                            category == DbLoggerCategory.Database.Command.Name && 
                            level == LogLevel.Information)
                    .AddConsole();
            });
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dataServer = Environment.GetEnvironmentVariable("DATA_SERVER") ?? "localhost";
            var useMySql = Environment.GetEnvironmentVariable("USE_MY_SQL") == "true";
            Console.WriteLine($@"Using DATA_SERVER: {dataServer}");
            if (useMySql)
            {
                Console.WriteLine("Using MySQL");
                optionsBuilder
                    .UseLoggerFactory(ConsoleLoggerFactory)
                    .UseMySQL($@"Server={dataServer}; Port=3306; Database=homecooking; Uid=dbuser; Pwd=Password1!;");
            } 
            else
            {
                Console.WriteLine("Using SQLLite");
                optionsBuilder
                    .UseLoggerFactory(ConsoleLoggerFactory)
                    .UseSqlite($"Data Source=RecipeDb.db"); 
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ingredient>().ToTable("Ingredients");
        }
    }
}