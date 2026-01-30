using System;
using System.Collections.Generic;

namespace RecursosHumanos.Models;

public partial class Reclutador
{
    public int Id { get; set; }

    public string? NombreCompleto { get; set; }

    public DateTime FechaCreación { get; set; }

    public virtual ICollection<DatosReclutamiento> DatosReclutamientos { get; set; } = new List<DatosReclutamiento>();
}
