using wrench.auto.repair.core.DomainObjects;
using wrench.auto.repair.estoque.domain.Entities;

namespace wrench.auto.repair.estoque.domain.tests
{
    public class PecaTests
    {
        [Fact(DisplayName = "Criar Peça Dados Inválidos")]
        [Trait("Estoque", "Domains")]
        public void CriarPecas_DadosInvalidos_DeveRetornarException()
        {
            // Arrange, Act & Assert
            Assert.Throws<DomainException>(() => new Peca("", "Aro 15", 150, 10, true, DateTime.UtcNow));
            Assert.Throws<DomainException>(() => new Peca("Pneu", "", 150, 10, true, DateTime.UtcNow));
            Assert.Throws<DomainException>(() => new Peca("Pneu", "Aro 15", -1, 10, true, DateTime.UtcNow));
            Assert.Throws<DomainException>(() => new Peca("Pneu", "Aro 15", 150, -1, true, DateTime.UtcNow));
        }

        [Fact(DisplayName = "Criar Peça Dados Válidos")]
        [Trait("Estoque", "Domains")]
        public void CriarPecas_DadosValido_NehumaExceptionDeveSerRetornada()
        {
            // Arrange & Act
            var peca = new Peca("Pneu", "Aro 15", 150, 10, true, DateTime.UtcNow);

            //Assert
            Assert.NotNull(peca);
        }

        [Fact(DisplayName = "Criar Peça Alterar Descrição Para Vazio")]
        [Trait("Estoque", "Domains")]
        public void CriarPecas_AlterarDescricaoParaVazio_DeveRetornarException()
        {

            // Arrange
            var peca = new Peca("Pneu", "Aro 15", 150, 10, true, DateTime.UtcNow);

            // Act
            Assert.Throws<DomainException>(() => peca.AlterarDescricao(""));
        }

        [Fact(DisplayName = "Criar Peça Alterar Descrição Válida")]
        [Trait("Estoque", "Domains")]
        public void CriarPecas_AlterarDescricaoValida_NehumaExceptionDeveSerRetornada()
        {
            // Arrange & Act
            var novaDescricao = "Aro 14";
            var peca = new Peca("Pneu", "Aro 15", 150, 10, true, DateTime.UtcNow);

            // Act
            peca.AlterarDescricao(novaDescricao);

            //Assert
            Assert.Equal(novaDescricao, peca.Descricao, ignoreCase: true);
        }

        [Fact(DisplayName = "Criar Peça Alterar Valor Inválido")]
        [Trait("Estoque", "Domains")]
        public void CriarPecas_AlterarValorInvalido_DeveRetornarException()
        {

            // Arrange
            var peca = new Peca("Pneu", "Aro 15", 150, 10, true, DateTime.UtcNow);

            // Act
            Assert.Throws<DomainException>(() => peca.AlterarValor(-1));
        }

        [Fact(DisplayName = "Criar Peça Alterar Valor Válido")]
        [Trait("Estoque", "Domains")]
        public void CriarPecas_AlterarValorValido_NehumaExceptionDeveSerRetornada()
        {
            // Arrange & Act
            var novoValor = 160.00;
            var peca = new Peca("Pneu", "Aro 15", 150, 10, true, DateTime.UtcNow);

            // Act
            peca.AlterarValor(novoValor);

            //Assert
            Assert.Equal(novoValor, peca.Valor);
        }

        [Fact(DisplayName = "Criar Peça Repor Estoque Quantidade Inválida")]
        [Trait("Estoque", "Domains")]
        public void CriarPecas_ReporEstoqueComQuantidadeInvalida_DeveRetornarException()
        {

            // Arrange
            var peca = new Peca("Pneu", "Aro 15", 150, 10, true, DateTime.UtcNow);

            // Act
            Assert.Throws<DomainException>(() => peca.ReporEstoque(-5));
        }

        [Fact(DisplayName = "Criar Peça Repor Estoque Quantidade Válida")]
        [Trait("Estoque", "Domains")]
        public void CriarPecas_ReporEstoqueComQuantidadeInvalida_NehumaExceptionDeveSerRetornada()
        {
            // Arrange & Act
            var peca = new Peca("Pneu", "Aro 15", 150, 10, true, DateTime.UtcNow);

            // Act
            peca.ReporEstoque(5);

            //Assert
            Assert.Equal(15, peca.Quantidade);
        }

        [Fact(DisplayName = "Criar Peça Baixar Estoque Quantidade Inválida")]
        [Trait("Estoque", "Domains")]
        public void CriarPecas_BaixarEstoqueComQuantidadeInvalida_DeveRetornarException()
        {

            // Arrange
            var peca = new Peca("Pneu", "Aro 15", 150, 10, true, DateTime.UtcNow);

            // Act
            Assert.Throws<DomainException>(() => peca.BaixarEstoque(-5));
        }

        [Fact(DisplayName = "Criar Peça Baixar Estoque Quantidade Válida")]
        [Trait("Estoque", "Domains")]
        public void CriarPecas_BaixarEstoqueComQuantidadeInvalida_NehumaExceptionDeveSerRetornada()
        {
            // Arrange & Act
            var peca = new Peca("Pneu", "Aro 15", 150, 10, true, DateTime.UtcNow);

            // Act
            peca.BaixarEstoque(5);

            //Assert
            Assert.Equal(5, peca.Quantidade);
        }
    }
}
