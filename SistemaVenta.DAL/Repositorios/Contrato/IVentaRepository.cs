using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APISistemaVenta.Models;

namespace APISistemaVenta.SistemaVenta.DAL.Repositorios.Contrato
{
    public interface IVentaRepository : IGenericRepository<Venta>
    {
        Task<Venta> Registrar(Venta modelo);
    }
}