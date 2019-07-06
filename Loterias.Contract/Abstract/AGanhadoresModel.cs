using System;
using Loterias.Contract.Inteface;
using Loterias.Common.Enums;
using Loterias.Common.Extensions;

namespace Loterias.Contract.Abstract
{
    public class AGanhadoresModel : IModel
    {
        public long Id { get; set; }
        public string EstadoUF { get; set; }
        public string Localizacao { get; set; }
        public int Ganhadores { get; set; }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:Loterias.Contract.Abstract.GanhadoresModel"/>.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:Loterias.Contract.Abstract.GanhadoresModel"/>.</returns>
        public override string ToString()
        {
            var estado = EstadoUF.ToEnum<Estados>();
            return $@"Para o estado de {estado.GetDescription()}, houveram {Ganhadores} ganhador(es).
                    com a localização de {Localizacao}";
        }
    }
}
