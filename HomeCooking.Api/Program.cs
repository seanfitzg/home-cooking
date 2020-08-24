using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeCooking.Data;
using HomeCooking.Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HomeCooking.Api
{
    public class Program
    {
        private static RecipeContext _recipeContext = new RecipeContext();
        
        public static void Main(string[] args)
        {
            // _recipeContext.Database.EnsureCreated();
            // _recipeContext.Users.Add(new User()
            // {
            //     Name = "Sean"
            // });
            // _recipeContext.SaveChanges();
            // var users = _recipeContext.Users.ToList();
            //var x = _recipeContext.Recipes.Where(recipe => EF.Functions.Like)
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}