using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Loterias.Domain.Abstract;

namespace Loterias.Domain.Entities.Sena
{
    [DisplayName("Concurso Mega Sena")]
    [Table("sena_concursosena")]
    public class ConcursoSena : AConcursoModel
    {
        [DisplayName("Ganhadores")]
        public virtual ICollection<GanhadoresSena> GanhadoresModel { get;set; }

        [DisplayName("Id")]
        [Column("id")]
        public override int Id { get; set; }

        [DisplayName("Ganhadores quadra")]
        [Column("ganhadores_quadra")]
        public int? GanhadoresQuadra { get; set; }

        [DisplayName("Ganhadores quina")]
        [Column("ganhadores_quina")]
        public int? GanhadoresQuina { get; set; }

        [DisplayName("Valor acumulado")]
        [Column("valor_acumulado")]
        public decimal? ValorAcumulado { get; set; }

        [DisplayName("Valor quina")]
        [Column("valor_quina")]
        public decimal? ValorQuina { get; set; }

        [DisplayName("Valor quadra")]
        [Column("valor_quadra")]
        public decimal? ValorQuadra { get; set; }
    }
}
