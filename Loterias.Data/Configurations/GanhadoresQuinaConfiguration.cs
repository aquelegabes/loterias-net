using Loterias.Domain.Entities.Quina;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loterias.Data.Configurations
{
    public class GanhadoresQuinaConfiguration : GanhadoresConfiguration<GanhadoresQuina>
    {
        public override void Configure(EntityTypeBuilder<GanhadoresQuina> builder)
        {
            builder.ToTable("quina_ganhadoresquina");
            base.Configure(builder);
        }
    }
}