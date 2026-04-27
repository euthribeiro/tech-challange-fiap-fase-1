using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using wrench.auto.repair.autenticacao.application.Commands;
using wrench.auto.repair.autenticacao.application.Commands.ViewModels;
using wrench.auto.repair.autenticacao.application.Queries;
using wrench.auto.repair.autenticacao.application.Queries.ViewModels;
using wrench.auto.repair.core.Mediator;
using wrench.auto.repair.core.Pagination;
using wrench.web.api.Extensions;

namespace wrench.web.api.Controllers
{
    /// <summary>
    /// Serviço para criação de usuários
    /// </summary>
    /// <param name="_mediatorHandler"></param>
    [ApiVersion(1.0)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class UsuarioController(IMediatorHandler _mediatorHandler) : ControllerBase
    {
        /// <summary>
        /// Criar um novo usuário
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] CriarUsuarioViewModel request)
        {
            var command = new CriarUsuarioCommand(request.Email, request.Senha, request.PerfilId, request.Ativo);

            var result = await _mediatorHandler.EnviarComando<CriarUsuarioCommand, Guid>(command);

            return result.ToActionResult();
        }

        /// <summary>
        /// Lista todos os usuários cadastrados
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] RequisicaoPaginada request)
        {
            var obterTodosUsuariosQuery = new ObterTodosUsuariosQuery(request);

            var result = await _mediatorHandler
                .EnviarComando<ObterTodosUsuariosQuery, ResultadoPaginado<UsuarioViewModel>>(obterTodosUsuariosQuery);

            return result.ToActionResult();
        }

        /// <summary>
        /// Busca usuário pelo id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var obterUsuarioPorIdQuery = new ObterUsuarioPorIdQuery(id);

            var result = await _mediatorHandler
                .EnviarComando<ObterUsuarioPorIdQuery, UsuarioViewModel>(obterUsuarioPorIdQuery);

            return result.ToActionResult();
        }
    }
}
