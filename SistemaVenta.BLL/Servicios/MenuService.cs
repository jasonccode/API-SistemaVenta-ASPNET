using APISistemaVenta.Models;
using APISistemaVenta.SistemaVenta.BLL.Servicios.Contrato;
using APISistemaVenta.SistemaVenta.DAL.Repositorios.Contrato;
using APISistemaVenta.SistemaVenta.DTO;
using AutoMapper;

namespace APISistemaVenta.SistemaVenta.BLL.Servicios
{
    public class MenuService : IMenuService
    {
        private readonly IGenericRepository<Usuario> _usuarioRepositorio;
        private readonly IGenericRepository<MenuRol> _menuRolRepositorio;
        private readonly IGenericRepository<Menu> _menuRepositorio;
        private readonly IMapper _mapper;

        public MenuService(
                   IGenericRepository<Usuario> usuarioRepositorio,
                   IGenericRepository<MenuRol> menuRolRepositorio,
                   IGenericRepository<Menu> menuRepositorio,
                   IMapper mapper)
        {
            // Inicializar los repositorios y el mapeador a través del constructor
            _usuarioRepositorio = usuarioRepositorio;
            _menuRolRepositorio = menuRolRepositorio;
            _menuRepositorio = menuRepositorio;
            _mapper = mapper;
        }

        /// Obtiene la lista de menús disponibles para un usuario específico.
        public async Task<List<MenuDTO>> Lista(int idUsuario)
        {
            // Consultar el usuario, los roles asociados y los menús correspondientes
            IQueryable<Usuario> tbUsuario = await _usuarioRepositorio.Consultar(u => u.IdUsuario == idUsuario);
            IQueryable<MenuRol> tbMenuRol = await _menuRolRepositorio.Consultar();
            IQueryable<Menu> tbMenu = await _menuRepositorio.Consultar();

            try
            {
                // Realizar una consulta LINQ para obtener los menús asociados al usuario a través de sus roles
                IQueryable<Menu> tbResultado = (from u in tbUsuario
                                                join mr in tbMenuRol on u.IdRol equals mr.IdRol
                                                join m in tbMenu on mr.IdMenu equals m.IdMenu
                                                select m).AsQueryable();

                // Convertir la lista de menús resultante y devolverla
                var listaMenus = tbResultado.ToList();
                return _mapper.Map<List<MenuDTO>>(listaMenus);
            }
            catch
            {
                throw;
            }
        }
    }
}