using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;
using wrench.auto.repair.autenticacao.infra;
using wrench.auto.repair.cadastro.infra;
using wrench.auto.repair.estoque.infra.Context;
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

            var ordemContext = scope.ServiceProvider.GetRequiredService<OrdemServicoDbContext>();
            await ordemContext.Database.EnsureCreatedAsync();

            var cadastroContext = scope.ServiceProvider.GetRequiredService<CadastroContext>();
            var dbCreatorCadastro = cadastroContext.Database.GetService<Microsoft.EntityFrameworkCore.Storage.IRelationalDatabaseCreator>();
            await dbCreatorCadastro.CreateTablesAsync();

            var authContext = scope.ServiceProvider.GetRequiredService<AutenticacaoContext>();
            var dbCreatorAuth = authContext.Database.GetService<Microsoft.EntityFrameworkCore.Storage.IRelationalDatabaseCreator>();
            await dbCreatorAuth.CreateTablesAsync();

            var estoqueContext = scope.ServiceProvider.GetRequiredService<PecaDbContext>();
            var dbCreatorEstoque = estoqueContext.Database.GetService<Microsoft.EntityFrameworkCore.Storage.IRelationalDatabaseCreator>();
            await dbCreatorEstoque.CreateTablesAsync();

        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                var descriptorOrdem = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<OrdemServicoDbContext>));
                if (descriptorOrdem != null) services.Remove(descriptorOrdem);

                var descriptorCadastro = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<CadastroContext>));
                if (descriptorCadastro != null) services.Remove(descriptorCadastro);

                var descriptorAuth = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<AutenticacaoContext>));
                if (descriptorAuth != null) services.Remove(descriptorAuth);

                var descriptorEstoque = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<PecaDbContext>));
                if (descriptorEstoque != null) services.Remove(descriptorEstoque);

                string connectionString = _dbContainer.GetConnectionString();

                services.AddDbContext<OrdemServicoDbContext>(options => options.UseNpgsql(connectionString));
                services.AddDbContext<CadastroContext>(options => options.UseNpgsql(connectionString));
                services.AddDbContext<AutenticacaoContext>(options => options.UseNpgsql(connectionString));
                services.AddDbContext<PecaDbContext>(options => options.UseNpgsql(connectionString));

                services.AddAuthentication(TestAuthHandler.DefaultScheme)
                        .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(TestAuthHandler.DefaultScheme, options => { });
            });
        }

        async Task IAsyncLifetime.DisposeAsync()
        {
            await _dbContainer.StopAsync();
        }

    }
}
