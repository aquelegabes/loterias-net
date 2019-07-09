using Loterias.Domain.Entities.Lotofacil;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loterias.Data.Configurations
{
    public class GanhadoresFacilConfiguration : IEntityTypeConfiguration<GanhadoresFacil>
    {
        public void Configure(EntityTypeBuilder<GanhadoresFacil> builder)
        {
            builder.ToTable("lotofacil_ganhadoresfacil");
            #region From abstract
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Estado).HasMaxLength(5);
            builder.Property(p => p.Ganhadores).HasDefaultValue(0);
            builder.Property(p => p.Localizacao).HasMaxLength(100);
            builder.OwnsOne(o => o.Concurso);
            #endregion From abstract
        }
    }
}