using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using wrench.auto.repair.core.Mediator;
using wrench.auto.repair.ordem.servico.application.UseCases.Os;
using wrench.web.api.Extensions;

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
    public class OrdemServicoController(IMediator _mediator) : ControllerBase
    {
        /// <summary>
        /// Cria ordem de serviço
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CriarOrdemServicoCommand request)
        {
            var result = await _mediatorHandler.EnviarComando<CriarOrdemServicoCommand, Guid>(request);
            
            return result.ToActionResult();
        }
    }
}
