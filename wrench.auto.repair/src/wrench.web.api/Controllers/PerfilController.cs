using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using wrench.auto.repair.autenticacao.application.Queries;
using wrench.auto.repair.autenticacao.application.Queries.ViewModels;
using wrench.auto.repair.core.Mediator;
using wrench.web.api.Extensions;

namespace wrench.web.api.Controllers
{
    /// <summary>
    /// Serviço para listagem de Perfis
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PerfilController(IMediatorHandler _mediatorHandler) : ControllerBase
    {
        /// <summary>
        /// Obter todos os usuário
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            var result = await _mediatorHandler
                .EnviarComando<ObterTodosPerfisQuery, IEnumerable<PerfilViewModel>>(
                    new ObterTodosPerfisQuery()
                );

            return result.ToActionResult();
        }
    }
}
