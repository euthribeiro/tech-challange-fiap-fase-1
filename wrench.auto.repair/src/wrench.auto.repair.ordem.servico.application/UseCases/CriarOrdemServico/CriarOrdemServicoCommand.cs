using MediatR;
using System;

namespace wrench.auto.repair.ordem.servico.application.UseCases.CriarOrdemServico
{
    public record CriarOrdemServicoCommand : IRequest<Guid>
    {
        public Guid ClienteId { get; set; }
        public Guid VeiculoId { get; set; }
        public Guid AtendenteId { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
