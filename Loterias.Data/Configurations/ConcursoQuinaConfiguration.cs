using Loterias.Domain.Entities.Quina;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loterias.Data.Configurations
{
    public class ConcursoQuinaConfiguration : IEntityTypeConfiguration<ConcursoQuina>
    {
        public void Configure(EntityTypeBuilder<ConcursoQuina> builder)
        {
            #region From abstract
            builder.HasKey(k => k.Id);
            builder.Property(p => p.Concurso);
            builder.Property(p => p.Data);
            builder.Property(p => p.Acumulado).HasDefaultValue(false);
            builder.Property(p => p.Resultado).HasMaxLength(150);
            builder.Property(p => p.Ganhadores).HasDefaultValue(0);
            builder.HasMany(m => m.GanhadoresModel);
            #endregion From abstract

            builder.Property(p => p.Duque).HasMaxLength(50);
            builder.Property(p => p.GanhadoresDuque);
            builder.Property(p => p.GanhadoresQuadra);
            builder.Property(p => p.GanhadoresTerno);
            builder.Property(p => p.ConcursoEspecial).HasDefaultValue(false);
            builder.Property(p => p.ValorQuadra);
            builder.Property(p => p.ValorRateioDuque);
            builder.Property(p => p.ValorTerno);
        }
        
    }
}