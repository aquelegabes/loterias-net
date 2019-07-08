using System;
using Loterias.Domain.Interfaces;
using Loterias.Common.Enums;
using Loterias.Common.Extensions;
using System.ComponentModel;

namespace Loterias.Domain.Abstract
{
    public class AGanhadoresModel
    {
        [DisplayName("Id")]
        public long Id { get; set; }

        [DisplayName("Estado")]
        public Estados Estado { get; set; }

        [DisplayName("Localização")]
        public string Localizacao { get; set; }

        [DisplayName("Quantidade de ganhadores por estado")]
        public int Ganhadores { get; set; }

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
