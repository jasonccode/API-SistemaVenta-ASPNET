using APISistemaVenta.SistemaVenta.DTO;

namespace APISistemaVenta.SistemaVenta.BLL.Servicios.Contrato
{
    public interface IMenuService
    {
        Task<List<MenuDTO>> Lista(int idUsuario);
    }
}