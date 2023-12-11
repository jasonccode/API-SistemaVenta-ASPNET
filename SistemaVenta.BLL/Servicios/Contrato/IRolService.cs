using APISistemaVenta.SistemaVenta.DTO;

namespace APISistemaVenta.SistemaVenta.BLL.Servicios.Contrato
{
    public interface IRolService
    {
        Task<List<RolDTO>> Lista();
    }
}