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
        [DisplayName("Concurso")]
        [Column("concurso_id")]
        public virtual ConcursoSena Concurso { get; set; }
    }
}
