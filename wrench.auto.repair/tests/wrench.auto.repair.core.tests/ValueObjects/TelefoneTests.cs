using wrench.auto.repair.core.DomainObjects;
using wrench.auto.repair.core.ValueObjects;

namespace wrench.auto.repair.core.tests.ValueObjects
{
    public class TelefoneTests
    {
        [Fact(DisplayName = "Criar Telefone Vazio Deve Retornar Exception")]
        [Trait("Core", "ValueObjects")]
        public void CriarTelefone_TelefoneVazio_DeveRetornarException()
        {
            // Arrange & Act & Assert
            Assert.Throws<DomainException>(() => new Telefone("", "11", "123456789"));
            Assert.Throws<DomainException>(() => new Telefone("55", "", "123456789"));
            Assert.Throws<DomainException>(() => new Telefone("55", "11", ""));
            Assert.Throws<DomainException>(() => new Telefone("", "", ""));
        }

        [Fact(DisplayName = "Criar Telefone Com Letras e Simbolos Deve Retornar Exception")]
        [Trait("Core", "ValueObjects")]
        public void CriarTelefone_TelefoneComLetrasESimbolos_DeveRetornarException()
        {
            // Arrange & Act & Assert
            Assert.Throws<DomainException>(() => new Telefone("+55", "#1", "12345678#"));
        }

        [Fact(DisplayName = "Criar Telefone Valido")]
        [Trait("Core", "ValueObjects")]
        public void CriarTelefone_TelefoneValido_DeveObterTelefoneCompleto()
        {
            // Arrange
            var ddi = "55";
            var ddd = "11";
            var numero = "997978989";

            // Act
            var telefone = new Telefone(ddi, ddd, numero);

            // Assert
            Assert.Equal($"+{ddi}{ddd}{numero}", telefone.ObterTelefone());
        }

        [Fact(DisplayName = "Criar Telefone Sem DDI Deve Completar com DDI Brasileiro")]
        [Trait("Core", "ValueObjects")]
        public void CriarTelefone_TelefoneSemDDI_DevePreencherComDDIBrasileiro()
        {
            // Arrange
            var ddd = "11";
            var numero = "997978989";

            // Act
            var telefone = new Telefone(ddd, numero);

            // Assert
            Assert.Equal($"+55{ddd}{numero}", telefone.ObterTelefone());
        }

        [Fact(DisplayName = "Criar Telefone Estrangeiro Deve Retornar Exception")]
        [Trait("Core", "ValueObjects")]
        public void CriarTelefone_TelefoneEstrangeiro_DeveRetornarException()
        {
            // Arrange
            var telefone = "+1 212 555 1234";

            // Act & Assert
            Assert.Throws<DomainException>(() => new Telefone(telefone));
        }

        [Fact(DisplayName = "Criar Telefone Brasileiro Deve Retornar Telefone Completo")]
        [Trait("Core", "ValueObjects")]
        public void CriarTelefone_TelefoneBrasileiro_DeveRetornarTelefoneCompleto()
        {
            // Arrange
            var numeroDeTelefone = "+5511981811515";

            // Act
            var telefone = new Telefone(numeroDeTelefone);

            // Assert
            Assert.Equal(numeroDeTelefone, telefone.ObterTelefone());
        }
    }
}
