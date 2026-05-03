using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http;
using System.Text;
using wrench.auto.repair.autenticacao.domain.Entities;
using wrench.auto.repair.autenticacao.domain.Security;
using wrench.auto.repair.core.ValueObjects;
using wrench.auto.repair.estoque.application.Commands;
using wrench.auto.repair.ordem.servico.application.UseCases.DiagnosticoUseCase;
using wrench.auto.repair.ordem.servico.application.UseCases.OrdemServicoUseCase;
using wrench.web.api.integration.tests.Base;
using wrench.web.api.Models.OrdemServico;

namespace wrench.web.api.integration.tests.Tests
{
    [Collection("SharedContainer")]
    public class OrcamentoEndPointTest
    {
        private readonly HttpClient _httpClient;
        private readonly IntegrationTestFactory _integrationTestFactory;

        public OrcamentoEndPointTest(IntegrationTestFactory integrationTestFactory)
        {
            _integrationTestFactory = integrationTestFactory;
            _httpClient = _integrationTestFactory.CreateClient();

            // Configurar autenticação JWT para o HttpClient
            using var scope = _integrationTestFactory.Services.CreateScope();
            var jwtGenerator = scope.ServiceProvider.GetRequiredService<IJwtTokenGenerator>();

            // Necessário criar um usuário válido conforme a regra de negócio/domain
            var email = new Email("teste@teste.com");
            var perfil = new Perfil("Cliente", "Cliente do sistema", true, DateTime.UtcNow);

            var usuario = new Usuario(email, perfil.Id, true, DateTime.UtcNow);
            // Associar perfil manualmente para o token generator conseguir ler as claims de perfil
            typeof(Usuario).GetProperty("Perfil")?.SetValue(usuario, perfil, null);

            var token = jwtGenerator.GerarToken(usuario);
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.Token);
        }

        [Fact(DisplayName = "Aprovar orçamento com sucesso")]
        [Trait("Integration", "WebApi")]
        public async Task Aprovar_Orcamento_Com_Sucesso()
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
                "83318154083",
                "Cliente Teste",
                "11999999998",
                "clienteTeste@teste2.com",
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
                "ABC-1237",
                "Veiculo Teste",
                DateTime.UtcNow,
                10000
            );

            var veiculoResponse = await mediatoR.Send(veiculoCommand);
            var veiculoId = veiculoResponse.Valor;

            var ordemServicoCommand = new CriarOrdemServicoCommand(clienteId, veiculoId, "Problema no sistema de freios");

            var ordemServicoResponse = await mediatoR.Send(ordemServicoCommand);
            var ordemServicoId = ordemServicoResponse.Valor;

            var nomePecaUnico = $"PASTILHA FREIO ORC INT {Guid.NewGuid():N}";
            var cadastrarPeca = new CadastrarPecaCommand(
                nomePecaUnico,
                "Pastilha para sistema de freios — teste integração orçamento",
                149.90,
                20,
                true);
            var pecaResponse = await mediatoR.Send(cadastrarPeca);
            var pecaId = pecaResponse.Valor;

            var solicitaDiagnostico = new SolicitarDiagnosticoCommand(ordemServicoId);
            var diagnosticoResponse = await mediatoR.Send(solicitaDiagnostico);

            var pecasIds = new HashSet<Guid> { pecaId };
            var registrarDiagnostico = new RealizarDiagnosticoCommand(ordemServicoId, 500.00m, "Problema identificado", pecasIds);
            var registrarDiagnosticoResponse = await mediatoR.Send(registrarDiagnostico);

            var response = await _httpClient.PutAsync(
                $"/api/v1/orcamento/{ordemServicoId}/aprovar",
                new StringContent(string.Empty, Encoding.UTF8, "application/json"));

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}
