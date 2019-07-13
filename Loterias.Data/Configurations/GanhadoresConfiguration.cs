using Loterias.Domain.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loterias.Data.Configurations
{
    public class GanhadoresConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : AGanhadoresModel
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            #region From abstract
            builder.Property(p => p.EstadoUF).IsRequired().HasMaxLength(5);
            builder.Property(p => p.Ganhadores).HasDefaultValue(1);
            builder.Property(p => p.Localizacao).HasDefaultValue(null).HasMaxLength(100);
            #endregion From abstract
        }
    }
}