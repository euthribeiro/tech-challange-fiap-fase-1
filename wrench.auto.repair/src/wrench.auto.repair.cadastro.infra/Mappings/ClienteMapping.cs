using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using wrench.auto.repair.cadastro.domain.Entities;

namespace wrench.auto.repair.cadastro.infra.Mappings
{
    public class ClienteMapping : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.OwnsOne(c => c.Endereco, e =>
            {
                e.ToTable("ClienteEnderecos");
            });

            throw new NotImplementedException();
        }
    }
}
