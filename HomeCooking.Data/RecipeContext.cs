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
            Console.WriteLine($@"Using DATA_SERVER: {dataServer}");
            optionsBuilder
                .UseLoggerFactory(ConsoleLoggerFactory)
                .UseMySQL($@"Server={dataServer}; Port=3306; Database=homecooking; Uid=dbuser; Pwd=Password1!;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ingredient>().ToTable("Ingredients");
        }
    }
}