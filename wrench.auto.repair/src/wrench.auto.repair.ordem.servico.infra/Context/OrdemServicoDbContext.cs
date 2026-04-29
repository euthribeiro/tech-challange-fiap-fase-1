using Microsoft.EntityFrameworkCore;
using wrench.auto.repair.core.Data;
using wrench.auto.repair.ordem.servico.domain.Entities;
using wrench.auto.repair.ordem.servico.domain.ValueObjects;

namespace wrench.auto.repair.ordem.servico.infra.Context
{
    // dotnet ef migrations add OrdemServicoInit --project .\src\wrench.auto.repair.ordem.servico.infra --startup-project .\src\wrench.web.api --context OrdemServicoDbContext
    // dotnet ef database update --project .\src\wrench.auto.repair.autenticacao.infra --startup-project .\src\wrench.web.api --context OrdemServicoDbContext
    public class OrdemServicoDbContext : DbContext, IUnitOfWork
    {
        public DbSet<OrdemServico> OrdemServico { get; set; }
        public DbSet<Diagnostico> Diagnostico { get; set; }
        public DbSet<Orcamento> Orcamento { get; set; }

        public OrdemServicoDbContext(DbContextOptions<OrdemServicoDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("ordem_servico");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrdemServicoDbContext).Assembly);



            base.OnModelCreating(modelBuilder);
        }

        public async Task<bool> CommitAsync()
        {
            return await base.SaveChangesAsync() > 0;
        }
    }
}
