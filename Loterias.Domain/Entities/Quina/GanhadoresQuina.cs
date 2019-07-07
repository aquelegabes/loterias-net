using System.ComponentModel;
using Loterias.Domain.Abstract;

namespace Loterias.Domain.Entities.Quina
{
    [DisplayName("Ganhadores Quina")]
    public class GanhadoresQuina : AGanhadoresModel
    {
        [DisplayName ("Concurso")]
        public virtual ConcursoQuina Concurso { get;set; }
    }
}