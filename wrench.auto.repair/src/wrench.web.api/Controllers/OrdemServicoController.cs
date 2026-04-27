using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using wrench.auto.repair.ordem.servico.application.UseCases.CriarOrdemServico;
using wrench.web.api.Models.Requests;

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
    [Authorize]
    public class OrdemServicoController(IMediator _mediator) : ControllerBase
    {
        /// <summary>
        /// Cria ordem de serviço
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CriarOrdemServicoRequest request)
        {
            var result = await _mediator.Send((CriarOrdemServicoCommand)request);

            return Ok(result);
        }
    }
}
