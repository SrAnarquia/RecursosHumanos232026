using System;
using System.Collections.Generic;

namespace RecursosHumanos.Models;

public partial class RazonesIncidente
{
    public int Id { get; set; }

    public string? Razon { get; set; }

    public virtual ICollection<Incidente> Incidentes { get; set; } = new List<Incidente>();
}
