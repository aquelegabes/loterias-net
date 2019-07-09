using Loterias.Domain.Entities.Sena;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loterias.Data.Configurations
{
    public class ConcursoSenaConfigurations : ConcursoConfiguration<ConcursoSena>
    {
        public override void Configure(EntityTypeBuilder<ConcursoSena> builder)
        {
            base.Configure(builder);
            builder.ToTable("sena_concursosena").HasKey(k => k.Id);
            builder.Property(p => p.Acumulado).HasDefaultValue(false);
            builder.Property(p => p.GanhadoresQuadra);
            builder.Property(p => p.GanhadoresQuina);
            builder.Property(p => p.ValorAcumulado);
            builder.Property(p => p.ValorQuadra);
            builder.Property(p => p.ValorQuina);
            builder.HasMany(m => m.GanhadoresModel)
                .WithOne(o => o.Concurso)
                .HasForeignKey(fk => fk.ConcursoId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}