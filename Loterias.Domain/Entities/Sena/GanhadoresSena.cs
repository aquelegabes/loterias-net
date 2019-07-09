using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Loterias.Domain.Abstract;

namespace Loterias.Domain.Entities.Sena
{
    [DisplayName("Ganhadores Mega Sena")]
    [Table("sena_ganhadoressena")]
    public class GanhadoresSena : AGanhadoresModel
    {
        [DisplayName("Id")]
        [Column("id")]
        public override int Id { get; set; }
        
        [DisplayName("Concurso")]
        public ConcursoSena Concurso { get; set; }
    }
}
