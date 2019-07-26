using AutoMapper;
using Loterias.Application.ViewModels;
using Loterias.Domain.Entities.Sena;

namespace Loterias.Application.AutoMapper
{
    /// <summary>
    /// Responsible for mapping domain entities to view models.
    /// </summary>
    public class AutoMapperProfile : Profile
    {
        /// <summary>
        /// Declare all mapping models on this constructor.
        /// </summary>
        public AutoMapperProfile()
        {
            CreateMap<ConcursoSena, ConcursoSenaVm>().ReverseMap();
            CreateMap<GanhadoresSena, GanhadoresSenaVm>().ReverseMap();
        }
    }
}