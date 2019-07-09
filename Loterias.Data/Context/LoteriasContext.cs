using Microsoft.EntityFrameworkCore;
using Loterias.Domain.Entities.Sena;
using Loterias.Domain.Entities.Quina;
using Loterias.Domain.Entities.Lotofacil;
using Loterias.Data.Configurations;
using System.Configuration;

namespace Loterias.Data.Context
{
    public class LoteriasContext : DbContext
    {
        public LoteriasContext(DbContextOptions options) : base(options) { }

        public DbSet<ConcursoSena> ConcursosSena { get;set; }
        public DbSet<ConcursoQuina> ConcursosQuina { get; set; }
        public DbSet<ConcursoLotofacil> ConcursosLotofacil { get; set; }
        public DbSet<GanhadoresFacil> GanhadoresFacil { get; set; }
        public DbSet<GanhadoresQuina> GanhadoresQuina { get; set; }
        public DbSet<GanhadoresSena> GanhadoresSena { get;set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=db.sqlite3");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(ConfigurationManager.AppSettings["Sqlite"]);

            modelBuilder.ApplyConfiguration(new ConcursoSenaConfigurations());
            modelBuilder.ApplyConfiguration(new GanhadoresSenaConfiguration());
            modelBuilder.ApplyConfiguration(new ConcursoQuinaConfiguration());
            modelBuilder.ApplyConfiguration(new GanhadoresQuinaConfiguration());
            modelBuilder.ApplyConfiguration(new GanhadoresFacilConfiguration());
            modelBuilder.ApplyConfiguration(new ConcursoLotofacilConfiguration());
        }
    }
}