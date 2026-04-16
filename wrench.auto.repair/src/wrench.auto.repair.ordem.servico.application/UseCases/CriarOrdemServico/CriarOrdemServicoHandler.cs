using MediatR;
using wrench.auto.repair.ordem.servico.domain.Entities;
using wrench.auto.repair.ordem.servico.domain.Enums;
using wrench.auto.repair.ordem.servico.domain.Interfaces.Repositories;

namespace wrench.auto.repair.ordem.servico.application.UseCases.CriarOrdemServico
{
    public class CriarOrdemServicoHandler : IRequestHandler<CriarOrdemServicoCommand, Guid>
    {
        private readonly IOrdemServicoRepository _repository;

        public CriarOrdemServicoHandler(IOrdemServicoRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(CriarOrdemServicoCommand request, CancellationToken cancellationToken)
        {
            var ordemServico = new OrdemServico(
                request.ClienteId,
                request.VeiculoId,
                request.Descricao,
                OrdemServicoStatus.Recebida
            );

            await _repository.IncluirAsync(ordemServico);

            return ordemServico.Id;
        }
    }
}
