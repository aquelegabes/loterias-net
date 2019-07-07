using Loterias.Domain.Entities.Sena;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loterias.Data.Configurations
{
    public class ConcursoSenaConfigurations : IEntityTypeConfiguration<ConcursoSena>
    {
        public void Configure(EntityTypeBuilder<ConcursoSena> builder)
        {
            #region From abstract
            builder.HasKey(k => k.Id);
            builder.Property(p => p.Concurso);
            builder.Property(p => p.Data);
            builder.Property(p => p.ValorAcumulado).HasMaxLength(15);
            builder.Property(p => p.Resultado).HasMaxLength(150);
            builder.Property(p => p.Ganhadores).HasDefaultValue(0);
            builder.HasMany(m => m.GanhadoresModel);
            #endregion From abstract

            builder.Property(p => p.Acumulado).HasDefaultValue(false);
            builder.Property(p => p.GanhadoresQuadra);
            builder.Property(p => p.GanhadoresQuina);
            builder.Property(p => p.ValorAcumulado);
            builder.Property(p => p.ValorQuadra);
            builder.Property(p => p.ValorQuina);
        }
    }
}