using wrench.auto.repair.cadastro.domain.tests.Fixtures;
using wrench.auto.repair.cadastro.domain.ValueObjects;
using wrench.auto.repair.core.DomainObjects;

namespace wrench.auto.repair.cadastro.domain.tests
{
    [Collection(nameof(ClienteCollection))]
    public class EnderecoTests(ClienteFixture _fixture)
    {

        [Fact(DisplayName = "Endereço Validações Devem Retornar Exception")]
        [Trait("Cadastro", "ValueObjects")]
        public void Endereco_Validar_ValidacoesDevemRetornarException()
        {
            // Arrange & Act & Assert

            Assert.Throws<DomainException>(() => new Endereco(string.Empty, "0", string.Empty, "Meu Bairro", "123456000", "São Paulo", "SP", "Brasil"));
            Assert.Throws<DomainException>(() => new Endereco("Minha Rua", "", string.Empty, "Meu Bairro", "123456000", "São Paulo", "SP", "Brasil"));
            Assert.Throws<DomainException>(() => new Endereco("Minha Rua", "0", string.Empty, "", "123456000", "São Paulo", "SP", "Brasil"));
            Assert.Throws<DomainException>(() => new Endereco("Minha Rua", "0", string.Empty, "Meu Bairro", "", "São Paulo", "SP", "Brasil"));
            Assert.Throws<DomainException>(() => new Endereco("Minha Rua", "0", string.Empty, "Meu Bairro", "123", "São Paulo", "SP", "Brasil"));
            Assert.Throws<DomainException>(() => new Endereco("Minha Rua", "0", string.Empty, "Meu Bairro", "123456000", "", "SP", "Brasil"));
            Assert.Throws<DomainException>(() => new Endereco("Minha Rua", "0", string.Empty, "Meu Bairro", "123456000", "São Paulo", "", "Brasil"));
            Assert.Throws<DomainException>(() => new Endereco("Minha Rua", "0", string.Empty, "Meu Bairro", "123456000", "São Paulo", "SP", ""));
        }

        [Fact(DisplayName = "Criar Endereço Válido Deve Retornar Endereço Formatado")]
        [Trait("Cadastro", "Domains")]
        public void CriarEndereco_EnderecoValido_DeveRetornarEnderecoFormatado()
        {
            var endereco = _fixture.GerarEnderecoValido();

            Assert.NotEmpty(endereco.ObterEnderecoFormatado());
        }
    }
}
