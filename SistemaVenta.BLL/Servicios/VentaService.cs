
using System.Globalization;
using APISistemaVenta.Models;
using APISistemaVenta.SistemaVenta.BLL.Servicios.Contrato;
using APISistemaVenta.SistemaVenta.DAL.Repositorios.Contrato;
using APISistemaVenta.SistemaVenta.DTO;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace APISistemaVenta.SistemaVenta.BLL.Servicios
{
    public class VentaService : IVentaService
    {
        private readonly IVentaRepository _ventaRepositorio;
        private readonly IGenericRepository<DetalleVenta> _detalleVentaRepositorio;
        private readonly IMapper _mapper;

        public VentaService(IVentaRepository ventaRepositorio, IGenericRepository<DetalleVenta> detalleVentaRepositorio, IMapper mapper)
        {
            _ventaRepositorio = ventaRepositorio;
            _detalleVentaRepositorio = detalleVentaRepositorio;
            _mapper = mapper;
        }

        // Método asincrónico que registra una venta.
        public async Task<VentaDTO> Registrar(VentaDTO modelo)
        {
            try
            {
                // Llama al método de registro en el repositorio de ventas.
                var ventaGenerada = await _ventaRepositorio.Registrar(_mapper.Map<Venta>(modelo));

                if (ventaGenerada.IdVenta == 0)
                    throw new TaskCanceledException("No se pudo crear");

                // Mapea la venta generada a un objeto VentaDTO y lo devuelve.
                return _mapper.Map<VentaDTO>(ventaGenerada);
            }
            catch
            {
                throw;
            }
        }

        // Método que proporciona un historial de ventas según los parámetros de búsqueda.
        public async Task<List<VentaDTO>> Historial(string buscarPor, string numeroVenta, string fechaInicio, string fechaFin)
        {
            // Consulta inicial que incluye detalles y productos relacionados con las ventas.
            IQueryable<Venta> query = await _ventaRepositorio.Consultar();
            var ListaResultado = new List<Venta>();
            try
            {
                // Verifica el criterio de búsqueda y realiza la consulta correspondiente.
                if (buscarPor == "fecha")
                {
                    DateTime fech_inicio = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", new CultureInfo("es-CO"));
                    DateTime fech_fin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("es-CO"));

                    // Realiza la consulta filtrando por el rango de fechas.
                    ListaResultado = await query.Where(v =>
                    v.FechaRegistro.Value.Date >= fech_inicio.Date &&
                    v.FechaRegistro.Value.Date <= fech_fin.Date)
                    .Include(dv => dv.DetalleVenta)
                    .ThenInclude(p => p.IdProductoNavigation)
                    .ToListAsync();
                }
                else
                {
                    // Realiza la consulta filtrando por el número de venta.
                    ListaResultado = await query.Where(v => v.NumeroDocumento == numeroVenta)
                    .Include(dv => dv.DetalleVenta)
                    .ThenInclude(p => p.IdProductoNavigation)
                    .ToListAsync();
                }
            }
            catch
            {
                throw;
            }
            // Mapea la lista de ventas resultante a una lista de objetos VentaDTO y la devuelve.
            return _mapper.Map<List<VentaDTO>>(ListaResultado);
        }



        //Método Reporte esta destinado a recuperar datos relacionados con ventas en un rango de fechas dado.
        public async Task<List<ReporteDTO>> Reporte(string fechaInicio, string fechaFin)
        {
            // Se inicia con una consulta de DetalleVenta, que representa los detalles de ventas.
            IQueryable<DetalleVenta> query = await _detalleVentaRepositorio.Consultar();

            // Lista donde se almacenarán los resultados de la consulta.
            var ListaResultado = new List<DetalleVenta>();

            try
            {
                // Convertir las cadenas de fecha a objetos DateTime utilizando un formato específico.
                DateTime fech_inicio = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", new CultureInfo("es-CO"));
                DateTime fech_fin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("es-CO"));

                // Se ejecuta la consulta, incluyendo las relaciones de Producto y Venta, y filtrando por el rango de fechas.
                ListaResultado = await query
                .Include(p => p.IdProductoNavigation)  // Incluir datos relacionados con el Producto.
                .Include(v => v.IdVentaNavigation)    // Incluir datos relacionados con la Venta.
                .Where(dv =>
                dv.IdVentaNavigation.FechaRegistro.Value.Date >= fech_inicio.Date &&
                dv.IdVentaNavigation.FechaRegistro.Value.Date <= fech_fin.Date
                ).ToListAsync();
            }
            catch
            {
                throw;
            }
            // Mapear los resultados a la clase DTO antes de devolverlos.
            return _mapper.Map<List<ReporteDTO>>(ListaResultado);
        }
    }
}