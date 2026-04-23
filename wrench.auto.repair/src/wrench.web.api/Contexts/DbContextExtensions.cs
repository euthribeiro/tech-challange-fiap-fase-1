using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using wrench.auto.repair.autenticacao.infra;
using wrench.auto.repair.ordem.servico.infra.Context;
using wrench.web.api.Options;

namespace wrench.web.api.Contexts
{
    public static class DbContextExtensions
    {
        public static void AddContexts(this WebApplicationBuilder builder)
        {
            AddOrdemDeServicoContexto(builder);
            AddAutenticacaoContext(builder);
        }

        private static void AddOrdemDeServicoContexto(WebApplicationBuilder builder)
        {
            // Configurar o DbContext para usar PostgreSQL e pasta migration do contexto ser criada
            // no projeto infra do contexto

            // TODO: Mover string de conexão para secrets ou configuração externa
            builder.Services.AddDbContext<OrdemServicoDbContext>(options =>
            {
                options.UseNpgsql("Host=localhost;Port=5432;Database=db_wrench;Username=postgres;Password=postgres",
                    p => p.MigrationsAssembly("wrench.auto.repair.ordem.servico.infra"));
            });
        }

        private static void AddAutenticacaoContext(WebApplicationBuilder builder)
        {
            var isDevelopment = builder.Environment.IsDevelopment();

            builder.Services.AddDbContext<AutenticacaoContext>((serviceProvider, dbContextOptionsBuilder) =>
            {
                var databaseOptions = serviceProvider.GetService<IOptions<DatabaseOptions>>()!.Value;

                var connectionString = builder.Configuration.GetConnectionString("Database");

                //dbContextOptionsBuilder.UseNpgsql(connectionString, sqlAction =>
                //{
                //    sqlAction.EnableRetryOnFailure(databaseOptions.MaxRetryCount);
                //    sqlAction.CommandTimeout(databaseOptions.CommandTimeout);
                //});

                dbContextOptionsBuilder.UseSqlServer(connectionString, sqlAction =>
                {
                    sqlAction.EnableRetryOnFailure(databaseOptions.MaxRetryCount);
                    sqlAction.CommandTimeout(databaseOptions.CommandTimeout);
                });

                dbContextOptionsBuilder.EnableDetailedErrors(isDevelopment);
                dbContextOptionsBuilder.EnableSensitiveDataLogging(isDevelopment);
            });
        }
    }
}
