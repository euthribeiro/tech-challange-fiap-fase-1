using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using wrench.auto.repair.autenticacao.infra;
using wrench.auto.repair.estoque.infra.Context;
using wrench.auto.repair.ordem.servico.infra.Context;
using wrench.web.api.Options;

namespace wrench.web.api.Contexts
{
    public static class DatabaseContextConfigure
    {
        public static void AddDbContexts(this IServiceCollection services)
        {
            AddAutenticacaoDbContext(services);
            AddEstoqueDbContext(services);
            AddOrdemServicoDbContext(services);
        }

        private static void AddOrdemServicoDbContext(IServiceCollection services)
        {
            services.AddDbContext<OrdemServicoDbContext>((serviceProvider, dbContextOptionsBuilder) =>
            {
                var databaseOptions = serviceProvider.GetService<IOptions<DatabaseOptions>>()!.Value;

                dbContextOptionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=db_wrench;Username=postgres;Password=postgres", sqlAction =>
                {
                    sqlAction.EnableRetryOnFailure(databaseOptions.MaxRetryCount);
                    sqlAction.CommandTimeout(databaseOptions.CommandTimeout);
                    sqlAction.MigrationsAssembly("wrench.auto.repair.ordem.servico.infra");
                });

                dbContextOptionsBuilder.EnableDetailedErrors(databaseOptions.EnableDetailedErrors);
                dbContextOptionsBuilder.EnableSensitiveDataLogging(databaseOptions.EnableSensitiveDataLogging);
            });
        }

        private static void AddEstoqueDbContext(IServiceCollection services)
        {
            services.AddDbContext<PecaDbContext>((serviceProvider, dbContextOptionsBuilder) =>
            {
                var databaseOptions = serviceProvider.GetService<IOptions<DatabaseOptions>>()!.Value;

                dbContextOptionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=db_wrench;Username=postgres;Password=postgres", sqlAction =>
                {
                    sqlAction.EnableRetryOnFailure(databaseOptions.MaxRetryCount);
                    sqlAction.CommandTimeout(databaseOptions.CommandTimeout);
                    sqlAction.MigrationsAssembly("wrench.auto.repair.estoque.infra");
                });


                dbContextOptionsBuilder.EnableDetailedErrors(databaseOptions.EnableDetailedErrors);
                dbContextOptionsBuilder.EnableSensitiveDataLogging(databaseOptions.EnableSensitiveDataLogging);
            });
        }

        private static void AddAutenticacaoDbContext(IServiceCollection services)
        {
            services.AddDbContext<AutenticacaoContext>((serviceProvider, dbContextOptionsBuilder) =>
            {
                var databaseOptions = serviceProvider.GetService<IOptions<DatabaseOptions>>()!.Value;

                //dbContextOptionsBuilder.UseNpgsql(connectionString, sqlAction =>
                //{
                //    sqlAction.EnableRetryOnFailure(databaseOptions.MaxRetryCount);
                //    sqlAction.CommandTimeout(databaseOptions.CommandTimeout);
                //});

                dbContextOptionsBuilder.UseSqlServer(databaseOptions.ConnectionString, sqlAction =>
                {
                    sqlAction.EnableRetryOnFailure(databaseOptions.MaxRetryCount);
                    sqlAction.CommandTimeout(databaseOptions.CommandTimeout);
                });

                dbContextOptionsBuilder.EnableDetailedErrors(databaseOptions.EnableDetailedErrors);
                dbContextOptionsBuilder.EnableSensitiveDataLogging(databaseOptions.EnableSensitiveDataLogging);
            });
        }
    }
}
