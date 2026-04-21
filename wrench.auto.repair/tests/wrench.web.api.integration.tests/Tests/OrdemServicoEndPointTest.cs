using FluentAssertions;
using System.Net.Http.Json;
using wrench.auto.repair.ordem.servico.application.UseCases.CriarOrdemServico;
using wrench.web.api.integration.tests.Base;

namespace wrench.web.api.integration.tests.Tests
{
    [Collection("SharedContainer")]
    public class OrdemServicoEndPointTest
    {
        private readonly HttpClient _httpClient;
        private readonly IntegrationTestFactory _integrationTestFactory;

        public OrdemServicoEndPointTest(IntegrationTestFactory integrationTestFactory)
        {
            _integrationTestFactory = integrationTestFactory;
            _httpClient = _integrationTestFactory.CreateClient();
        }

        [Fact]
        public async Task Should_Create_Ordem_Servico_Witch_Success()
        {
            // Arrange
            var command = new CriarOrdemServicoCommand()
            {
                ClienteId = Guid.NewGuid(),
                VeiculoId = Guid.NewGuid(),
                Descricao = "Teste de criação de ordem de serviço",
                DataCriacao = DateTime.UtcNow
            };

            // Act
            var response = await _httpClient.PostAsJsonAsync("/api/ordem-servico", command);

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
