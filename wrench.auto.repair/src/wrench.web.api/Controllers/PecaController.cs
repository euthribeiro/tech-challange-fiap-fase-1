using MediatR;
using Microsoft.AspNetCore.Mvc;
using wrench.auto.repair.estoque.application.UseCases.ConsultaPecaPorId;
using wrench.auto.repair.estoque.application.UseCases.ConsultaPecaPorNome;
using wrench.auto.repair.estoque.application.UseCases.ConsultaPecas;
using wrench.auto.repair.estoque.application.UseCases.CriarPeca;
using wrench.auto.repair.estoque.domain.Entities;
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
        /// Cria peça
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Post([FromBody] CriarPecaRequest request)
        {
            var novaPeca = await _mediator.Send((CriarPecaCommand)request);

            return CreatedAtAction(nameof(GetPecaPorId), new {id = novaPeca.Id}, novaPeca );

        }
        
        /// <summary>
        /// Busca peça por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns name="peca"></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Peca),StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPecaPorId([FromRoute] Guid id)
        {
            ConsultarPecaPorIdRequest request = new ConsultarPecaPorIdRequest() { IdPeca = id };
            var peca = await _mediator.Send((ConsultarPecaPorIdCommand)request);
            return Ok(peca);
        }
        
        /// <summary>
        /// Busca peças
        /// </summary>
        /// <param name="nomePeca"></param>
        /// <returns name="peca"></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Peca>),StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] string? nomePeca)
        {
            IEnumerable<Peca> pecas;
            if (string.IsNullOrWhiteSpace(nomePeca))
            {
                pecas = await _mediator.Send(new ConsultaPecasCommand());
                return Ok(pecas);
            }

            var request = new ConsultaPecaPorNomeRequest() { NomePeca = nomePeca };
            pecas = await _mediator.Send((ConsultaPecaPorNomeCommand)request);
            return Ok(pecas);
        }
        
       
    }
}
