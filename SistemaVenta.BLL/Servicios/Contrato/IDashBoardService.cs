
using APISistemaVenta.SistemaVenta.DTO;

namespace APISistemaVenta.SistemaVenta.BLL.Servicios.Contrato
{
    public interface IDashBoardService
    {
        Task<DashBoardDTO> Resumen();
    }
}