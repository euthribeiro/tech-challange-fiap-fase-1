using MediatR;
using wrench.auto.repair.estoque.domain.Enums;
using wrench.auto.repair.estoque.domain.Interfaces.Repositories;

namespace wrench.auto.repair.estoque.application.UseCases.EntradaPeca;

public class EntradaPecaHandler(IPecaRepository pecaRepository) : IRequestHandler<EntradaPecaCommand, double>
{
    public Task<double> Handle(EntradaPecaCommand request, CancellationToken cancellationToken)
    {
      var quantidadeAtualizada = pecaRepository.MovimentaEstoque(request.PecaId, TipoMovimentacao.Entrada, request.Quantidade);
      return Task.FromResult(quantidadeAtualizada);
    }
}