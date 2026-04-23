using Microsoft.AspNetCore.Mvc;
using wrench.auto.repair.autenticacao.application.Commands;
using wrench.auto.repair.autenticacao.application.Models.ViewModels;
using wrench.auto.repair.core.Mediator;
using wrench.web.api.Extensions;

namespace wrench.web.api.Controllers
{
    /// <summary>
    /// Serviço para criação de usuários
    /// </summary>
    /// <param name="_mediatorHandler"></param>
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController(IMediatorHandler _mediatorHandler) : ControllerBase
    {
        /// <summary>
        /// Criar um novo usuário
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CriarUsuarioViewModel request)
        {
            var command = new CriarUsuarioCommand(request.Email, request.Senha, request.PerfilId, request.Ativo);

            var result = await _mediatorHandler.EnviarComando(command);

            return result.ToActionResult();
        }
    }
}
