using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using wrench.auto.repair.cadastro.domain.ValueObjects;

namespace wrench.auto.repair.cadastro.infra.Mappings
{
    public class EnderecoMapping : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            throw new NotImplementedException();
        }
    }
}
