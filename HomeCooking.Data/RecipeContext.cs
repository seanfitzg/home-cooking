using HomeCooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HomeCooking.Data
{
    public class RecipeContext : DbContext
    {
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source= RecipeDb.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Weight>().ToTable("Weights");
            modelBuilder.Entity<Other>().ToTable("Others");
            modelBuilder.Entity<Volume>().ToTable("Volumes");
        }
    }
}