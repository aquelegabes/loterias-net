using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Loterias.Domain.Abstract;

namespace Loterias.Domain.Entities.Lotofacil
{
    [DisplayName("Ganhadores Lotofácil")]
    [Table("lotofacil_ganhadoresfacil")]
    public class GanhadoresFacil : AGanhadoresModel
    {
        
    }
}