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
            builder.Property(p => p.GanhadoresDuque);
            builder.Property(p => p.GanhadoresQuadra);
            builder.Property(p => p.GanhadoresTerno);
            builder.Property(p => p.ConcursoEspecial).HasDefaultValue(false);
            builder.Property(p => p.ValorQuadra);
            builder.Property(p => p.ValorRateioDuque);
            builder.Property(p => p.ValorTerno);
            builder.HasMany(m => m.GanhadoresModel);
        }
    }
}