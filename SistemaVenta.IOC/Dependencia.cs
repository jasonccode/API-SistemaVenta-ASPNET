
using APISistemaVenta.SistemaVenta.BLL.Servicios;
using APISistemaVenta.SistemaVenta.BLL.Servicios.Contrato;
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
            // Configuración de DbContext para la conexión a la base de datos utilizando Npgsql.
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

            // Registro de servicios específicos del dominio mediante inyección de dependencias.
            services.AddScoped<IRolService, RolService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<ICategoriaService, CategoriaService>();
            services.AddScoped<IProductoService, ProductoService>();
            services.AddScoped<IVentaService, VentaService>();
            services.AddScoped<IDashBoardService, DasboardService>();
            services.AddScoped<IMenuService, MenuService>();


        }
    }
}