using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using wrench.auto.repair.core.Mediator;
using wrench.auto.repair.ordem.servico.application.Queries;
using wrench.auto.repair.ordem.servico.application.Queries.ViewModels;
using wrench.auto.repair.ordem.servico.application.UseCases.OrdemServicoUseCase;
using wrench.auto.repair.ordem.servico.domain.Entities;
using wrench.web.api.Extensions;
using wrench.web.api.Models.OrdemServico;

namespace wrench.web.api.Controllers
{
    /// <summary>
    /// Serviço para criar, atualizar e listar ordem de serviço
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

        /// <summary>
        /// Consultar detalher de uma ordem de serviço
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Guid id)
        {
            var result = await _mediatorHandler
                .EnviarComando<ObterOrdemServicoIdQuery, OrdemServicoViewModel>(new ObterOrdemServicoIdQuery(id));

            return result.ToActionResult();
        }
    }
}
