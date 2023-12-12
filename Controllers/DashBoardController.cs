using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APISistemaVenta.SistemaVenta.API.Utilidad;
using APISistemaVenta.SistemaVenta.BLL.Servicios.Contrato;
using APISistemaVenta.SistemaVenta.DTO;
using Microsoft.AspNetCore.Mvc;

namespace APISistemaVenta.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashBoardController : ControllerBase
    {
        private readonly IDashBoardService _dashboardServicio;

        public DashBoardController(IDashBoardService dashboardServicio)
        {
            _dashboardServicio = dashboardServicio;
        }


        [HttpGet]
        [Route("Resumen")]
        public async Task<IActionResult> Resumen()
        {
            // Crear una instancia de la clase para manejar la respuesta de la API.
            var rsp = new Response<DashBoardDTO>();

            try
            {
                // Intentar obtener el resumen del dashboard desde el servicio.
                rsp.status = true;
                rsp.value = await _dashboardServicio.Resumen();

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