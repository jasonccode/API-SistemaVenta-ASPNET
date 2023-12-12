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
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuServicio;

        public MenuController(IMenuService menuServicio)
        {
            _menuServicio = menuServicio;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista(int idUsuario)
        {
            // Crear una instancia de la clase para manejar la respuesta de la API.
            var rsp = new Response<List<MenuDTO>>();

            try
            {
                // Intentar obtener el menu por idusuario desde el servicio.
                rsp.status = true;
                rsp.value = await _menuServicio.Lista(idUsuario);

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