using System;
using System.Collections.Generic;

namespace RecursosHumanos.Models;

public partial class Razone
{
    public int Id { get; set; }

    public string? Razon { get; set; }

    public virtual ICollection<Vacacion> Vacacions { get; set; } = new List<Vacacion>();
}
