using AutoMapper;
using Loterias.Application.ViewModels;
using Loterias.Domain.Entities.Sena;

namespace Loterias.Application.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ConcursoSena, ConcursoSenaVm>().ReverseMap();
            CreateMap<GanhadoresSena, GanhadoresSenaVm>().ReverseMap();
        }
    }
}