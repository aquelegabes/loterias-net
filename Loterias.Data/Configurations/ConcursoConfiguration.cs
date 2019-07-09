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
            builder.Property(p => p.Concurso);
            builder.Property(p => p.Data);
            builder.Property(p => p.Acumulado).HasDefaultValue(false);
            builder.Property(p => p.Resultado).HasMaxLength(150);
            builder.Property(p => p.Ganhadores).HasDefaultValue(0);
            #endregion From abstract
        }
    }
}