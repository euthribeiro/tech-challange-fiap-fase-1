using MediatR;
using wrench.auto.repair.estoque.domain.Entities;

namespace wrench.auto.repair.estoque.application.UseCases.CriarPeca;

public record CriarPecaCommand : IRequest<Peca>
{
    public string Nome { get; set; }
    public string Descricao   {get; set; }
    public double Valor { get; set; }
    public double Quantidade { get; set; }
}