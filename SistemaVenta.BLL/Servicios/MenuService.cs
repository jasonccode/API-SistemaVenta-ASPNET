using APISistemaVenta.Models;
using APISistemaVenta.SistemaVenta.BLL.Servicios.Contrato;
using APISistemaVenta.SistemaVenta.DAL.Repositorios.Contrato;
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
    }
}