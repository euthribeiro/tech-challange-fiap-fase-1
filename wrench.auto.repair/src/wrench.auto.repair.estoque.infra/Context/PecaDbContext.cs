
using Microsoft.EntityFrameworkCore;
using wrench.auto.repair.estoque.domain.Entities;

namespace wrench.auto.repair.estoque.infra.Context;

public class PecaDbContext: DbContext
{
    public DbSet<Peca> Pecas { get; set; }
    
    public PecaDbContext(DbContextOptions<PecaDbContext> options): base(options) {}
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("peca");

        base.OnModelCreating(modelBuilder);
    }
}