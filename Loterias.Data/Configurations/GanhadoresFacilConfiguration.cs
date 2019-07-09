using Loterias.Domain.Entities.Lotofacil;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loterias.Data.Configurations
{
    public class GanhadoresFacilConfiguration : GanhadoresConfiguration<GanhadoresFacil>
    {
        public override void Configure(EntityTypeBuilder<GanhadoresFacil> builder)
        {
            builder.ToTable("lotofacil_ganhadoresfacil");
            base.Configure(builder);
        }
    }
}