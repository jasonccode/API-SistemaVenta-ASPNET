
using APISistemaVenta.SistemaVenta.DAL.DbContext;
using Microsoft.EntityFrameworkCore;

namespace APISistemaVenta.SistemaVenta.IOC
{
    public static class Dependencia
    {
        public static void InyectarDependencias(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DbventaContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("Postgres"));
            });
        }
    }
}