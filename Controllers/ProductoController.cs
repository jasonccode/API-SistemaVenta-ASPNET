
using APISistemaVenta.SistemaVenta.API.Utilidad;
using APISistemaVenta.SistemaVenta.BLL.Servicios.Contrato;
using APISistemaVenta.SistemaVenta.DTO;
using Microsoft.AspNetCore.Mvc;

namespace APISistemaVenta.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoService _productoServicio;

        public ProductoController(IProductoService productoServicio)
        {
            _productoServicio = productoServicio;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            // Crear una instancia de la clase para manejar la respuesta de la API.
            var rsp = new Response<List<ProductoDTO>>();

            try
            {
                // Intentar obtener la lista de productos desde el servicio.
                rsp.status = true;
                rsp.value = await _productoServicio.Lista();

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
        public async Task<IActionResult> Guardar([FromBody] ProductoDTO producto)
        {
            // Crear una instancia de la clase para manejar la respuesta de la API.
            var rsp = new Response<ProductoDTO>();

            try
            {
                // Intentar crear un nuevo producto utilizando el servicio.
                rsp.status = true;
                rsp.value = await _productoServicio.Crear(producto);

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
        public async Task<IActionResult> Editar([FromBody] ProductoDTO producto)
        {
            // Crear una instancia de la clase para manejar la respuesta de la API.
            var rsp = new Response<bool>();

            try
            {
                // Intentar editar el producto utilizando el servicio.
                rsp.status = true;
                rsp.value = await _productoServicio.Editar(producto);

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
            // Crear una instancia de la clase para manejar la respuesta de la API.
            var rsp = new Response<bool>();

            try
            {
                // Intentar eliminar el producto utilizando el servicio.
                rsp.status = true;
                rsp.value = await _productoServicio.Eliminar(id);

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