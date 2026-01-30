using System;
using System.Collections.Generic;

namespace RecursosHumanos.Models;

public partial class CursosPersona
{
    public int Id { get; set; }

    public int? IdPersona { get; set; }

    public int? IdCurso { get; set; }

    public string? NombreCurso { get; set; }

    public string? Descripcion { get; set; }

    public int? IdDepartamento { get; set; }

    public DateTime? FechaInicio { get; set; }

    public DateTime? FechaFinalizacion { get; set; }

    public int? IdEstatus { get; set; }

    public string? Diploma { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public virtual EstatusCurso? IdEstatusNavigation { get; set; }
}
