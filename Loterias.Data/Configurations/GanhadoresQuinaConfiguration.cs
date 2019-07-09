using Loterias.Domain.Entities.Quina;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loterias.Data.Configurations
{
    public class GanhadoresQuinaConfiguration : GanhadoresConfiguration<GanhadoresQuina>
    {
        public override void Configure(EntityTypeBuilder<GanhadoresQuina> builder)
        {
            base.Configure(builder);
            builder.ToTable("quina_ganhadoresquina").HasKey(k => k.Id);
            builder.HasOne(o => o.Concurso)
                .WithMany(m => m.GanhadoresModel)
                .HasForeignKey(fk => fk.ConcursoId);
        }
    }
}