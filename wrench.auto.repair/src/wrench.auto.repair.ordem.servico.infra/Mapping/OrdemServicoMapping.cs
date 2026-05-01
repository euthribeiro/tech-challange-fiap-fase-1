using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using wrench.auto.repair.ordem.servico.domain.Entities;

namespace wrench.auto.repair.ordem.servico.infra.Mapping
{
    public class OrdemServicoMapping : IEntityTypeConfiguration<OrdemServico>
    {
        public void Configure(EntityTypeBuilder<OrdemServico> builder)
        {
            builder.OwnsOne(c => c.Diagnostico, d =>
            {
                d.WithOwner();
                d.ToTable("OrdemServicoDiagnostico");
            });

            builder.OwnsOne(c => c.Orcamento, d =>
            {
                d.WithOwner();
                d.WithOwner().HasForeignKey("OrdemServicoId");
                d.ToTable("OrdemServicoOrcamento");
            });

            builder.Navigation(c => c.Diagnostico)
                .IsRequired(false);

            builder.Navigation(c => c.Orcamento)
                .IsRequired(false);
        }
    }
}
