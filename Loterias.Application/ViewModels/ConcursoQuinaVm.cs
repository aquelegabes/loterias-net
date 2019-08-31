using System.Collections.Generic;

namespace Loterias.Application.ViewModels
{
    public class ConcursoQuinaVm
    {
        public string Duque { get; set; }
        public int? GanhadoresQuadra { get; set; }
        public int? GanhadoresTerno { get; set; }
        public int? GanhadoresDuque { get; set; }
        public bool ConcursoEspecial { get; set; }
        public decimal? ValorQuadra { get; set; }
        public decimal? ValorTerno { get; set; }
        public decimal? ValorRateioDuque { get; set; }
        public IEnumerable<GanhadoresQuinaVm> GanhadoresModel { get; set; }
    }
}
