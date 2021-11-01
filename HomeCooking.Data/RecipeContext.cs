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
            // var dataServer = Environment.GetEnvironmentVariable("DATA_SERVER") ?? "localhost";
            // var useMySql = Environment.GetEnvironmentVariable("USE_MY_SQL") == "true";
            // var pgSqlConnectioString =
            //     "Host=ec2-54-161-189-150.compute-1.amazonaws.com;Port=5432;Database=d8a3si2306grc5;Username=kapirumozmjysa;Password=762e33e53486014ef9c7daf55816e707ec90d6692aa0d27bc77acda94e22ccfb;sslmode=Require;Trust Server Certificate=true;";
                
            var pgSqlConnectioString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") ?? "Host=localhost;Port=5432;Database=homecooking;Username=homecooking;Password=homecooking";
            Console.WriteLine($@"Using postGres: {pgSqlConnectioString}");
            optionsBuilder
                .UseLoggerFactory(ConsoleLoggerFactory)
                .UseNpgsql(pgSqlConnectioString);            
            // if (useMySql)
            // {
            //     Console.WriteLine("Using MySQL");
            //     optionsBuilder
            //         .UseLoggerFactory(ConsoleLoggerFactory)
            //         .UseMySQL($@"Server={dataServer}; Port=3306; Database=homecooking; Uid=dbuser; Pwd=Password1!;");
            // } 
            // else
            // {
            //     Console.WriteLine("Using SQLLite");
            //     var connection = _configuration["SqlLite:ConnectionString"];
            //     //var connection = "DataSource=file::memory:?cache=shared";
            //     optionsBuilder
            //         .UseLoggerFactory(ConsoleLoggerFactory)
            //         .UseSqlite(connection);            
            // }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ingredient>().ToTable("Ingredients");
        }
    }
}