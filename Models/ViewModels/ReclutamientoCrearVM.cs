using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace RecursosHumanos.Models.ViewModels
{
    public class ReclutamientoCrearVM
    {
        // Datos del formulario
        public DatosReclutamiento Nuevo { get; set; } = new();

        // Combos
        public IEnumerable<SelectListItem> Empresas { get; set; }
        public IEnumerable<SelectListItem> Bases { get; set; }
        public IEnumerable<SelectListItem> Fuentes { get; set; }
        public IEnumerable<SelectListItem> Estatus { get; set; }
        public IEnumerable<SelectListItem> Posiciones { get; set; }
        public IEnumerable<SelectListItem> Sexos { get; set; }
        public IEnumerable<SelectListItem> Reclutadores { get; set; }

        // UI
        public bool MostrarModal { get; set; }
    }
}
