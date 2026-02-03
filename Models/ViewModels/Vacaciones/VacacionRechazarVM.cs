using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RecursosHumanos.Models.ViewModels.Vacaciones
{
    public class VacacionRechazarVM
    {
        public int Id { get; set; }

        public string Nombre { get; set; }
        public string Departamento { get; set; }

        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFinalizacion { get; set; }

        public string Razon { get; set; }
        public string Detalles { get; set; }

    }
}
