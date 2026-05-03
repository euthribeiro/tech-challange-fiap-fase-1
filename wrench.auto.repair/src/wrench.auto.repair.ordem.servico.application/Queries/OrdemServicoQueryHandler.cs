using MediatR;
using wrench.auto.repair.core.Errors;
using wrench.auto.repair.ordem.servico.application.Queries.ViewModels;
using wrench.auto.repair.ordem.servico.domain.Data;

namespace wrench.auto.repair.ordem.servico.application.Queries
{
    public class OrdemServicoQueryHandler(IOrdemServicoRepository _ordemServicoRepository) : IRequestHandler<ObterOrdemServicoIdQuery, Result<OrdemServicoViewModel>>
    {
        public async Task<Result<OrdemServicoViewModel>> Handle(ObterOrdemServicoIdQuery request, CancellationToken cancellationToken)
        {
            if(!request.EhValido())
                return Result<OrdemServicoViewModel>.NotFound(request.ObterErros());

            var ordemServico = await _ordemServicoRepository.ObterPorIdAsync(request.Id, cancellationToken);

            if(ordemServico == null)
                return Result<OrdemServicoViewModel>.NotFound("Ordem de serviço não encontrada");

            return Result<OrdemServicoViewModel>.Ok(ordemServico);
        }
    }
}
