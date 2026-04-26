using MediatR;
using wrench.auto.repair.estoque.domain.Entities;

namespace wrench.auto.repair.estoque.application.UseCases.ConsultaPecaPorId;

public record ConsultarPecaPorIdCommand : IRequest<Peca>
{
    public Guid IdPeca { get; set; }
}