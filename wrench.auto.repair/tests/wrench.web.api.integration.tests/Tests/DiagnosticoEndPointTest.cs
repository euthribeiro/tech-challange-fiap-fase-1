using Microsoft.Extensions.DependencyInjection;
using System.Text;
using wrench.auto.repair.autenticacao.domain.Entities;
using wrench.auto.repair.autenticacao.domain.Security;
using wrench.auto.repair.core.ValueObjects;
using wrench.auto.repair.ordem.servico.application.UseCases.DiagnosticoUseCase;
using wrench.auto.repair.ordem.servico.application.UseCases.OrdemServicoUseCase;
using wrench.web.api.integration.tests.Base;
using wrench.web.api.Models.Diagnostico;

namespace wrench.web.api.integration.tests.Tests
{
    [Collection("SharedContainer")]
    public class DiagnosticoEndPointTest
    {
        private readonly HttpClient _httpClient;
        private readonly IntegrationTestFactory _integrationTestFactory;

        public DiagnosticoEndPointTest(IntegrationTestFactory integrationTestFactory)
        {
            _integrationTestFactory = integrationTestFactory;
            _httpClient = _integrationTestFactory.CreateClient();

            // Configurar autenticação JWT para o HttpClient
            using var scope = _integrationTestFactory.Services.CreateScope();
            var jwtGenerator = scope.ServiceProvider.GetRequiredService<IJwtTokenGenerator>();

            // Necessário criar um usuário válido conforme a regra de negócio/domain
            var email = new Email("teste@teste.com");
            var perfil = new Perfil("Admin", "Administrador", true, DateTime.UtcNow);

            var usuario = new Usuario(email, perfil.Id, true, DateTime.UtcNow);
            // Associar perfil manualmente para o token generator conseguir ler as claims de perfil
            typeof(Usuario).GetProperty("Perfil")?.SetValue(usuario, perfil, null);

            var token = jwtGenerator.GerarToken(usuario);
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.Token);
        }

        [Fact(DisplayName = "Realizar Diagnóstico Com Sucesso")]
        [Trait("Integration", "WebApi")]
        public async Task Realizar_Diagnostico_Com_Sucesso()
        {
            // Arrange
            using var scope = _integrationTestFactory.Services.CreateScope();
            var mediatoR = scope.ServiceProvider.GetRequiredService<MediatR.IMediator>();

            var enderecoDto = new wrench.auto.repair.cadastro.application.Commands.Dtos.EnderecoDto(
                "Rua Teste",
                "123",
                "Apto 1",
                "Bairro Teste",
                "01234-567",
                "São Paulo",
                "SP",
                "Brasil"
            );

            var clienteCommand = new wrench.auto.repair.cadastro.application.Commands.CadastrarClienteCommand(
                "92761932005",
                "Cliente Teste",
                "11999999999",
                "cliente@teste1.com",
                enderecoDto
            );

            var clienteResponse = await mediatoR.Send(clienteCommand);
            var clienteId = clienteResponse.Valor;

            var veiculoCommand = new wrench.auto.repair.cadastro.application.Commands.CadastrarVeiculoCommand(
                clienteId,
                "Marca Teste",
                "Modelo Teste",
                "Cor Teste",
                2020,
                2021,
                "ABC-1235",
                "Veiculo Teste",
                DateTime.UtcNow,
                10000
            );

            var veiculoResponse = await mediatoR.Send(veiculoCommand);
            var veiculoId = veiculoResponse.Valor;

            var ordemServicoCommand = new CriarOrdemServicoCommand(clienteId, veiculoId, "Problema no sistema de freios");

            var ordemServicoResponse = await mediatoR.Send(ordemServicoCommand);
            var ordemServicoId = ordemServicoResponse.Valor;

            var solicitaDiagnostico = new SolicitarDiagnosticoCommand(ordemServicoId);
            var diagnosticoResponse = await mediatoR.Send(solicitaDiagnostico);

            var request = new RealizarDiagnosticoRequest
            {
                OrdemServicoId = ordemServicoId,
                SolucaoProposta = "Substituição das pastilhas de freio",
                ValorEstimado = 500.00m
            };

            var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PostAsync("/api/v1/diagnostico", content);

            var resultString = await response.Content.ReadAsStringAsync();
            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(System.Net.HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}
