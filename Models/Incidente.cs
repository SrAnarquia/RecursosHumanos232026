using System;
using System.Collections.Generic;

namespace RecursosHumanos.Models;

public partial class Incidente
{
    public int Id { get; set; }

    public int? IdPersona { get; set; }

    public string? NombreIncidente { get; set; }

    public string? Descripcion { get; set; }

    public int? IdDepartamento { get; set; }

    public DateTime? FechaIncidente { get; set; }

    public int? IdRazon { get; set; }

    public string? Evidencia { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public virtual RazonesIncidente? IdRazonNavigation { get; set; }
}
