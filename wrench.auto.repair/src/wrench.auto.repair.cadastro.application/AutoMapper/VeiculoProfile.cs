using AutoMapper;
using wrench.auto.repair.cadastro.application.Commands;
using wrench.auto.repair.cadastro.domain.Entities;

namespace wrench.auto.repair.cadastro.application.AutoMapper
{
    public class VeiculoProfile : Profile
    {
        public VeiculoProfile()
        {
            CreateMap<CadastrarVeiculoCommand, Veiculo>();
        }
    }
}
