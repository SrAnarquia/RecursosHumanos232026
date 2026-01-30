using System;
using System.Collections.Generic;

namespace RecursosHumanos.Models;

public partial class EstatusCurso
{
    public int Id { get; set; }

    public string? Estatus { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public virtual ICollection<CursosPersona> CursosPersonas { get; set; } = new List<CursosPersona>();
}
