using System;
using System.Collections.Generic;

namespace RecursosHumanos.Models;

public partial class CatalogosDepartamento
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public DateTime? FechaCreacion { get; set; }
}
