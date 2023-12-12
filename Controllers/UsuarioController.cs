using APISistemaVenta.SistemaVenta.API.Utilidad;
using APISistemaVenta.SistemaVenta.BLL.Servicios.Contrato;
using APISistemaVenta.SistemaVenta.DTO;
using Microsoft.AspNetCore.Mvc;

namespace APISistemaVenta.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioServicio;

        public UsuarioController(IUsuarioService usuarioServicio)
        {
            _usuarioServicio = usuarioServicio;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            // Crear una instancia de la clase Response para manejar la respuesta de la API.
            var rsp = new Response<List<UsuarioDTO>>();

            try
            {
                // Intentar obtener la lista de usuarios desde el servicio.
                rsp.status = true;
                rsp.value = await _usuarioServicio.Lista();

            }
            catch (Exception ex)
            {
                // En caso de error, establecer el estado a false y almacenar el mensaje de error.
                rsp.status = false;
                rsp.msg = ex.Message;
            }
            return Ok(rsp);
        }

        [HttpPost]
        [Route("IniciarSesion")]
        public async Task<IActionResult> IniciarSesion([FromBody] LoginDTO login)
        {
            // Crear una instancia de la clase Response para manejar la respuesta de la API.
            var rsp = new Response<SesionDTO>();

            try
            {
                // Intentar validar las credenciales del usuario utilizando el servicio.
                rsp.status = true;
                rsp.value = await _usuarioServicio.ValidarCredenciales(login.Correo, login.Clave);

            }
            catch (Exception ex)
            {
                // En caso de error, establecer el estado a false y almacenar el mensaje de error.
                rsp.status = false;
                rsp.msg = ex.Message;
            }
            return Ok(rsp);
        }

        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar([FromBody] UsuarioDTO usuario)
        {
            // Crear una instancia de la clase Response para manejar la respuesta de la API.
            var rsp = new Response<UsuarioDTO>();

            try
            {
                // Intentar crear un nuevo usuario utilizando el servicio.
                rsp.status = true;
                rsp.value = await _usuarioServicio.Crear(usuario);

            }
            catch (Exception ex)
            {
                // En caso de error, establecer el estado a false y almacenar el mensaje de error.
                rsp.status = false;
                rsp.msg = ex.Message;
            }
            return Ok(rsp);
        }

        [HttpPut]
        [Route("Editar")]
        public async Task<IActionResult> Editar([FromBody] UsuarioDTO usuario)
        {
            // Crear una instancia de la clase Response para manejar la respuesta de la API.
            var rsp = new Response<bool>();

            try
            {
                // Intentar editar el usuario utilizando el servicio.
                rsp.status = true;
                rsp.value = await _usuarioServicio.Editar(usuario);

            }
            catch (Exception ex)
            {
                // En caso de error, establecer el estado a false y almacenar el mensaje de error.
                rsp.status = false;
                rsp.msg = ex.Message;
            }
            return Ok(rsp);
        }

        [HttpDelete]
        [Route("Eliminar/{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            // Crear una instancia de la clase Response para manejar la respuesta de la API.
            var rsp = new Response<bool>();

            try
            {
                // Intentar eliminar el usuario utilizando el servicio.
                rsp.status = true;
                rsp.value = await _usuarioServicio.Eliminar(id);

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