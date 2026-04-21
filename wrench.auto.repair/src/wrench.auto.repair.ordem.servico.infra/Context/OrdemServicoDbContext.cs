using Microsoft.EntityFrameworkCore;
using wrench.auto.repair.ordem.servico.domain.Entities;

namespace wrench.auto.repair.ordem.servico.infra.Context
{
    public class OrdemServicoDbContext : DbContext
    {
        public DbSet<OrdemServico> OrdemServico { get; set; }

        public OrdemServicoDbContext(DbContextOptions<OrdemServicoDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("ordem_servico");

            base.OnModelCreating(modelBuilder);
        }
    }
}
