using System;
using System.Collections.Generic;

namespace RecursosHumanos.Models;

public partial class CatalogoCurso
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public int? IdDepartamento { get; set; }

    public int? IdTipoCurso { get; set; }

    public int? IdNivel { get; set; }

    public string? Diploma { get; set; }

    public DateTime? FechaInicio { get; set; }

    public DateTime? FechaFinalizacion { get; set; }

    public DateTime? FechaExpiracion { get; set; }

    public DateTime FechaCreacion { get; set; }

    public virtual Departamento? IdDepartamentoNavigation { get; set; }

    public virtual CatalogoNivel? IdNivelNavigation { get; set; }

    public virtual TipoCurso? IdTipoCursoNavigation { get; set; }
}
