using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using wrench.auto.repair.cadastro.application.Queries;
using wrench.auto.repair.cadastro.application.Queries.ViewModels;
using wrench.auto.repair.core.Mediator;
using wrench.auto.repair.core.Pagination;
using wrench.web.api.Extensions;

namespace wrench.web.api.Controllers
{
    /// <summary>
    /// Serviço para criar, atualizar e listar clientes
    /// </summary>
    /// <param name="_mediatorHandler"></param>
    [ApiVersion(1.0)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ClienteController(
        IMediatorHandler _mediatorHandler
    ) : ControllerBase
    {
        /// <summary>
        /// Lista todos os clientes de forma paginada
        /// </summary>
        /// <param name="requisicao"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] RequisicaoPaginada requisicao)
        {
            var obterTodosClientesQuery = new ObterTodosClientesQuery(requisicao);

            var result = await _mediatorHandler
                .EnviarComando<ObterTodosClientesQuery, ResultadoPaginado<ClienteViewModel>>(obterTodosClientesQuery);

            return result.ToActionResult();
        }

        /// <summary>
        /// Busca um determinado cliente pelo seu identificar
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var obterClientePorIdQuery = new ObterClientePorIdQuery(id);

            var result = await _mediatorHandler
                .EnviarComando<ObterClientePorIdQuery, ClienteViewModel>(obterClientePorIdQuery);

            return result.ToActionResult();
        }

        /// <summary>
        /// Busca um determinado cliente pelo seu documento (CPF/CNPJ)
        /// </summary>
        /// <param name="documento"></param>
        /// <returns></returns>
        [HttpGet("{documento}")]
        public async Task<IActionResult> GetByDocument([FromRoute] string documento)
        {
            var obterClientePorDocumentoQuery = new ObterClientePorDocumentoQuery(documento);

            var result = await _mediatorHandler
                .EnviarComando<ObterClientePorDocumentoQuery, ClienteViewModel>(obterClientePorDocumentoQuery);

            return result.ToActionResult();
        }
    }
}
