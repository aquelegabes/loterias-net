using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Loterias.Domain.Abstract;

namespace Loterias.Data.Configurations
{
    public class ConcursoConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : AConcursoModel
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            #region From abstract
            builder.HasIndex(index => index.Concurso)
                .IsUnique();
            builder.Property(p => p.Concurso).IsRequired();
            builder.Property(p => p.Data).IsRequired();
            builder.Property(p => p.Acumulado).HasDefaultValue(false);
            builder.Property(p => p.Resultado).IsRequired().HasMaxLength(150);
            builder.Property(p => p.Ganhadores).HasDefaultValue(0);
            builder.Property(p => p.Valor).HasDefaultValue(0m);
            #endregion From abstract
        }
    }
}