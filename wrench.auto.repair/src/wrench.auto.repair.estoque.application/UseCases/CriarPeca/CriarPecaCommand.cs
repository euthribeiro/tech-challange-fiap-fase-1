using MediatR;

namespace wrench.auto.repair.estoque.application.UseCases.CriarPeca;

public record CriarPecaCommand : IRequest<Guid>
{
    public string Nome { get; set; }
    public string Descricao   {get; set; }
    public double Valor { get; set; }
}