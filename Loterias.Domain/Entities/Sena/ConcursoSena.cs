using System;
using System.ComponentModel;
using Loterias.Domain.Abstract;

namespace Loterias.Domain.Entities.Sena
{
    [DisplayName("Concurso Mega Sena")]
    public class ConcursoSena : AConcursoModel
    {
        [DisplayName("Ganhadores quadra")]
        public int? GanhadoresQuadra { get; set; }

        [DisplayName("Ganhadores quina")]
        public int? GanhadoresQuina { get; set; }

        [DisplayName("Valor acumulado")]
        public decimal? ValorAcumulado { get; set; }

        [DisplayName("Valor quina")]
        public decimal? ValorQuina { get; set; }

        [DisplayName("Valor quadra")]
        public decimal? ValorQuadra { get; set; }
    }
}
