using System.Collections.Generic;

namespace Loterias.Application.ViewModels
{
    public class ConcursoSenaVm : AConcursoVm
    {
        public int? GanhadoresQuadra { get; set; }
        public int? GanhadoresQuina { get; set; }
        public decimal? ValorAcumulado { get; set; }
        public decimal? ValorQuina { get; set; }
        public decimal? ValorQuadra { get; set; }
        public ICollection<GanhadoresSenaVm> GanhadoresModel { get;set; }
    }
}