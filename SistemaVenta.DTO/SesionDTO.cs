
namespace APISistemaVenta.SistemaVenta.DTO
{
    public class SesionDTO
    {
        public int IdUsuario { get; set; }

        public string? NombreCompleto { get; set; }

        public string? Correo { get; set; }

        public int? RolDescripcion { get; set; }
    }
}