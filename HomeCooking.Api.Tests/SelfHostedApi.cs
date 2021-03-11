using HomeCooking.Api.Authentication;
using HomeCooking.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace HomeCooking.Api.Tests
{
    public sealed class SelfHostedApi : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.RemoveAll<IRecipeRepository>();
                services.AddSingleton<IRecipeRepository>(new FakeDatabase());
                
                services.RemoveAll<IAuthorizationHandler>();
                services.AddSingleton<IAuthorizationHandler, FakeScopeHandler>();
                
            });
        }
    }
}