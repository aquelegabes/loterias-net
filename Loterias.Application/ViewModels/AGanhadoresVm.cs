using Loterias.Common.Enums;

namespace Loterias.Application.ViewModels
{
    public abstract class AGanhadoresVm
    {
        public string Estado { get; set; }
        public string Localizacao { get; set; }
        public int Ganhadores { get; set; }
    }
}