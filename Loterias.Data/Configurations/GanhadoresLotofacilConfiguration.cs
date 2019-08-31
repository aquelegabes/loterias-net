using Loterias.Domain.Entities.Lotofacil;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loterias.Data.Configurations
{
    public class GanhadoresLotofacilConfiguration : GanhadoresConfiguration<GanhadoresLotofacil>
    {
        public override void Configure(EntityTypeBuilder<GanhadoresLotofacil> builder)
        {
            base.Configure(builder);
            builder.ToTable("lotofacil_ganhadoresfacil").HasKey(k => k.Id);
            builder.HasOne(o => o.Concurso)
                .WithMany(m => m.GanhadoresModel)
                .HasForeignKey(fk => fk.ConcursoId);
        }
    }
}