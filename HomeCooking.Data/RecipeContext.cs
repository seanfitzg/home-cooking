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
            optionsBuilder
                .UseLoggerFactory(ConsoleLoggerFactory)
                .UseMySQL("server=localhost;port=3306;database=homecooking;user=dbuser;password=Password1!");
            //.UseSqlite($"Data Source=RecipeDb.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ingredient>().ToTable("Ingredients");
        }
    }
}