using Loterias.Domain.Entities.Sena;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loterias.Data.Configurations
{
    public class GanhadoresSenaConfiguration : GanhadoresConfiguration<GanhadoresSena>
    {
        public override void Configure(EntityTypeBuilder<GanhadoresSena> builder)
        {
            base.Configure(builder);
            builder.ToTable("sena_ganhadoressena").HasKey(k => k.Id);
            builder.HasOne(o => o.Concurso)
                .WithMany(m => m.GanhadoresModel)
                .HasForeignKey(fk => fk.ConcursoId);
        }
    }
}