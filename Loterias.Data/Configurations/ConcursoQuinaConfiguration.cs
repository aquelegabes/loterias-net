using Loterias.Domain.Entities.Quina;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loterias.Data.Configurations
{
    public class ConcursoQuinaConfiguration : ConcursoConfiguration<ConcursoQuina>
    {
        public override void Configure(EntityTypeBuilder<ConcursoQuina> builder)
        {
            base.Configure(builder);
            builder.ToTable("quina_concursoquina").HasKey(k => k.Id);
            builder.Property(p => p.Duque).HasMaxLength(50);
            builder.Property(p => p.GanhadoresDuque).HasDefaultValue(0);
            builder.Property(p => p.GanhadoresQuadra).HasDefaultValue(0);
            builder.Property(p => p.GanhadoresTerno).HasDefaultValue(0);
            builder.Property(p => p.ConcursoEspecial).HasDefaultValue(false);
            builder.Property(p => p.ValorQuadra).HasDefaultValue(0m);
            builder.Property(p => p.ValorRateioDuque).HasDefaultValue(0m);
            builder.Property(p => p.ValorTerno).HasDefaultValue(0m);
            builder.HasMany(m => m.GanhadoresModel)
                .WithOne(o => o.Concurso)
                .HasForeignKey(fk => fk.ConcursoId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}