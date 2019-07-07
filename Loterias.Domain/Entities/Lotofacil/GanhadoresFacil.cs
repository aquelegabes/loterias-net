using System.ComponentModel;
using Loterias.Domain.Abstract;

namespace Loterias.Domain.Entities.Lotofacil
{
    [DisplayName("Ganhadores Lotofácil")]
    public class GanhadoresFacil : AGanhadoresModel
    {
        [DisplayName("Concurso")]
        public virtual ConcursoLotofacil Concurso { get; set; }
    }
}