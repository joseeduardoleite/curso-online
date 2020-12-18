using CursoOnline.Dados.Data;
using CursoOnline.Dados.Repositories;
using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.Cursos.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CursoOnline.Ioc
{
    public static class StartupIoc
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(x => x.UseMySql(configuration["ConnectionString"]));

            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));

            services.AddScoped(typeof(ICursoRepository), typeof(CursoRepository));

            services.AddScoped<ArmazenadorCurso>();
        }
    }
}