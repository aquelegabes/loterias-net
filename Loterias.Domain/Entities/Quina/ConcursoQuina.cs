using System;
using System.ComponentModel;
using Loterias.Domain.Abstract;

namespace Loterias.Domain.Entities.Quina 
{
    [DisplayName("Concurso Quina")]
    public class ConcursoQuina : AConcursoModel
    {
        [DisplayName("Duque")]
        public string Duque { get; set; }

        [DisplayName("Ganhadores quadra")]
        public int? GanhadoresQuadra { get; set; }

        [DisplayName("Ganhadores terno")]
        public int? GanhadoresTerno { get; set; }

        [DisplayName("Ganhadores duque")]
        public int? GanhadoresDuque { get; set; }

        [DisplayName("Concurso especial?")]
        public bool ConcursoEspecial { get; set; }

        [DisplayName("Valor quadra")]
        public decimal? ValorQuadra { get; set; }

        [DisplayName("Valor terno")]
        public decimal? ValorTerno { get; set; }

        [DisplayName("Valor rateio duque")]
        public decimal? ValorRateioDuque { get; set; }
    }
}