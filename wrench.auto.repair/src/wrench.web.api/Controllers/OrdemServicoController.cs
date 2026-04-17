using MediatR;
using Microsoft.AspNetCore.Http;
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
    [Route("api/[controller]")]
    [ApiController]
    public class OrdemServicoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdemServicoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Cria ordem de serviço
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CriarOrdemServico")]
        public async Task<IActionResult> Post([FromBody] CriarOrdemServicoRequest request)
        {
            var result = await _mediator.Send((CriarOrdemServicoCommand)request);

            return Ok(result);
        }
    }
}
