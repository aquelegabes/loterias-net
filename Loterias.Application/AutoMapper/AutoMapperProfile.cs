using AutoMapper;
using Loterias.Application.ViewModels;
using Loterias.Domain.Entities.Sena;
using Loterias.Domain.Entities.Lotofacil;
using Loterias.Domain.Entities.Quina;

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
            CreateMap<ConcursoLotofacil, ConcursoLotofacilVm>().ReverseMap();
            CreateMap<ConcursoQuina, ConcursoQuinaVm>().ReverseMap();
            
            CreateMap<GanhadoresLotofacil, GanhadoresLotofacilVm>().ReverseMap();
            CreateMap<GanhadoresSena, GanhadoresSenaVm>().ReverseMap();
            CreateMap<GanhadoresQuina, GanhadoresQuinaVm>().ReverseMap();
        }
    }
}