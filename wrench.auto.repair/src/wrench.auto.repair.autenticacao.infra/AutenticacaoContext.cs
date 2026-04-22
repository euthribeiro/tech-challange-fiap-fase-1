using Microsoft.EntityFrameworkCore;
using wrench.auto.repair.autenticacao.domain.Entities;
using wrench.auto.repair.core.Data;

namespace wrench.auto.repair.autenticacao.infra
{
    public class AutenticacaoContext : DbContext, IUnitOfWork
    {
        public AutenticacaoContext(DbContextOptions<AutenticacaoContext> options) :
            base(options)
        { }

        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AutenticacaoContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        public async Task<bool> CommitAsync()
        {
            return await base.SaveChangesAsync() > 0;
        }
    }
}
