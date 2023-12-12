using APISistemaVenta.SistemaVenta.API.Utilidad;
using APISistemaVenta.SistemaVenta.BLL.Servicios.Contrato;
using APISistemaVenta.SistemaVenta.DTO;
using Microsoft.AspNetCore.Mvc;

namespace APISistemaVenta.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VentaController : ControllerBase
    {
        private readonly IVentaService _ventaServicio;

        public VentaController(IVentaService ventaServicio)
        {
            _ventaServicio = ventaServicio;
        }

        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar([FromBody] VentaDTO venta)
        {
            // Crear una instancia de la clase Response para manejar la respuesta de la API.
            var rsp = new Response<VentaDTO>();

            try
            {
                // Intentar guardar un nueva venta utilizando el servicio.
                rsp.status = true;
                rsp.value = await _ventaServicio.Registrar(venta);

            }
            catch (Exception ex)
            {
                // En caso de error, establecer el estado a false y almacenar el mensaje de error.
                rsp.status = false;
                rsp.msg = ex.Message;
            }
            return Ok(rsp);
        }


        [HttpGet]
        [Route("Historial")]
        public async Task<IActionResult> Historial(string buscarPor, string? numeroVenta, string? fechaInicio, string? fechaFin)
        {
            // Crear una instancia de la clase Response para manejar la respuesta de la API.
            var rsp = new Response<List<VentaDTO>>();
            
            //se asegura de que numeroVenta no sea nulo y, en caso de serlo, se le asigna una cadena vacía.
            numeroVenta = numeroVenta is null ? "" : numeroVenta;
            fechaInicio = fechaInicio is null ? "" : fechaInicio;
            fechaFin = fechaFin is null ? "" : fechaFin;

            try
            {
                // Intentar obtener el historial de ventas desde el servicio.
                rsp.status = true;
                rsp.value = await _ventaServicio.Historial(buscarPor, numeroVenta, fechaInicio, fechaFin);

            }
            catch (Exception ex)
            {
                // En caso de error, establecer el estado a false y almacenar el mensaje de error.
                rsp.status = false;
                rsp.msg = ex.Message;
            }
            return Ok(rsp);
        }


        [HttpGet]
        [Route("Reporte")]
        public async Task<IActionResult> Reporte(string? fechaInicio, string? fechaFin)
        {
            // Crear una instancia de la clase Response para manejar la respuesta de la API.
            var rsp = new Response<List<ReporteDTO>>();

            try
            {
                // Intentar obtener el reporte de ventas desde el servicio.
                rsp.status = true;
                rsp.value = await _ventaServicio.Reporte(fechaInicio, fechaFin);

            }
            catch (Exception ex)
            {
                // En caso de error, establecer el estado a false y almacenar el mensaje de error.
                rsp.status = false;
                rsp.msg = ex.Message;
            }
            return Ok(rsp);
        }
    }
}