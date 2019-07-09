using Loterias.Domain.Entities.Lotofacil;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loterias.Data.Configurations
{
    public class ConcursoLotofacilConfiguration : IEntityTypeConfiguration<ConcursoLotofacil>
    {
        public void Configure(EntityTypeBuilder<ConcursoLotofacil> builder)
        {
            builder.ToTable("lotofacil_concursofacil");

            #region From abstract
            builder.HasKey(k => k.Id);
            builder.Property(p => p.Concurso);
            builder.Property(p => p.Data);
            builder.Property(p => p.Acumulado).HasDefaultValue(false);
            builder.Property(p => p.Resultado).HasMaxLength(150);
            builder.Property(p => p.Ganhadores).HasDefaultValue(0);
            builder.HasMany(m => m.GanhadoresModel);
            #endregion From abstract

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
        }
    }
}