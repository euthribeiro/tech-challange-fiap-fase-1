using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using wrench.auto.repair.autenticacao.domain.Entities;

namespace wrench.auto.repair.autenticacao.infra.Mappings
{
    public class PerfilMapping : IEntityTypeConfiguration<Perfil>
    {
        public void Configure(EntityTypeBuilder<Perfil> builder)
        {
            builder.HasKey(p => p.Id);

            //builder.HasData([
            //    new Perfil("Admin", "Perfil Adminstrativo", true, DateTime.Now),
            //    new Perfil("Employee", "Perfil Funcionário", true, DateTime.Now),
            //]);
        }
    }
}
