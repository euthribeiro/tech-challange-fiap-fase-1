using MediatR;

namespace wrench.auto.repair.estoque.application.UseCases.EntradaPeca;

public record EntradaPecaCommand : IRequest<double>
{
    public Guid PecaId { get; set; }
    public double Quantidade { get; set; }
}