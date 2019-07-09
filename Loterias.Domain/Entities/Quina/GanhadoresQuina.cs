using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Loterias.Domain.Abstract;

namespace Loterias.Domain.Entities.Quina
{
    [DisplayName("Ganhadores Quina")]
    [Table("quina_ganhadoresquina")]
    public class GanhadoresQuina : AGanhadoresModel
    {
        [DisplayName("Id")]
        [Column("id")]
        public override int Id { get; set; }

        [DisplayName("Concurso")]
        public ConcursoQuina Concurso { get;set; }
    }
}