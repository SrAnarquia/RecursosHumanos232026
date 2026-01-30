using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RecursosHumanos.Models.ViewModels.Vacaciones
{
    public class VacacionCreateVM
    {
        public Vacacion Nuevo { get; set; } = new();

        [ValidateNever]
        public List<SelectListItem> Razones { get; set; }
        [ValidateNever]
        public List<SelectListItem> Aprobaciones { get; set; }
    }
}
