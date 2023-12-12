using APISistemaVenta.SistemaVenta.API.Utilidad;
using APISistemaVenta.SistemaVenta.BLL.Servicios.Contrato;
using APISistemaVenta.SistemaVenta.DTO;
using Microsoft.AspNetCore.Mvc;

namespace APISistemaVenta.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolController : ControllerBase
    {
        private readonly IRolService _rolServicio;
        public RolController(IRolService rolServicio)
        {
            _rolServicio = rolServicio;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            // Crear una instancia de la clase para manejar la respuesta de la API.
            var rsp = new Response<List<RolDTO>>();

            try
            {
                // Intentar obtener la lista de roles desde el servicio.
                rsp.status = true;
                rsp.value = await _rolServicio.Lista();

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