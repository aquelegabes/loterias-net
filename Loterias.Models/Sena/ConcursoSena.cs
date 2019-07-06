using System;
using Loterias.Contract.Abstract;

namespace Loterias.Models.Sena
{
    public class ConcursoSena : AConcursoModel
    {
        public int GanhadoresQuadra { get; set; }
        public int GanhadoresQuina { get; set; }
        public decimal ValorAcumulado { get; set; }
        public decimal ValorQuina { get; set; }
        public decimal ValorQuadra { get; set; }
    }
}
