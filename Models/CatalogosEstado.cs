using System;
using System.Collections.Generic;

namespace RecursosHumanos.Models;

public partial class CatalogosEstado
{
    public int Id { get; set; }

    public string? IdOrigen { get; set; }

    public string? Nombre { get; set; }

    public DateTime? FechaOrigen { get; set; }
}
