using MediatR;
using Microsoft.AspNetCore.Mvc;
using wrench.auto.repair.ordem.servico.application.UseCases.CriarOrdemServico;

namespace wrench.web.api.Endpoints
{
    public static class OrdemServicoEndpoint
    {
        public static void MapOrdemServicoEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/ordem-servico").WithTags("Ordem de Serviço");

            _ = group.MapPost("/", async ([FromBody] CriarOrdemServicoCommand command, [FromServices] IMediator mediator) =>
            {
                var result = await mediator.Send(command);

                return Results.Created($"/api/ordem-servico/{result}", new { id = result });
            })
            .WithName("CriarOrdemServico")
            .WithSummary("Cria uma nova ordem de serviço");
        }
    }
}
