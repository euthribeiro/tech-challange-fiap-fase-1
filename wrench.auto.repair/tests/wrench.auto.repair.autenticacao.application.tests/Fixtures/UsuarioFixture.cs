using Bogus;
using wrench.auto.repair.autenticacao.domain.Entities;
using wrench.auto.repair.autenticacao.infra.Security;
using wrench.auto.repair.core.ValueObjects;

namespace wrench.auto.repair.autenticacao.application.tests.Fixtures
{
    [CollectionDefinition(nameof(UsuarioCollection))]
    public class UsuarioCollection : ICollectionFixture<UsuarioFixture>
    {

    }

    public class UsuarioFixture
    {
        public string GerarEmail()
        {
            return new Faker().Internet.Email();
        }

        public string GerarSenha(int tamanho)
        {
            return new Faker().Internet.Password(tamanho);
        }

        public Perfil GerarPerfil()
        {
            return new Faker<Perfil>("pt_BR")
            .CustomInstantiator((fk) => new Perfil("Admin", fk.Random.Word(), true, fk.Date.Recent()));
        }

        public Usuario GerarUsuario()
        {
            return new Usuario(new Email(GerarEmail()), Guid.NewGuid(), true, DateTime.Now);
        }

        public string GerarHashSenha(string senha)
        {
            return new PasswordHasher().GerarHash(senha);
        }
    }
}
