using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using HojeEuCaso.Sessions;
using Microsoft.AspNetCore.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Http;
using HojeEuCaso.Models;
using HojeEuCaso.Interfaces;
using HojeEuCaso.Services;
using AutoMapper;

namespace HojeEuCaso
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
            services.AddControllersWithViews();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20);
            });

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAuthenticatedUser", policy =>
                {
                    policy.RequireAuthenticatedUser();
                });
            });

            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<ICidadeService, CidadeService>();
            services.AddScoped<IEstadoService, EstadoService>();
            services.AddScoped<ITipoCategoriaService, TipoCategoriaService>();
            services.AddScoped<ICategoriaService, CategoriaService>();
            services.AddScoped<IPlanoService, PlanoService>();
            services.AddScoped<IClausulaContratoService, ClausulaContratoService>();
            services.AddScoped<ICategoriasDosPlanosService, CategoriasDosPlanosService>();
            services.AddScoped<IPacoteService, PacoteService>();
            services.AddScoped<IUsuarioSistemaService, UsuarioSistemaService>();
            services.AddScoped<IFornecedorService, FornecedorService>();
            services.AddScoped<IItensDePacotesService, ItensDePacotesService>();
            services.AddScoped<IFornecedorIndicadoService, FornecedorIndicadoService>();
            services.AddScoped<IItensDePacotesDeUsuariosService, ItensDePacotesDeUsuariosService>();
            services.AddScoped<IPacotesDeUsuariosService, PacotesDeUsuariosService>();
            services.AddScoped<IPaisService, PaisService>();

            services.AddScoped<ISessionUsuarioService, SessionUsuarioService>();

            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<HojeEuCasoDbContext>(options =>
                options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 1, 0))));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Login}/{action=Login}/{id?}");

            });

            app.Use(async (context, next) =>
            {
                if (context.Session.Keys.Count() == 0)
                {
                    context.Response.Redirect("/Login/Logout");
                    return;
                }

                await next();
            });

            app.UseStaticFiles();
        }
    }
}
