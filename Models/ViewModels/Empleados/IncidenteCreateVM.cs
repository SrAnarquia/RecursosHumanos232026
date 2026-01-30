using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace RecursosHumanos.Models.ViewModels.Empleados
{
    public class IncidenteCreateVM
    {
        // ===================== RELACIÓN =====================
        public int IdPersona { get; set; }

        // ===================== INCIDENTE =====================
        [Required(ErrorMessage = "El nombre del incidente es obligatorio")]
        [StringLength(255)]
        public string NombreIncidente { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria")]
        [StringLength(1000)]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Seleccione una razón")]
        public int? IdRazon { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria")]
        [DataType(DataType.Date)]
        public DateTime FechaIncidente { get; set; }

        [StringLength(500)]
        public string? Evidencia { get; set; }
      

        // 👉 ARCHIVO SUBIDO
        public IFormFile? EvidenciaFile { get; set; }




        // ===================== DROPDOWN =====================
        public List<SelectListItem> Razones { get; set; } = new();
    }
}
