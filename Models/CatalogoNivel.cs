using System;
using System.Collections.Generic;

namespace RecursosHumanos.Models;

public partial class CatalogoNivel
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public DateTime FechaCreacion { get; set; }

    public virtual ICollection<CatalogoCurso> CatalogoCursos { get; set; } = new List<CatalogoCurso>();
}
