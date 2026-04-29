using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using wrench.auto.repair.core.Mediator;
using wrench.auto.repair.ordem.servico.application.UseCases.DiagnosticoUseCase;
using wrench.web.api.Extensions;
using wrench.web.api.Models.Diagnostico;

namespace wrench.web.api.Controllers
{
    [ApiVersion(1.0)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Funcionario")]
    public class DiagnosticoController : ControllerBase
    {
        private readonly IMediatorHandler _mediatorHandler;

        public DiagnosticoController(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] SolicitarDiagnosticoRequest request)
        {
            var command = new SolicitarDiagnosticoCommand(request.OrdemServicoId);

            var result = await _mediatorHandler
                .EnviarComando<SolicitarDiagnosticoCommand>(command);

            return result.ToActionResult();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RealizarDiagnosticoRequest request)
        {
            var result = await _mediatorHandler
                .EnviarComando<RealizarDiagnosticoCommand, Guid>((RealizarDiagnosticoCommand)request);

            return result.ToActionResult();
        }
    }
}
