using MediatR;
using wrench.auto.repair.estoque.domain.Entities;

namespace wrench.auto.repair.estoque.application.UseCases.ConsultaPecas;

public record ConsultaPecasCommand(): IRequest<IEnumerable<Peca>>;