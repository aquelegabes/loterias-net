using System.Collections.Generic;

namespace Loterias.Application.ViewModels
{
    public class ConcursoLotofacilVm : AConcursoVm
    {
        public bool ConcursoEspecial { get; set; }
        public int? CatorzeAcertos { get; set; }
        public int? TrezeAcertos { get; set; }
        public int? DozeAcertos { get; set; }
        public int? OnzeAcertos { get; set; }
        public decimal? ValorAcumuladoEspecial { get; set; }
        public decimal? ValorTreze { get; set; }
        public decimal? ValorDoze { get; set; }
        public decimal? ValorOnze { get; set; }
        public IEnumerable<GanhadoresLotofacilVm> GanhadoresModel { get; set; }
    }
}
