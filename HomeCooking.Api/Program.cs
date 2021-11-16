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
        public static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Starting HomeCooking API...");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    
                    // required for Heroku???
                    // better to use ASPNETCORE_URLS - https://andrewlock.net/5-ways-to-set-the-urls-for-an-aspnetcore-app/
                    // var port = System.Environment.GetEnvironmentVariable("PORT");
                    // port ??= "5000";
                    // var ip = System.Environment.GetEnvironmentVariable("IP");
                    // ip ??= "localhost"; 
                    // webBuilder.UseUrls($"http://{ip}:{port}");  // required for Heroku???
                });
    }
}