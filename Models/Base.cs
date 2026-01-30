using System;
using System.Collections.Generic;

namespace RecursosHumanos.Models;

public partial class Base
{
    public int Id { get; set; }

    public string? Base1 { get; set; }

    public DateTime FechaCreación { get; set; }

    public virtual ICollection<DatosReclutamiento> DatosReclutamientos { get; set; } = new List<DatosReclutamiento>();
}
