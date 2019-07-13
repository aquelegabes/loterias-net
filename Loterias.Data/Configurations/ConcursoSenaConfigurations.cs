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
            builder.Property(p => p.GanhadoresQuadra).HasDefaultValue(0);
            builder.Property(p => p.GanhadoresQuina).HasDefaultValue(0);
            builder.Property(p => p.ValorAcumulado).HasDefaultValue(0m);
            builder.Property(p => p.ValorQuadra).HasDefaultValue(0m);
            builder.Property(p => p.ValorQuina).HasDefaultValue(0m);
            builder.HasMany(m => m.GanhadoresModel)
                .WithOne(o => o.Concurso)
                .HasForeignKey(fk => fk.ConcursoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("sena_concursosena").HasKey(k => k.Id);
        }
    }
}