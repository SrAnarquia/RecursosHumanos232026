using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace RecursosHumanos.Models.ViewModels.Empleados
{
    public class CursoPersonaCreateVM
    {
        public int IdPersona { get; set; }

        [Required(ErrorMessage = "El nombre del curso es obligatorio.")]
        public string NombreCurso { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "La fecha de inicio es obligatoria.")]
        public DateTime? FechaInicio { get; set; }

        [Required(ErrorMessage = "La fecha de finalización es obligatoria.")]
        public DateTime? FechaFinalizacion { get; set; }

        [Required(ErrorMessage = "El estatus es obligatorio.")]
        public int? IdEstatus { get; set; }  // <-- nullable para poder validar

        [ValidateNever] 
        public List<SelectListItem> Estatus { get; set; }

        [ValidateNever]
        public string Diploma { get; set; } // ruta en DB

        [ValidateNever]
        public IFormFile DiplomaFile { get; set; } // archivo subido

    }
}
