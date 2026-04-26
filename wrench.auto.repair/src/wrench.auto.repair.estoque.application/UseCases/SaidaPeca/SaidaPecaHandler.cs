using MediatR;
using wrench.auto.repair.estoque.domain.Enums;
using wrench.auto.repair.estoque.domain.Interfaces.Repositories;

namespace wrench.auto.repair.estoque.application.UseCases.EntradaPeca;

public class SaidaPecaHandler(IPecaRepository pecaRepository) : IRequestHandler<SaidaPecaCommand, double>
{
    public Task<double> Handle(SaidaPecaCommand request, CancellationToken cancellationToken)
    {
      var quantidadeAtualizada = pecaRepository.MovimentaEstoque(request.PecaId, TipoMovimentacao.Saida, request.Quantidade);
      return Task.FromResult(quantidadeAtualizada);
    }
}