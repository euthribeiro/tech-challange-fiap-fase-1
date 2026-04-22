using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;
using wrench.auto.repair.ordem.servico.infra.Context;

namespace wrench.web.api.integration.tests.Base
{
    public class IntegrationTestFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
       .WithImage("postgres:16-alpine")
       .WithDatabase("db_test")
       .WithUsername("postgres")
       .WithPassword("postgres")
       .Build();

        public async Task InitializeAsync()
        {
            await _dbContainer.StartAsync();

            using var scope = Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<OrdemServicoDbContext>();

            await context.Database.EnsureCreatedAsync();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                // 1. Removemos a configuração original do DbContext (que aponta para o banco real)
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<OrdemServicoDbContext>));
                if (descriptor != null) services.Remove(descriptor);

                // 2. Adicionamos o DbContext apontando para a string de conexão dinâmica do container
                services.AddDbContext<OrdemServicoDbContext>(options =>
                {
                    string connectionString = _dbContainer.GetConnectionString();
                    options.UseNpgsql(connectionString);
                });
            });
        }

        async Task IAsyncLifetime.DisposeAsync()
        {
            await _dbContainer.StopAsync();
        }

    }
}
