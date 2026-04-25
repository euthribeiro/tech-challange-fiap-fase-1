using MediatR;
using wrench.auto.repair.estoque.domain.Interfaces.Repositories;

namespace wrench.auto.repair.estoque.application.UseCases.DeletarPeca;

public class DeletarPecaHandler : IRequestHandler<DeletarPecaCommand, Guid>
{
    IPecaRepository _pecaRepository;

    public DeletarPecaHandler(IPecaRepository pecaRepository)
    {
        _pecaRepository = pecaRepository;
    }
    
    async public Task<Guid> Handle(DeletarPecaCommand request, CancellationToken cancellationToken)
    {
       _pecaRepository.DeletaPeca(request.Id);
        
       return request.Id;
    }
}