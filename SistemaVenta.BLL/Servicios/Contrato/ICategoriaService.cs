
using APISistemaVenta.SistemaVenta.DTO;

namespace APISistemaVenta.SistemaVenta.BLL.Servicios.Contrato
{
    public interface ICategoriaService
    {
        Task<List<CategoriaDTO>> Lista();
    }
}