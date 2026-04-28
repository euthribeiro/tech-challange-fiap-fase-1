using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using wrench.auto.repair.core.Errors;
using wrench.auto.repair.core.Mediator;
using wrench.auto.repair.ordem.servico.application.UseCases.DiagnosticoUseCase;
using wrench.auto.repair.ordem.servico.application.UseCases.Os;
using wrench.web.api.Extensions;

namespace wrench.web.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class DiagnosticoController : ControllerBase
    {
        private readonly IMediatorHandler _mediatorHandler;

        public DiagnosticoController(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] SolicitarDiagnosticoCommand command)
        {
            if (command == null) return BadRequest();

            var result = await _mediatorHandler.EnviarComando<SolicitarDiagnosticoCommand, Guid>(command);

            return result.ToActionResult();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RealizarDiagnosticoCommand command)
        {
            if (command == null) return BadRequest();

            var result = await _mediatorHandler.EnviarComando<RealizarDiagnosticoCommand, Guid>(command);

            return result.ToActionResult();
        }
    }
}
