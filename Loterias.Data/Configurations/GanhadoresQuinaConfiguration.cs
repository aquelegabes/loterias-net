using Loterias.Domain.Entities.Quina;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loterias.Data.Configurations
{
    public class GanhadoresQuinaConfiguration : IEntityTypeConfiguration<GanhadoresQuina>
    {
        public void Configure(EntityTypeBuilder<GanhadoresQuina> builder)
        {
            builder.ToTable("quina_ganhadoresquina");
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