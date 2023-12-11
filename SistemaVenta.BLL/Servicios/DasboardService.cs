
using System.Globalization;
using APISistemaVenta.Models;
using APISistemaVenta.SistemaVenta.BLL.Servicios.Contrato;
using APISistemaVenta.SistemaVenta.DAL.Repositorios.Contrato;
using APISistemaVenta.SistemaVenta.DTO;
using AutoMapper;

namespace APISistemaVenta.SistemaVenta.BLL.Servicios
{
    public class DasboardService : IDashBoardService
    {
        private readonly IVentaRepository _ventaRepositorio;
        private readonly IGenericRepository<Producto> _productoRepositorio;
        private readonly IMapper _mapper;

        public DasboardService(IVentaRepository ventaRepositorio, IGenericRepository<Producto> productoRepositorio, IMapper mapper)
        {
            _ventaRepositorio = ventaRepositorio;
            _productoRepositorio = productoRepositorio;
            _mapper = mapper;
        }

        // Método privado para obtener las ventas en un rango de fechas
        private IQueryable<Venta> retornarVentas(IQueryable<Venta> tablaVenta, int restarCantidadDias)
        {
            // Obtener la última fecha de la tabla y restar la cantidad de días especificada
            DateTime? ultimaFecha = tablaVenta.OrderByDescending(v => v.FechaRegistro).Select(v => v.FechaRegistro).FirstOrDefault();
            ultimaFecha = ultimaFecha.Value.AddDays(restarCantidadDias);

            // Filtrar las ventas por fechas mayores o iguales a la última fecha calculada
            return tablaVenta.Where(v => v.FechaRegistro.Value.Date >= ultimaFecha.Value.Date);
        }

        // Método privado para obtener el total de ventas de la última semana
        private async Task<int> TotalVentasUltimaSemana()
        {
            int total = 0;

            // Consultar las ventas de la base de datos
            IQueryable<Venta> _ventaQuery = await _ventaRepositorio.Consultar();

            if (_ventaQuery.Count() > 0)
            {
                // Obtener las ventas de la última semana y contarlas
                var tablaVenta = retornarVentas(_ventaQuery, -7);
                total = tablaVenta.Count();
            }
            return total;
        }

        // Método privado para obtener el total de ingresos de la última semana
        private async Task<string> TotalIngresosUltimaSemana()
        {
            decimal resultado = 0;

            // Consultar las ventas de la base de datos
            IQueryable<Venta> _ventaQuery = await _ventaRepositorio.Consultar();

            if (_ventaQuery.Count() > 0)
            {
                // Obtener las ventas de la última semana y sumar los totales
                var tablaventa = retornarVentas(_ventaQuery, -7);
                resultado = tablaventa.Select(v => v.Total).Sum(v => v.Value);
            }
            // Convertir el resultado a string con formato colombiano
            return Convert.ToString(resultado, new CultureInfo("es-CO"));
        }

        // Método privado para obtener el total de productos
        private async Task<int> TotalProductos()
        {
            // Consultar los productos de la base de datos
            IQueryable<Producto> _productoQuery = await _productoRepositorio.Consultar();

            // Contar los productos
            int total = _productoQuery.Count();
            return total;
        }


        // Método privado para obtener un diccionario con el total de ventas por fecha de la última semana
        private async Task<Dictionary<string, int>> VentasUltimaSemana()
        {
            Dictionary<string, int> resultado = new Dictionary<string, int>();

            // Consultar las ventas de la base de datos
            IQueryable<Venta> _ventaQuery = await _ventaRepositorio.Consultar();

            // Verificar si existen ventas en la base de datos
            if (_ventaQuery.Count() > 0)
            {
                // Obtener las ventas de la última semana y agruparlas por fecha
                var tablaVenta = retornarVentas(_ventaQuery, -7);

                // Agrupar las ventas por fecha y contar la cantidad de ventas para cada fecha
                resultado = tablaVenta
                    .GroupBy(v => v.FechaRegistro.Value.Date)
                    .OrderBy(g => g.Key)
                    .Select(dv => new { fecha = dv.Key.ToString("dd/MM/yyyy"), total = dv.Count() })
                    .ToDictionary(keySelector: r => r.fecha, elementSelector: r => r.total);
            }
            // Devolver el diccionario con el total de ventas por fecha
            return resultado;
        }

        // Método público para obtener un resumen del dashboard
        public async Task<DashBoardDTO> Resumen()
        {
            // Inicializar el objeto DTO que almacenará el resumen del dashboard
            DashBoardDTO vmDashBoard = new DashBoardDTO();

            try
            {
                // Obtener el total de ventas de la última semana
                vmDashBoard.TotalVentas = await TotalVentasUltimaSemana();

                // Obtener el total de ingresos de la última semana
                vmDashBoard.TotalIngresos = await TotalIngresosUltimaSemana();

                // Obtener el total de productos
                vmDashBoard.TotalProductos = await TotalProductos();

                // Inicializar una lista para almacenar el detalle de las ventas de la última semana
                List<VentaSemanaDTO> listaVentaSemana = new List<VentaSemanaDTO>();

                // Recorrer el diccionario de ventas de la última semana y agregar cada elemento a la lista
                foreach (KeyValuePair<string, int> item in await VentasUltimaSemana())
                {
                    listaVentaSemana.Add(new VentaSemanaDTO()
                    {
                        Fecha = item.Key,
                        Total = item.Value
                    });
                }

                // Asignar la lista al objeto DTO
                vmDashBoard.VentasUltimaSemana = listaVentaSemana;
            }
            catch
            {
                throw;
            }

            // Devolver el objeto DTO con el resumen del dashboard
            return vmDashBoard;
        }
    }
}
