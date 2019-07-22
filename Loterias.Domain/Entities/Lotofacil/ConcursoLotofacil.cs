using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Loterias.Domain.Abstract;

namespace Loterias.Domain.Entities.Lotofacil
{
    [DisplayName("Concurso Lotofácil")]
    [Table("lotofacil_concursofacil")]
    public class ConcursoLotofacil : AConcursoModel
    {
        [DisplayName("Ganhadores")]
        public virtual ICollection<GanhadoresFacil> GanhadoresModel { get;set; }

        [DisplayName("Id")]
        [Column("id")]
        public override int Id { get; set; }

        [DisplayName("Concurso especial?")]
        [Column("concurso_especial")]
        public bool ConcursoEspecial { get; set; }

        [DisplayName("Ganhadores com catorze acertos")]
        [Column("catorze_acertos")]
        public int? CatorzeAcertos { get; set; }

        [DisplayName("Ganhadores com treze acertos")]
        [Column("treze_acertos")]
        public int? TrezeAcertos { get; set; }

        [DisplayName("Ganhadores com doze acertos")]
        [Column("doze_acertos")]
        public int? DozeAcertos { get; set; }

        [DisplayName("Ganhadores com onze acertos")]
        [Column("onze_acertos")]
        public int? OnzeAcertos { get; set; }

        [DisplayName("Valor acumulado para concurso especial")]
        [Column("valor_acumulado_especial")]
        public decimal? ValorAcumuladoEspecial { get; set; }

        [DisplayName("Valor para catorze acertos")]
        [Column("valor_catorze")]
        public decimal? ValorCatorze { get; set; }

        [DisplayName("Valor para treze acertos")]
        [Column("valor_treze")]
        public decimal? ValorTreze { get; set; }

        [DisplayName("Valor para doze acertos")]
        [Column("valor_doze")]
        public decimal? ValorDoze { get; set; }

        [DisplayName("Valor para onze acertos")]
        [Column("valor_onze")]
        public decimal? ValorOnze { get; set; }
    }
}