using System;
using System.Collections.Generic;

namespace RecursosHumanos.Models;

public partial class DatosReclutamiento
{
    public int Id { get; set; }

    public string NombreCompleto { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string Comentarios { get; set; } = null!;

    public DateTime FechaCreación { get; set; }

    public int? IdEmpresa { get; set; }

    public int? IdBase { get; set; }

    public int? IdFuente { get; set; }

    public int? IdEstatus { get; set; }

    public int? IdPosicion { get; set; }

    public int? IdSexo { get; set; }

    public int? IdReclutador { get; set; }

    public DateTime FechaContacto { get; set; }

    public virtual Base? IdBaseNavigation { get; set; }

    public virtual Empresa? IdEmpresaNavigation { get; set; }

    public virtual Estatus? IdEstatusNavigation { get; set; }

    public virtual Fuente? IdFuenteNavigation { get; set; }

    public virtual Posicion? IdPosicionNavigation { get; set; }

    public virtual Reclutador? IdReclutadorNavigation { get; set; }

    public virtual Sexo? IdSexoNavigation { get; set; }
}
