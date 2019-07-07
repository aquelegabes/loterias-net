using Loterias.Domain.Entities.Sena;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loterias.Data.Configurations
{
    public class GanhadoresSenaConfiguration : IEntityTypeConfiguration<GanhadoresSena>
    {
        public void Configure(EntityTypeBuilder<GanhadoresSena> builder)
        {
            #region From abstract
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Estado).HasMaxLength(5);
            builder.Property(p => p.Ganhadores).HasDefaultValue(0);
            builder.Property(p => p.Localizacao).HasMaxLength(100);
            builder.HasOne(o => o.Concurso);
            #endregion From abstract
        }
    }
}