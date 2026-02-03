using System;
using System.Collections.Generic;

namespace RecursosHumanos.Models;

public partial class Correo
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Departamento { get; set; }

    public string? Correo1 { get; set; }

    public DateTime? FechaCreacion { get; set; }
}
