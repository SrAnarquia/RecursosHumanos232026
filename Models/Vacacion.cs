using System;
using System.Collections.Generic;

namespace RecursosHumanos.Models;

public partial class Vacacion
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Departamento { get; set; }

    public string? Detalles { get; set; }

    public DateTime? FechaInicio { get; set; }

    public DateTime? FechaFinalizacion { get; set; }

    public int? IdRazon { get; set; }

    public int? IdAprobado { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public int? CreadoPor { get; set; }

    public virtual Aprobacion? IdAprobadoNavigation { get; set; }

    public virtual Razone? IdRazonNavigation { get; set; }
}
