using System;
using System.Collections.Generic;
using System.Text;
using wrench.auto.repair.ordem.servico.domain.Entities;
using wrench.auto.repair.ordem.servico.domain.Enums;

namespace wrench.auto.repair.ordem.servico.application.Queries.ViewModels
{
    public class OrdemServicoViewModel
    {
        public Guid OrdemServicoId { get; set; }
        public Guid Veiculo { get; set; }
        public string Descricao { get; set; }
        public OrdemServicoStatus Status { get; set; }
        public DiagnosticoViewModel DiagnosticoViewModel { get; set; }

        public static implicit operator OrdemServicoViewModel(OrdemServico ordemServico)
        {
            if (ordemServico == null)
                return null;

            return new OrdemServicoViewModel
            {
                OrdemServicoId = ordemServico.Id,
                Veiculo = ordemServico.VeiculoId,
                Descricao = ordemServico.Descricao,
                Status = ordemServico.Status,
                DiagnosticoViewModel = new DiagnosticoViewModel
                {
                    SolucaoProposta = ordemServico.Diagnostico?.SolucaoProposta,
                    DataDiagnostico = ordemServico.Diagnostico?.DataDiagnostico ?? default
                }
            };
        }
    }
}
