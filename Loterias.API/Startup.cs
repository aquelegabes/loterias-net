using Loterias.Application.Interfaces;
using Loterias.Application.Service;
using Loterias.Data.Context;
using Loterias.Data.Repositories;
using Loterias.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Loterias.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<LoteriasContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("Sqlite")));

            services.AddScoped(typeof(IRepositoryConcursoSena), typeof(RepositoryConcursoSena));
            services.AddScoped(typeof(IRepositoryConcursoLotofacil), typeof(RepositoryConcursoLotofacil));
            services.AddScoped(typeof(IRepositoryConcursoQuina), typeof(RepositoryConcursoQuina));
            services.AddScoped(typeof(IRepositoryGanhadoresFacil), typeof(RepositoryGanhadoresFacil));
            services.AddScoped(typeof(IRepositoryGanhadoresQuina), typeof(RepositoryGanhadoresQuina));
            services.AddScoped(typeof(IRepositoryGanhadoresSena), typeof(RepositoryGanhadoresSena));

            services.AddSingleton<ISenaService, SenaService>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
