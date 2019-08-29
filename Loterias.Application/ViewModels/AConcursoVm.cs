using System;
using System.Collections.Generic;

namespace Loterias.Application.ViewModels
{
    public abstract class AConcursoVm
    {
        public int Concurso { get; set; }
        public DateTime Data { get; set; }
        public bool Acumulado { get; set; }
        public string ResultadoOrdenadoString { get; set; }
        public IEnumerable<int> ResultadoOrdenado { get; set; }
        public int Ganhadores { get; set; }
        public decimal Valor { get; set; }
    }
}