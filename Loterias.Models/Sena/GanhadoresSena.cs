using System;
using Loterias.Contract.Abstract;

namespace Loterias.Models.Sena
{
    public class GanhadoresSena : AGanhadoresModel
    {
        public virtual ConcursoSena Concurso { get; set; }
    }
}
