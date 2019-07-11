using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Loterias.Domain.Abstract;

namespace Loterias.Domain.Entities.Lotofacil
{
    [DisplayName("Ganhadores Lotofácil")]
    [Table("lotofacil_ganhadoresfacil")]
    public class GanhadoresFacil : AGanhadoresModel
    {
        [DisplayName("Id")]
        [Column("id")]
        public override int Id { get; set; }

        [DisplayName("Concurso")]
        public virtual ConcursoLotofacil Concurso { get;set; }
    }
}