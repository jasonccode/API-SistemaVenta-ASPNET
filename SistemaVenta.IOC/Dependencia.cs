
using APISistemaVenta.SistemaVenta.DAL.DbContext;
using APISistemaVenta.SistemaVenta.DAL.Repositorios;
using APISistemaVenta.SistemaVenta.DAL.Repositorios.Contrato;
using APISistemaVenta.SistemaVenta.Utility;
using Microsoft.EntityFrameworkCore;

namespace APISistemaVenta.SistemaVenta.IOC
{
    //Clase estática que proporciona métodos para inyectar dependencias en la capa de IOC (Inversión de Control).
    public static class Dependencia
    {
        public static void InyectarDependencias(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DbventaContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("Postgres"));
            });

            // Registrar implementaciones para la interfaz IGenericRepository<> y IVentaRepository.
            // Esto permite que estas implementaciones se resuelvan automáticamente cuando se solicitan.

            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IVentaRepository, VentaRepository>();

            // Configurar AutoMapper usando el perfil definido en AutoMapperProfile.
            services.AddAutoMapper(typeof(AutoMapperProfile));
        }
    }
}