using MediatR;

namespace wrench.auto.repair.estoque.application.UseCases.DeletarPeca;

public record DeletarPecaCommand : IRequest<Guid>
{
    public Guid Id { get; set; }
}