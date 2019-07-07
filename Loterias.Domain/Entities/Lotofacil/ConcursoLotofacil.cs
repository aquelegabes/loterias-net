using System;
using System.ComponentModel;
using Loterias.Domain.Abstract;

namespace Loterias.Domain.Entities.Lotofacil
{
    [DisplayName("Concurso Lotofácil")]
    public class ConcursoLotofacil : AConcursoModel
    {
        [DisplayName("Concurso especial?")]
        public bool ConcursoEspecial { get; set; }

        [DisplayName("Ganhadores com catorze acertos")]
        public int? CatorzeAcertos { get; set; }

        [DisplayName("Ganhadores com treze acertos")]
        public int? TrezeAcertos { get; set; }

        [DisplayName("Ganhadores com doze acertos")]
        public int? DozeAcertos { get; set; }

        [DisplayName("Ganhadores com onze acertos")]
        public int? OnzeAcertos { get; set; }

        [DisplayName("Valor acumulado para concurso especial")]
        public decimal? ValorAcumuladoEspecial { get; set; }

        [DisplayName("Valor para catorze acertos")]
        public decimal? ValorCatorze { get; set; }

        [DisplayName("Valor para treze acertos")]
        public decimal? ValorTreze { get; set; }

        [DisplayName("Valor para doze acertos")]
        public decimal ValorDoze { get; set; }

        [DisplayName("Valor para onze acertos")]
        public decimal? ValorOnze { get; set; }
    }
}