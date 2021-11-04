using System;
using System.Security.Claims;
using HomeCooking.Api.Authentication;
using HomeCooking.Application.EventBus;
using HomeCooking.Data;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace HomeCooking.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            using(var client = new RecipeContext(configuration))
            {
                client.Database.EnsureCreated();
            }
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddDapr();            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "HomeCooking.Api", Version = "v1"});
            });
            
            services.AddScoped<IRecipeRepository, SqlRecipeRepository>();
            
            services.AddEntityFrameworkNpgsql().AddDbContext<RecipeContext>(options =>
            {
                options.EnableSensitiveDataLogging();
            });

            services.AddSingleton<IEventBus, EventBus>();
            services.AddMediatR(typeof(Application.CreateRecipeCommand), typeof(Application.CreateRecipeHandler));
            services.AddMediatR(typeof(Application.GetAllRecipesHandler));
            
            ConfigureCors(services);
            
            ConfigureOAuth(services);

            // register the scope authorization handler
            services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();
            
            services.AddHttpContextAccessor();
        }

        private static void ConfigureCors(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        var ip = System.Environment.GetEnvironmentVariable("IP");
                        ip ??= "localhost";      
                        builder
                            .SetIsOriginAllowed(origin => new Uri(origin).Host == ip || new Uri(origin).Host == "localhost" ||
                                                          new Uri(origin).Host == "flux-home-cooking.herokuapp.com")
                            .WithMethods(HttpMethods.Put, HttpMethods.Post, HttpMethods.Delete)
                            .AllowAnyHeader();
                    });
            });
        }

        private void ConfigureOAuth(IServiceCollection services)
        {
            var domain = $"https://{Configuration["Auth0:Domain"]}/";
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = domain;
                options.Audience = Configuration["Auth0:ApiIdentifier"];
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = ClaimTypes.NameIdentifier
                };
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("read:recipes",
                    policy => policy.Requirements.Add(new HasScopeRequirement("read:recipes", domain)));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HomeCooking.Api v1"));

            app.UseCloudEvents();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapSubscribeHandler();
                endpoints.MapControllers();
            });
        }
        
    }
}