
using APISistemaVenta.Models;
using APISistemaVenta.SistemaVenta.BLL.Servicios.Contrato;
using APISistemaVenta.SistemaVenta.DAL.Repositorios.Contrato;
using APISistemaVenta.SistemaVenta.DTO;
using AutoMapper;

namespace APISistemaVenta.SistemaVenta.BLL.Servicios
{
    public class RolService : IRolService
    {
        private readonly IGenericRepository<Rol> _rolRepositorio;
        private readonly IMapper _mapper;

        public RolService(IGenericRepository<Rol> rolRepositorio, IMapper mapper)
        {
            _rolRepositorio = rolRepositorio;
            _mapper = mapper;
        }

        // Obtiene la lista de roles del sistema en formato DTO.
        public async Task<List<RolDTO>> Lista()
        {
            try
            {
                // Consulta la lista de roles desde el repositorio.
                var listaRoles = await _rolRepositorio.Consultar();

                //Convertir de rol a RolDTO en forma de lista
                return _mapper.Map<List<RolDTO>>(listaRoles.ToList());
            }
            catch
            {
                throw;
            }
        }
    }
}