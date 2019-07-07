using System;
using System.ComponentModel;
using Loterias.Domain.Abstract;

namespace Loterias.Domain.Entities.Sena
{
    [DisplayName("Ganhadores Mega Sena")]
    public class GanhadoresSena : AGanhadoresModel
    {
        [DisplayName("Concurso")]
        public virtual ConcursoSena Concurso { get; set; }
    }
}
