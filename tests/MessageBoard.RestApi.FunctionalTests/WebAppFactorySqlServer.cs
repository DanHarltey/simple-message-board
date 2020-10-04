using MessageBoard.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MessageBoard.RestApi.FunctionalTests
{
    public class WebAppFactorySqlServer : WebApplicationFactory<Startup>, IAsyncLifetime
    {
        private readonly Dictionary<string, string> _appSettings = new Dictionary<string, string>
        {
            { "ConnectionStrings:SqlServer", "Data Source=localhost;Initial Catalog=MessageServer.FunctionalTests;Integrated Security=true" }
        };

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("FunctionalTests");

            UseSqlServer(builder);
        }

        private void UseSqlServer(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.AddInMemoryCollection(_appSettings);
                config.AddEnvironmentVariables();
            });
        }

        public async Task InitializeAsync()
        {
            using var scope = Server.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<MessageBoardContext>();
            await db.Database.EnsureDeletedAsync();
            await db.Database.MigrateAsync();
            await db.Database.EnsureCreatedAsync();
        }

        public Task DisposeAsync() => Task.CompletedTask;
    }
}
