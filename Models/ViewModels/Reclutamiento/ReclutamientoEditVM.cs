using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace RecursosHumanos.Models.ViewModels.Reclutamiento
{
    public class ReclutamientoEditVM
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Rellenar campo")]
        public string NombreCompleto { get; set; }

        public string? Telefono { get; set; }
        public string? Comentarios { get; set; }

        public int? IdEmpresa { get; set; }
        public int? IdBase { get; set; }
        public int? IdFuente { get; set; }
        public int? IdEstatus { get; set; }
        public int? IdPosicion { get; set; }
        public int? IdSexo { get; set; }
        public int? IdReclutador { get; set; }

        [Required(ErrorMessage = "Rellenar campo")]
        [DataType(DataType.Date)]
        public DateTime? FechaContacto { get; set; }

        // Combos
        public IEnumerable<SelectListItem> Empresas { get; set; } = Enumerable.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> Bases { get; set; } = Enumerable.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> Fuentes { get; set; } = Enumerable.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> Estatus { get; set; } = Enumerable.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> Posiciones { get; set; } = Enumerable.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> Sexos { get; set; } = Enumerable.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> Reclutadores { get; set; } = Enumerable.Empty<SelectListItem>();
    }
}
