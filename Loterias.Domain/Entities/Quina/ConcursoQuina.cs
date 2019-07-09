using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Loterias.Domain.Abstract;

namespace Loterias.Domain.Entities.Quina 
{
    [DisplayName("Concurso Quina")]
    [Table("quina_concursoquina")]
    public class ConcursoQuina : AConcursoModel
    {
        [DisplayName("Duque")]
        [Column("duque")]
        public string Duque { get; set; }

        [DisplayName("Ganhadores quadra")]
        [Column("ganhadores_quadra")]
        public int? GanhadoresQuadra { get; set; }

        [DisplayName("Ganhadores terno")]
        [Column("ganhadores_terno")]
        public int? GanhadoresTerno { get; set; }

        [DisplayName("Ganhadores duque")]
        [Column("ganhadores_duque")]
        public int? GanhadoresDuque { get; set; }

        [DisplayName("Concurso especial?")]
        [Column("concurso_especial")]
        public bool ConcursoEspecial { get; set; }

        [DisplayName("Valor quadra")]
        [Column("valor_quadra")]
        public decimal? ValorQuadra { get; set; }

        [DisplayName("Valor terno")]
        [Column("valor_terno")]
        public decimal? ValorTerno { get; set; }

        [DisplayName("Valor rateio duque")]
        [Column("valor_rateio_duque")]
        public decimal? ValorRateioDuque { get; set; }
    }
}