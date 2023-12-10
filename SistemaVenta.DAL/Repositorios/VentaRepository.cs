using APISistemaVenta.Models;
using APISistemaVenta.SistemaVenta.DAL.DbContext;
using APISistemaVenta.SistemaVenta.DAL.Repositorios.Contrato;

namespace APISistemaVenta.SistemaVenta.DAL.Repositorios
{
    public class VentaRepository : GenericRepository<Venta>, IVentaRepository
    {
        private readonly DbventaContext _dbcontext;

        public VentaRepository(DbventaContext dbcontext) : base(dbcontext)
        {
            _dbcontext = dbcontext;
        }

        // Registra una nueva venta en la base de datos, actualizando el consecutivo de documentos y generando un número de venta.
        public async Task<Venta> Registrar(Venta modelo)
        {
            Venta ventaGenerada = new Venta();
            using (var transaction = _dbcontext.Database.BeginTransaction())
            {
                try
                {
                    // Actualizar la cantidad de productos en stock en base a los detalles de venta.
                    foreach (DetalleVenta dv in modelo.DetalleVenta)
                    {

                        Producto producto_encontrado = _dbcontext.Productos.Where(p => p.IdProducto == dv.IdProducto).First();
                        _dbcontext.Productos.Update(producto_encontrado);
                    }
                    await _dbcontext.SaveChangesAsync();

                    // Obtener y actualizar el consecutivo de documentos.
                    NumeroDocumento consecutivo = _dbcontext.NumeroDocumentos.First();
                    consecutivo.UltimoNumero = consecutivo.UltimoNumero + 1;
                    consecutivo.FechaRegistro = DateTime.Now;
                    _dbcontext.NumeroDocumentos.Update(consecutivo);
                    await _dbcontext.SaveChangesAsync();

                    // Generar el número de venta con ceros a la izquierda.
                    int CantidadDigitos = 4;
                    string ceros = string.Concat(Enumerable.Repeat("0", CantidadDigitos));
                    string numeroVenta = ceros + consecutivo.UltimoNumero.ToString();
                    numeroVenta = numeroVenta.Substring(numeroVenta.Length - CantidadDigitos, CantidadDigitos);
                    modelo.NumeroDocumento = numeroVenta;

                    // Agregar la venta a la base de datos.
                    await _dbcontext.Venta.AddAsync(modelo);
                    await _dbcontext.SaveChangesAsync();
                    ventaGenerada = modelo;

                    // Confirmar la transacción.
                    transaction.Commit();

                }
                catch
                {
                    // Revertir la transacción en caso de error.
                    transaction.Rollback();
                    throw;
                }
                return ventaGenerada;
            }
        }
    }
}