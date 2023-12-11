
using APISistemaVenta.Models;
using APISistemaVenta.SistemaVenta.BLL.Servicios.Contrato;
using APISistemaVenta.SistemaVenta.DAL.Repositorios.Contrato;
using APISistemaVenta.SistemaVenta.DTO;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace APISistemaVenta.SistemaVenta.BLL.Servicios
{
    public class ProductoService : IProductoService
    {
        private readonly IGenericRepository<Producto> _productoRepositorio;
        private readonly IMapper _mapper;

        public ProductoService(IGenericRepository<Producto> productoRepositorio, IMapper mapper)
        {
            _productoRepositorio = productoRepositorio;
            _mapper = mapper;
        }

        // Método que devuelve una lista de objetos ProductoDTO.
        // Obtiene la lista de productos desde el repositorio y realiza el mapeo a DTO.
        public async Task<List<ProductoDTO>> Lista()
        {
            try
            {
                var queryProducto = await _productoRepositorio.Consultar();

                // Inclusión de la información de la categoría para cada producto.
                var listaProductos = queryProducto.Include(cat => cat.IdCategoriaNavigation).ToList();

                // Mapeo de la lista de productos a una lista de objetos ProductoDTO.
                return _mapper.Map<List<ProductoDTO>>(listaProductos.ToList());
            }
            catch
            {
                throw;
            }
        }

        // Método que crea un nuevo producto a partir de un objeto ProductoDTO.
        public async Task<ProductoDTO> Crear(ProductoDTO modelo)
        {
            try
            {
                var productoCreado = await _productoRepositorio.Crear(_mapper.Map<Producto>(modelo));

                // Verifica si el IdProducto del producto creado es válido.
                if (productoCreado.IdProducto == 0)
                    throw new TaskCanceledException("No se pudo crear producto");

                // Realiza el mapeo del producto creado a un objeto ProductoDTO y lo devuelve.
                return _mapper.Map<ProductoDTO>(productoCreado);
            }
            catch
            {
                throw;
            }
        }

        // Método que edita un producto a partir de un objeto ProductoDTO.
        public async Task<bool> Editar(ProductoDTO modelo)
        {
            try
            {
                // Mapea el objeto ProductoDTO a la entidad Producto.
                var productoModelo = _mapper.Map<Producto>(modelo);

                // Busca el producto en el repositorio por el ID proporcionado.
                var productoEncontrado = await _productoRepositorio.Obtener(u => u.IdProducto == productoModelo.IdProducto);

                if (productoEncontrado == null)
                    throw new TaskCanceledException("No producto no existe");

                // Actualiza las propiedades del producto encontrado con los valores del modelo.
                productoEncontrado.Nombre = productoModelo.Nombre;
                productoEncontrado.IdCategoria = productoModelo.IdCategoria;
                productoEncontrado.Stock = productoModelo.Stock;
                productoEncontrado.Precio = productoModelo.Precio;
                productoEncontrado.EsActivo = productoModelo.EsActivo;

                // Realiza la operación de edición en el repositorio.
                bool respuesta = await _productoRepositorio.Editar(productoEncontrado);

                if (!respuesta)
                    throw new TaskCanceledException("No se pudo editar producto");

                return respuesta;
            }
            catch
            {
                throw;
            }
        }

        // Método que elimina un producto según su ID.
        public async Task<bool> Eliminar(int id)
        {
            try
            {
                // Busca el producto en el repositorio por el ID proporcionado.
                var productoEncontrado = await _productoRepositorio.Obtener(p => p.IdProducto == id);

                // Si el producto no se encuentra, lanza una excepción.
                if (productoEncontrado == null)
                    throw new TaskCanceledException("El producto no existe");

                // Realiza la operación de eliminación en el repositorio.
                bool respuesta = await _productoRepositorio.Eliminar(productoEncontrado);

                if (!respuesta)
                    throw new TaskCanceledException("No se pudo eliminar producto");

                return respuesta;
            }
            catch
            {
                throw;
            }
        }
    }
}