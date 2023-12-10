using System;
using System.Collections.Generic;

namespace APISistemaVenta.Models;

public partial class Ventum
{
    public int IdVenta { get; set; }

    public string? NumeroDocumento { get; set; }

    public string? TipoPago { get; set; }

    public decimal? Total { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<DetalleVentum> DetalleVenta { get; } = new List<DetalleVentum>();
}
