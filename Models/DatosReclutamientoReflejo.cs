using System;
using System.Collections.Generic;

namespace RecursosHumanos.Models;

public partial class DatosReclutamientoReflejo
{
    public int Id { get; set; }

    public string? NombreCompleto { get; set; }

    public string? Telefono { get; set; }

    public string? Comentarios { get; set; }

    public string? Empresa { get; set; }

    public string? Base { get; set; }

    public string? Fuente { get; set; }

    public string? Estatus { get; set; }

    public string? Posicion { get; set; }

    public string? Sexo { get; set; }

    public string? Reclutador { get; set; }
}
