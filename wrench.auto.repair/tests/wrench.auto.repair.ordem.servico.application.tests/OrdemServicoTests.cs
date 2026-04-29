using Moq;
using wrench.auto.repair.ordem.servico.application.UseCases.OrdemServicoUseCase;
using wrench.auto.repair.ordem.servico.domain.Entities;
using wrench.auto.repair.ordem.servico.domain.Interfaces.Repositories;

namespace wrench.auto.repair.ordem.servico.application.tests
{
    public class OrdemServicoTests
    {
        private readonly Mock<IOrdemServicoRepository> _repositoryMock;
        private readonly Mock<IDiagnosticoRepository> _diagnosticoRepositoryMock;
        private readonly OrdemServicoCommandHandler _handler;

        public OrdemServicoTests()
        {
            _repositoryMock = new Mock<IOrdemServicoRepository>();
            _diagnosticoRepositoryMock = new Mock<IDiagnosticoRepository>();

            _handler = new OrdemServicoCommandHandler(_repositoryMock.Object, _diagnosticoRepositoryMock.Object);
        }

        [Fact]
        public async Task CriarOrdemServico_DeveCriarEAdicionarOrdemServico_QuandoCommandValido()
        {
            // Arrange
            var command = new CriarOrdemServicoCommand(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), "Cliente falou que o carro tá com barulho na roda.");

            // Setup de comportamentos do mock (se necessário)
            _repositoryMock.Setup(r => r.IncluirAsync(It.IsAny<OrdemServico>()))
                           .Returns(Task.CompletedTask);

            // Act
            var resultado = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(resultado); 
            Assert.True(resultado.Sucesso);
            _repositoryMock.Verify(r => r.IncluirAsync(It.IsAny<OrdemServico>()), Times.Once);
        }

        [Fact]
        public async Task CriarOrdemServico_DeveRetornarErro_QuandoDadosForemInvalidos()
        {
            // Arrange
            var command = new CriarOrdemServicoCommand(default, default, default, String.Empty);

            // Act
            var resultado = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(resultado.Sucesso);
        }
    }
}
