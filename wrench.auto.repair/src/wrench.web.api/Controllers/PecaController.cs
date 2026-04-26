using MediatR;
using Microsoft.AspNetCore.Mvc;
using wrench.auto.repair.estoque.application.UseCases.CriarPeca;
using wrench.web.api.Models.Requests;

namespace wrench.web.api.Controllers
{
    /// <summary>
    /// Serviço para o contexto de estoque, 
    /// responsável por expor os endpoints relacionados a criação, consulta, 
    /// atualização e exclusão peças.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PecaController: ControllerBase
    {
        private IMediator _mediator;

        public PecaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Cria ordem de serviço
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CriarPecaRequest request)
        {
            var result = await _mediator.Send((CriarPecaCommand)request);

            return Ok(result);

        }
    }
}
