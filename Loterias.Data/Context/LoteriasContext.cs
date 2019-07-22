using Microsoft.EntityFrameworkCore;
using Loterias.Domain.Entities.Sena;
using Loterias.Domain.Entities.Quina;
using Loterias.Domain.Entities.Lotofacil;
using Loterias.Data.Configurations;
using System.Configuration;

namespace Loterias.Data.Context
{
    /// <summary>
    /// A context containing existing models.
    /// </summary>
    public class LoteriasContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoteriasContext"/> class
        /// using the specified options. The <see cref="OnConfiguring(DbContextOptionsBuilder)"/>
        /// method will still be called to allow further configuration of the options.
        /// </summary>
        /// <param name="options">The options for this context.</param>
        public LoteriasContext(DbContextOptions options) : base(options) { }

        public DbSet<ConcursoSena> ConcursosSena { get;set; }
        public DbSet<ConcursoQuina> ConcursosQuina { get; set; }
        public DbSet<ConcursoLotofacil> ConcursosLotofacil { get; set; }
        public DbSet<GanhadoresFacil> GanhadoresFacil { get; set; }
        public DbSet<GanhadoresQuina> GanhadoresQuina { get; set; }
        public DbSet<GanhadoresSena> GanhadoresSena { get;set; }

        /// <summary>
        /// Configure the database (and other options) to be used for this context.
        /// This method is called for each instance of the context that is created.
        /// </summary>
        /// <param name="optionsBuilder">
        /// A builder used to create or modify options for this context.
        /// Databases (and other extensions) typically define extension methods on this object that allow you to configure the context.
        /// </param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseLazyLoadingProxies()
                    .UseSqlite("Data Source=db.sqlite3");
            }
            optionsBuilder.UseLazyLoadingProxies();
        }

        /// <summary>
        /// Configure the model that was discovered by convention from the entity types.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
        /// <remarks>If a model is explicitly set on the options for this context then this method will not be run.</remarks>
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