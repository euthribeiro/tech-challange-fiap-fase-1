using wrench.auto.repair.autenticacao.domain.Entities;
using wrench.auto.repair.autenticacao.domain.tests.Fixture;
using wrench.auto.repair.core.DomainObjects;

namespace wrench.auto.repair.autenticacao.domain.tests
{
    [Collection("UsuarioCollection")]
    public class UsuarioTests(UsuarioFixture _fixture)
    {
        [Fact(DisplayName = "Criar Usuario Dados Vazio Deve Retornar Exception")]
        [Trait("Autenticacao", "Domains")]
        public void CriarUsuario_DadosVazio_DeveRetornarException()
        {
            // Arrange
            var email = _fixture.GerarEmail();

            // Act & Assert
            Assert.Throws<DomainException>(() => new Usuario(null, perfilId: Guid.NewGuid(), false, DateTime.Now));
            Assert.Throws<DomainException>(() => new Usuario(email, perfilId: Guid.Empty, false, DateTime.Now));
        }

        [Fact(DisplayName = "Criar Usuario Definir Senha Vazia Deve Retornar Exception")]
        [Trait("Autenticacao", "Domains")]
        public void CriarUsuario_DefinirSenhaVazia_DeveRetornarException()
        {
            // Arrange
            var usuario = _fixture.GerarUsuario();

            // Act & Assert
            var exception = Assert.Throws<DomainException>(() => usuario.DefinirSenha(""));
        }

        [Fact(DisplayName = "Criar Usuario Definir Senha Menor Que 60 Caracteres Deve Retornar Exception")]
        [Trait("Autenticacao", "Domains")]
        public void CriarUsuario_DefinirSenhaMenorQue60Caracteres_DeveRetornarException()
        {
            // Arrange
            var usuario = _fixture.GerarUsuario();

            // Act & Assert
            var exception = Assert.Throws<DomainException>(() => usuario.DefinirSenha("hash_senha"));
        }

        [Fact(DisplayName = "Criar Usuario Definir Senha Com Sucesso")]
        [Trait("Autenticacao", "Domains")]
        public void CriarUsuario_DefinirSenha_DeveFazerHashDaSenha()
        {
            // Arrange
            var usuario = _fixture.GerarUsuario();
            var senha = _fixture.GerarSenha(tamanho: 24);
            var hashSenha = _fixture.GerarHashSenha(senha);

            // Act
            usuario.DefinirSenha(hashSenha);

            // Assert
            Assert.Equal(hashSenha, usuario.Senha);
        }

        [Fact(DisplayName = "Criar Perfil Com Dados Vazio Deve Retornar Exception")]
        [Trait("Autenticacao", "Domains")]
        public void CriarPerfilDadosVazioDeveRetornarException()
        {
            // Arrange & Act & Assert
            Assert.Throws<DomainException>(() => new Perfil(nome: "", descricao: "Perfil Administrativo", true, DateTime.Now));
            Assert.Throws<DomainException>(() => new Perfil(nome: "Admin", descricao: "", true, DateTime.Now));
        }

        [Fact(DisplayName = "Alterar Descricao Perfil Nova Descricao Vazia Deve Retornar Exception")]
        [Trait("Autenticacao", "Domains")]
        public void AlterarDescricaoPerfilNovaDescricaoVaziaDeveRetornarException()
        {
            // Arrange
            var perfil = _fixture.GerarPerfil();

            // Act & Assert
            Assert.Throws<DomainException>(() => perfil.AlterarDescricao(""));
        }

        [Fact(DisplayName = "Alterar Perfil do Usuario Novo Perfil Deve Alterar")]
        [Trait("Autenticacao", "Domains")]
        public void AlterarPerfilDoUsuario_NovoPerfil_DeveAlterarPerfil()
        {
            // Arrange
            var usuario = _fixture.GerarUsuario();
            var perfil = _fixture.GerarPerfil();

            // Act
            usuario.AlterarPerfil(perfil);

            // Assert
            Assert.Equal(perfil.Id, usuario.PerfilId);
            Assert.Equal(perfil, usuario.Perfil);
        }
    }
}
