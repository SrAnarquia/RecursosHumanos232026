using System;
using System.Collections.Generic;

namespace RecursosHumanos.Models;

public partial class Aprobacion
{
    public int Id { get; set; }

    public int? IdPersona { get; set; }

    public bool? Estatus { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public virtual ICollection<Vacacion> Vacacions { get; set; } = new List<Vacacion>();
}
