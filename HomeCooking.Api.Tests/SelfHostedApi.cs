using System.Security.Claims;
using HomeCooking.Application.EventBus;
using HomeCooking.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;

namespace HomeCooking.Api.Tests
{
    public sealed class SelfHostedApi : WebApplicationFactory<Startup>
    {
        public static string TestUser = "TestUser";
        
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.RemoveAll<IRecipeRepository>();
                
                // TODO - use SqlLite in memory
                services.AddScoped<IRecipeRepository, SqlRecipeRepository>();

                services.RemoveAll<IAuthorizationHandler>();
                services.AddSingleton<IAuthorizationHandler, FakeScopeHandler>();
                services.AddSingleton<IEventBus, FakeEventBus>();
                
                var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
                mockHttpContextAccessor.Setup(req => req.HttpContext.User.FindFirst(It.IsAny<string>())).Returns(new Claim(ClaimTypes.NameIdentifier, TestUser));
                services.AddSingleton<IHttpContextAccessor>(mockHttpContextAccessor.Object);
            });
        }
    }
}