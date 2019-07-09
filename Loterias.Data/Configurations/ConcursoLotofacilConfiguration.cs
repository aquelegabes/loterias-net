using Loterias.Domain.Entities.Lotofacil;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Loterias.Data.Configurations;

namespace Loterias.Data.Configurations
{
    public class ConcursoLotofacilConfiguration : ConcursoConfiguration<ConcursoLotofacil>
    {
        public override void Configure(EntityTypeBuilder<ConcursoLotofacil> builder)
        {
            base.Configure(builder);
            builder.ToTable("lotofacil_concursofacil").HasKey(k => k.Id);
            builder.Property(p => p.ConcursoEspecial).HasDefaultValue(false);
            builder.Property(p => p.CatorzeAcertos);
            builder.Property(p => p.TrezeAcertos);
            builder.Property(p => p.DozeAcertos);
            builder.Property(p => p.OnzeAcertos);
            builder.Property(p => p.ValorAcumuladoEspecial);
            builder.Property(p => p.ValorCatorze);
            builder.Property(p => p.ValorDoze);
            builder.Property(p => p.ValorOnze);
            builder.Property(p => p.ValorTreze);
            builder.HasMany(m => m.GanhadoresModel);
        }
    }
}