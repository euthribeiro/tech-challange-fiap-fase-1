using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using wrench.auto.repair.core.Mediator;
using wrench.auto.repair.ordem.servico.application.UseCases.OrcamentoUseCase;
using wrench.web.api.Extensions;
using wrench.web.api.Models.OrdemServico;

namespace wrench.web.api.Controllers
{
    /// <summary>
    /// Serviço para aprovar e listar orçamentos
    /// </summary>
    [ApiVersion(1.0)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Funcionario")]
    public class OrcamentoController(IMediatorHandler _mediatorHandler) : ControllerBase
    {
        /// <summary>
        /// Aprovar orçamento
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AprovarOrcamentoRequest request)
        {
            var aprovarOrcamentoCommand = new AprovaOrcamentoCommand(request.OrdemServicoId);

            var result = await _mediatorHandler
                .EnviarComando<AprovaOrcamentoCommand, bool>(aprovarOrcamentoCommand);

            return result.ToActionResult();
        }
    }
}
