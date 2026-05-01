using System.Net.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using wrench.auto.repair.autenticacao.domain.Entities;
using wrench.auto.repair.autenticacao.domain.Security;
using wrench.auto.repair.core.ValueObjects;
using wrench.auto.repair.ordem.servico.application.UseCases.OrdemServicoUseCase;
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

        [Fact]
        public async Task Criar_Ordem_Servico_Com_Sucesso()
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
                "44580535820",
                "Cliente Teste",
                "11999999999",
                "cliente@teste.com",
                enderecoDto
            );

            var clienteResponse = await mediatoR.Send(clienteCommand);
            var clienteId = clienteResponse.Id;

            var veiculoCommand = new wrench.auto.repair.cadastro.application.Commands.CadastrarVeiculoCommand(
                clienteId,
                "Marca Teste",
                "Modelo Teste",
                "Cor Teste",
                2020,
                2021,
                "ABC-1234",
                "Veiculo Teste",
                DateTime.UtcNow,
                10000
            );

            var veiculoResponse = await mediatoR.Send(veiculoCommand);
            var veiculoId = veiculoResponse.Id;

            var command = new CriarOrdemServicoCommand(clienteId, veiculoId, "Teste de criação de ordem de serviço");

            // Act
            var response = await _httpClient.PostAsJsonAsync("/api/v1/ordem-servico", command);

            // Assert
            response.EnsureSuccessStatusCode();
        }
    
        [Fact]
        public async Task Finalizar_Ordem_Servico_Com_Sucesso()
        {
            // Arrange
            using var scope = _integrationTestFactory.Services.CreateScope();
            var mediatoR = scope.ServiceProvider.GetRequiredService<MediatR.IMediator>();

            var enderecoDto = new wrench.auto.repair.cadastro.application.Commands.Dtos.EnderecoDto(
                "Rua Teste Finalizar",
                "123",
                "Apto 1",
                "Bairro Teste",
                "01234-567",
                "São Paulo",
                "SP",
                "Brasil"
            );

            var clienteCommand = new wrench.auto.repair.cadastro.application.Commands.CadastrarClienteCommand(
                "44580535820",
                "Cliente Teste Finalizar",
                "11999999999",
                "cliente.finalizar@teste.com",
                enderecoDto
            );

            var clienteResponse = await mediatoR.Send(clienteCommand);
            var clienteId = clienteResponse.Id;

            var veiculoCommand = new wrench.auto.repair.cadastro.application.Commands.CadastrarVeiculoCommand(
                clienteId,
                "Marca Teste",
                "Modelo Teste",
                "Cor Teste",
                2020,
                2021,
                "XYZ-1234",
                "Veiculo Teste Finalizar",
                DateTime.UtcNow,
                10000
            );

            var veiculoResponse = await mediatoR.Send(veiculoCommand);
            var veiculoId = veiculoResponse.Id;

            var criarCommand = new CriarOrdemServicoCommand(clienteId, veiculoId, "Teste de finalização de ordem de serviço");
            var ordemServicoResponse = await mediatoR.Send(criarCommand);
            var ordemServicoId = ordemServicoResponse.Id;

            // Passar a ordem de serviço pelos fluxos necessários
            var solicitarDiagnosticoCommand = new wrench.auto.repair.ordem.servico.application.UseCases.DiagnosticoUseCase.SolicitarDiagnosticoCommand(ordemServicoId);
            await mediatoR.Send(solicitarDiagnosticoCommand);

            var realizarDiagnosticoCommand = new wrench.auto.repair.ordem.servico.application.UseCases.DiagnosticoUseCase.RealizarDiagnosticoCommand(ordemServicoId, 199.99m, "Solução proposta");
            await mediatoR.Send(realizarDiagnosticoCommand);

            var criarOrcamentoCommand = new wrench.auto.repair.ordem.servico.application.UseCases.OrcamentoUseCase.CriarOrcamentoCommand(ordemServicoId);
            await mediatoR.Send(criarOrcamentoCommand);

            var aprovaOrcamentoCommand = new wrench.auto.repair.ordem.servico.application.UseCases.OrcamentoUseCase.AprovaOrcamentoCommand(ordemServicoId);
            await mediatoR.Send(aprovaOrcamentoCommand);

            var finalizarCommand = new FinalizarOrdemServicoCommand(ordemServicoId);

            // Act
            var response = await _httpClient.PutAsJsonAsync($"/api/v1/diagnostico", finalizarCommand);

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
