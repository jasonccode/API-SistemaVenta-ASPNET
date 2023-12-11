
using APISistemaVenta.Models;
using APISistemaVenta.SistemaVenta.BLL.Servicios.Contrato;
using APISistemaVenta.SistemaVenta.DAL.Repositorios.Contrato;
using APISistemaVenta.SistemaVenta.DTO;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace APISistemaVenta.SistemaVenta.BLL.Servicios
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IGenericRepository<Usuario> _UsuarioRepositorio;
        private readonly IMapper _mapper; //Objeto para realizar mapeo de objetos entre modelos y DTOs.

        public UsuarioService(IGenericRepository<Usuario> usuarioRepositorio, IMapper mapper)
        {
            _UsuarioRepositorio = usuarioRepositorio;
            _mapper = mapper;
        }

        // Método que devuelve una lista de usuarios en formato DTO.
        public async Task<List<UsuarioDTO>> Lista()
        {
            try
            {
                // Consulta todos los usuarios incluyendo información relacionada con el rol.
                var queryUsuario = await _UsuarioRepositorio.Consultar();
                var listaUsuarios = queryUsuario.Include(rol => rol.IdRolNavigation).ToList();

                // Mapea la lista de usuarios a una lista de DTOs y la devuelve.
                return _mapper.Map<List<UsuarioDTO>>(listaUsuarios);
            }
            catch
            {
                throw;
            }
        }

        // Método que valida las credenciales de un usuario mediante su correo y clave.
        public async Task<SesionDTO> ValidarCredenciales(string correo, string clave)
        {
            try
            {
                var queryUsuario = await _UsuarioRepositorio.Consultar(u =>
                u.Correo == correo &&
                u.Clave == clave
                );

                if (queryUsuario.FirstOrDefault() == null)
                    throw new TaskCanceledException("El usuario no existe");

                // Obtiene el primer usuario encontrado, incluyendo información relacionada con el rol.
                Usuario devolverUsuario = queryUsuario.Include(rol => rol.IdRolNavigation).First();

                // Mapea el usuario a un objeto DTO de sesión y lo devuelve.
                return _mapper.Map<SesionDTO>(devolverUsuario);
            }
            catch
            {
                throw;
            }
        }

        // Método que crea un nuevo usuario a partir de un objeto UsuarioDTO proporcionado.
        public async Task<UsuarioDTO> Crear(UsuarioDTO modelo)
        {
            try
            {
                // Convierte el objeto UsuarioDTO a un objeto Usuario utilizando AutoMapper y lo crea en el repositorio.
                var usuarioCreado = await _UsuarioRepositorio.Crear(_mapper.Map<Usuario>(modelo));

                if (usuarioCreado.IdUsuario == 0)
                    throw new TaskCanceledException("No se pudo crear");

                // Consulta el usuario recién creado, incluyendo información relacionada con el rol.
                var query = await _UsuarioRepositorio.Consultar(u => u.IdUsuario == usuarioCreado.IdUsuario);

                // Obtiene el primer usuario encontrado con información de rol incluida.
                usuarioCreado = query.Include(rol => rol.IdRolNavigation).First();

                return _mapper.Map<UsuarioDTO>(usuarioCreado);
            }
            catch
            {
                throw;
            }
        }

        // Método que edita la información de un usuario a partir de un objeto UsuarioDTO proporcionado.
        public async Task<bool> Editar(UsuarioDTO modelo)
        {
            try
            {
                var usuarioModelo = _mapper.Map<Usuario>(modelo);

                // Obtiene el usuario existente del repositorio según su IdUsuario.
                var usuarioEncontrado = await _UsuarioRepositorio.Obtener(u => u.IdUsuario == usuarioModelo.IdUsuario);

                if (usuarioEncontrado == null)
                    throw new TaskCanceledException("El usuario no existe");

                // Actualiza la información del usuario existente con la información del UsuarioDTO proporcionado.
                usuarioEncontrado.NombreCompleto = usuarioModelo.NombreCompleto;
                usuarioEncontrado.Correo = usuarioModelo.Correo;
                usuarioEncontrado.IdRol = usuarioModelo.IdRol;
                usuarioEncontrado.Clave = usuarioModelo.Clave;
                usuarioEncontrado.EsActivo = usuarioModelo.EsActivo;

                // Intenta editar el usuario en el repositorio y guarda la respuesta.
                bool respuesta = await _UsuarioRepositorio.Editar(usuarioEncontrado);

                // Si la edición no fue exitosa, se lanza una excepción indicando que el usuario no existe.
                if (!respuesta)
                    throw new TaskCanceledException("El usuario no existe");

                // Devuelve la respuesta de la edición.
                return respuesta;
            }
            catch
            {
                throw;
            }
        }


        // Método que elimina un usuario según su IdUsuario.
        public async Task<bool> Eliminar(int id)
        {
            try
            {
                // Busca el usuario en el repositorio según su IdUsuario.
                var usuarioEncontrado = await _UsuarioRepositorio.Obtener(u => u.IdUsuario == id);

                if (usuarioEncontrado == null)
                    throw new TaskCanceledException("El usuario no existe");

                // Intenta eliminar el usuario del repositorio y guarda la respuesta.
                bool respuesta = await _UsuarioRepositorio.Eliminar(usuarioEncontrado);

                // Si la eliminación no fue exitosa, se lanza una excepción indicando que no se pudo eliminar.
                if (!respuesta)
                    throw new TaskCanceledException("No se pudo eliminar");

                // Devuelve la respuesta de la eliminación.
                return respuesta;
            }
            catch
            {
                throw;
            }
        }
    }
}