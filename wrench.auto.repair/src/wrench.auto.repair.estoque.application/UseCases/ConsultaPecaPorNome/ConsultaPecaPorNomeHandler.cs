using MediatR;
using wrench.auto.repair.estoque.domain.Entities;
using wrench.auto.repair.estoque.domain.Interfaces.Repositories;

namespace wrench.auto.repair.estoque.application.UseCases.ConsultaPecaPorNome;

public class ConsultaPecaPorNomeHandler(IPecaRepository _pecaRepository) :IRequestHandler<ConsultaPecaPorNomeCommand, IEnumerable<Peca>>
{
    
    
    public Task<IEnumerable<Peca>> Handle(ConsultaPecaPorNomeCommand request, CancellationToken cancellationToken)
    {
        var pecas = _pecaRepository.ConsultaPecaPorNome(request.NomePeca);
        return Task.FromResult(pecas);
    }
}