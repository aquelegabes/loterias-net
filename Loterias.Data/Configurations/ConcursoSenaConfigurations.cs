using Loterias.Domain.Entities.Sena;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loterias.Data.Configurations
{
    public class ConcursoSenaConfigurations : ConcursoConfiguration<ConcursoSena>
    {
        public override void Configure(EntityTypeBuilder<ConcursoSena> builder)
        {
            builder.ToTable("sena_concursosena");
            base.Configure(builder);
            builder.Property(p => p.Acumulado).HasDefaultValue(false);
            builder.Property(p => p.GanhadoresQuadra);
            builder.Property(p => p.GanhadoresQuina);
            builder.Property(p => p.ValorAcumulado);
            builder.Property(p => p.ValorQuadra);
            builder.Property(p => p.ValorQuina);
        }
    }
}