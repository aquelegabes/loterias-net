using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Loterias.Domain.Abstract;

namespace Loterias.Domain.Entities.Quina
{
    [DisplayName("Ganhadores Quina")]
    [Table("quina_ganhadoresquina")]
    public class GanhadoresQuina : AGanhadoresModel
    {
        
    }
}