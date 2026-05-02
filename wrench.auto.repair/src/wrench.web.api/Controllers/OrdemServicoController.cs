using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using wrench.auto.repair.core.Mediator;
using wrench.auto.repair.ordem.servico.application.UseCases.OrdemServicoUseCase;
using wrench.web.api.Extensions;
using wrench.web.api.Models.OrdemServico;

namespace wrench.web.api.Controllers
{
    /// <summary>
    /// Serviço para o contexto de ordem de serviço, 
    /// responsável por expor os endpoints relacionados a criação, consulta, 
    /// atualização e exclusão de ordens de serviço.
    /// </summary>
    [ApiVersion(1.0)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Funcionario")]
    public class OrdemServicoController(IMediatorHandler _mediatorHandler) : ControllerBase
    {
        /// <summary>
        /// Cria ordem de serviço
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CriarOrdemServicoRequest request)
        {
            var result = await _mediatorHandler
                .EnviarComando<CriarOrdemServicoCommand, Guid>((CriarOrdemServicoCommand)request);

            return result.ToActionResult();
        }

        /// <summary>
        /// Cria ordem de serviço
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] AtualizarOrdemServicoRequest request)
        {
            var result = await _mediatorHandler
                .EnviarComando<FinalizarOrdemServicoCommand, Guid>((FinalizarOrdemServicoCommand)request);

            return result.ToActionResult();
        }
    }
}
