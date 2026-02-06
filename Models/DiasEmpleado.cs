using System;
using System.Collections.Generic;

namespace RecursosHumanos.Models;

public partial class DiasEmpleado
{
    public int Id { get; set; }

    public int? IdEmpleado { get; set; }

    public int? TotalDias { get; set; }

    public int? DiasUsados { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public int? DiasRestantes { get; set; }

    public DateTime? FechaExpiracion { get; set; }
}
