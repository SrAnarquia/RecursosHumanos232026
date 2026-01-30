using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecursosHumanos.Models.ViewModels.Catalogos
{
    public class CatalogoCursoCreateVM
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un departamento")]
        public int? IdDepartamento { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un nivel")]
        public int? IdNivel { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un tipo de curso")]
        public int? IdTipoCurso { get; set; }

        [DataType(DataType.Date)]
        public DateTime? FechaInicio { get; set; }

        [DataType(DataType.Date)]
        public DateTime? FechaFinalizacion { get; set; }

        [DataType(DataType.Date)]
        public DateTime? FechaExpiracion { get; set; }

        public IFormFile DiplomaFile { get; set; }

        // Combos
        public IEnumerable<SelectListItem> Departamentos { get; set; }
        public IEnumerable<SelectListItem> Niveles { get; set; }
        public IEnumerable<SelectListItem> TiposCurso { get; set; }
    }
}
