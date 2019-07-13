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
            builder.Property(p => p.CatorzeAcertos).HasDefaultValue(0);
            builder.Property(p => p.TrezeAcertos).HasDefaultValue(0);
            builder.Property(p => p.DozeAcertos).HasDefaultValue(0);
            builder.Property(p => p.OnzeAcertos).HasDefaultValue(0);
            builder.Property(p => p.ValorAcumuladoEspecial).HasDefaultValue(0m);
            builder.Property(p => p.ValorCatorze).HasDefaultValue(0m);
            builder.Property(p => p.ValorDoze).HasDefaultValue(0m);
            builder.Property(p => p.ValorOnze).HasDefaultValue(0m);
            builder.Property(p => p.ValorTreze).HasDefaultValue(0m);
            builder.HasMany(m => m.GanhadoresModel)
                .WithOne(o => o.Concurso)
                .HasForeignKey(fk => fk.ConcursoId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}