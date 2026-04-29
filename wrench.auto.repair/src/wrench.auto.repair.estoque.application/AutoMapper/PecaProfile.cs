using AutoMapper;
using wrench.auto.repair.estoque.application.Queries.ViewModels;
using wrench.auto.repair.estoque.domain.Entities;

namespace wrench.auto.repair.estoque.application.AutoMapper
{
    public class PecaProfile : Profile
    {
        public PecaProfile()
        {
            CreateMap<Peca, PecaViewModel>();
        }
    }
}
