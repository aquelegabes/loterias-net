using System;
using Loterias.Domain.Interfaces;
using Loterias.Common.Enums;
using Loterias.Common.Extensions;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Loterias.Domain.Abstract
{
    public abstract class AGanhadoresModel
    {
        [DisplayName("Id")]
        [Column("id")]
        public virtual int Id { get; set; }
        
        [DisplayName("Estado")]
        [Column("sguf")]
        public Estados Estado { get; set; }

        [DisplayName("Estado")]
        [NotMapped]
        public string EstadoCompleto 
        {
            get
            {
                return Estado.GetDescription();
            }
        }

        [DisplayName("Localização")]
        [Column("location")]
        public string Localizacao { get; set; }

        [DisplayName("Quantidade de ganhadores por estado")]
        [Column("ganhadores_uf")]
        public int Ganhadores { get; set; }

        [DisplayName("Concurso")]
        [Column("concurso_id")]
        public int ConcursoId { get;set; }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:Loterias.Domain.Abstract.GanhadoresModel"/>.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:Loterias.Domain.Abstract.GanhadoresModel"/>.</returns>
        public override string ToString()
        {
            return $@"Para o estado de {this.Estado.GetDescription()}, houveram {Ganhadores} ganhador(es).
                    com a localização de {Localizacao}";
        }
    }
}
