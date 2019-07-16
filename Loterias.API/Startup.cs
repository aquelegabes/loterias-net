using AutoMapper;
using Loterias.Application.AutoMapper;
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
using Swashbuckle.AspNetCore.Swagger;
using System;

namespace Loterias.API
{
#pragma warning disable CS1591
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
            #region DbContext
            services.AddDbContext<LoteriasContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("Sqlite")));
            #endregion

            #region Repositories
            services.AddScoped(typeof(IRepositoryConcursoSena), typeof(RepositoryConcursoSena));
            services.AddScoped(typeof(IRepositoryConcursoLotofacil), typeof(RepositoryConcursoLotofacil));
            services.AddScoped(typeof(IRepositoryConcursoQuina), typeof(RepositoryConcursoQuina));
            services.AddScoped(typeof(IRepositoryGanhadoresFacil), typeof(RepositoryGanhadoresFacil));
            services.AddScoped(typeof(IRepositoryGanhadoresQuina), typeof(RepositoryGanhadoresQuina));
            services.AddScoped(typeof(IRepositoryGanhadoresSena), typeof(RepositoryGanhadoresSena));
            #endregion

            #region Services
            services.AddScoped(typeof(ISenaService), typeof(SenaService));
            #endregion

            #region AutoMapper
            var config = new MapperConfiguration(cfg => cfg.AddProfile(new AutoMapperProfile()));

            var mapper = config.CreateMapper();

            services.AddSingleton(mapper);
            #endregion

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Loterias API", Version = "v1" });
                c.IncludeXmlComments(Configuration.GetValue<string>("XmlDocumentation"));
            });
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

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Loterias API V1");
                // c.RoutePrefix = string.Empty;
            });

            app.UseMvc();
        }
    }
}
