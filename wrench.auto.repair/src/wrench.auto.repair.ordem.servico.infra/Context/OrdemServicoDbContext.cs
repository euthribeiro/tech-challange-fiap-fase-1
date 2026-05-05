using Microsoft.EntityFrameworkCore;
using wrench.auto.repair.core.Data;
using wrench.auto.repair.ordem.servico.domain.Entities;
using wrench.auto.repair.ordem.servico.infra.Extensions;

namespace wrench.auto.repair.ordem.servico.infra.Context
{
    // dotnet ef migrations add OrdemServicoInit --project .\src\wrench.auto.repair.ordem.servico.infra --startup-project .\src\wrench.web.api --context OrdemServicoDbContext
    // dotnet ef database update --project .\src\wrench.auto.repair.autenticacao.infra --startup-project .\src\wrench.web.api --context OrdemServicoDbContext
    public class OrdemServicoDbContext(DbContextOptions<OrdemServicoDbContext> options) : DbContext(options), IUnitOfWork
    {
        public DbSet<OrdemServico> OrdemServico { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrdemServicoDbContext).Assembly);

            modelBuilder
                .HasDbFunction(typeof(PostgresDbFunctions)
                .GetMethod(nameof(PostgresDbFunctions.DateDiffMilliseconds))!)
                .HasName("datediff_milliseconds");

            base.OnModelCreating(modelBuilder);
        }

        public async Task<bool> CommitAsync()
        {
            return await base.SaveChangesAsync() > 0;
        }
    }
}
