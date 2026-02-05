using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecursosHumanos.Models;

public partial class Vacacion
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Departamento { get; set; }


    [Required(ErrorMessage = "Debe seleccionar una razón")]
    public int? IdRazon { get; set; }


    [Required(ErrorMessage = "La fecha de inicio es obligatoria")]
    public DateTime? FechaInicio { get; set; }


    [Required(ErrorMessage = "La fecha final es obligatoria")]
    public DateTime? FechaFinalizacion { get; set; }


    [Required(ErrorMessage = "Debe ingresar un detalle")]
    [StringLength(500)]
    public string? Detalles { get; set; }

    public int? IdAprobado { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public int? CreadoPor { get; set; }

    public virtual Aprobacion? IdAprobadoNavigation { get; set; }

    public virtual Razone? IdRazonNavigation { get; set; }

    /*public enum RazonVacaciones
    {
        Enfermedad = 1,
        AsuntosPersonales = 2,
        ActividadesRecreativas = 3,
        Otros = 4
    }*/
}
