using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Loterias.Domain.Abstract;

namespace Loterias.Domain.Entities.Lotofacil
{
    [DisplayName("Ganhadores Lotofácil")]
    [Table("lotofacil_ganhadoresfacil")]
    public class GanhadoresFacil : AGanhadoresModel
    {
        [DisplayName("Concurso")]
        [Column("concurso_id")]
        public virtual ConcursoLotofacil Concurso { get; set; }
    }
}