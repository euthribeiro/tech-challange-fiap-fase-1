using wrench.auto.repair.core.DomainObjects;
using wrench.auto.repair.core.ValueObjects;

namespace wrench.auto.repair.core.tests.ValueObjects
{
    public class NomeCompletoTests
    {
        [Trait(name: "Core", value: "ValueObjects")]
        [Fact(DisplayName = "Criar Nome Completo - Primeiro Nome e Sobrenome Vazio - Deve Retornar Domain Exception")]
        public void CriarNomeCompleto_PrimeiroNomeESobrenomeVazio_DeveRetornarDomainException()
        {
            // Arrange & Act & Assert
            Assert.Throws<DomainException>(() => new NomeCompleto("", ""));
            var exception = Assert.Throws<DomainException>(() => new NomeCompleto("", "Ribeiro"));
            var exception2 = Assert.Throws<DomainException>(() => new NomeCompleto("Thiago", ""));

            Assert.Equal("O nome não pode estar vazio", exception.Message);
            Assert.Equal("O sobrenomes não pode estar vazio", exception2.Message);
        }

        [Trait(name: "Core", value: "ValueObjects")]
        [Fact(DisplayName = "Criar Nome Completo - Com Numeros e Símbolos - Deve Retornar Domain Exception")]
        public void CriarNomeCompleto_ComNumerosESimbolos_DeveRetornarDomainException()
        {
            // Arrange & Act & Assert
            var exception = Assert.Throws<DomainException>(() => new NomeCompleto("Jose3", "S*ilva"));
            var exception2 = Assert.Throws<DomainException>(() => new NomeCompleto("Jose", "S*ilva"));

            Assert.Equal("Somente letras são permitidas", exception.Message);
            Assert.Equal("Somente letras são permitidas", exception2.Message);
        }

        [Trait(name: "Core", value: "ValueObjects")]
        [Fact(DisplayName = "Nome e Sobrenomes informados - Deve Retornar Nome Completo")]
        public void ObterNomeCompleto_NomeESobrenomeInformados_DeveRetornarNomeCompleto()
        {
            // Arrange
            var nomeCompletoObjeto = new NomeCompleto("Jose", "Antonio da Silva");

            // Act
            var nomeCompleto = nomeCompletoObjeto.ObterNomeCompleto();

            // Assert
            Assert.Equal("Jose Antonio da Silva", nomeCompleto);
        }
    }
}
