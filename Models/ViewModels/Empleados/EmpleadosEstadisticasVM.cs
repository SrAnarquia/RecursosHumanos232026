using Microsoft.AspNetCore.Mvc.Rendering;

namespace RecursosHumanos.Models.ViewModels.Empleados
{
    public class EmpleadosEstadisticasVM
    {
        public int Anio { get; set; }
        public int Mes { get; set; }

        public Dictionary<string, int> CursosPorMes { get; set; }
        public int TotalCursos { get; set; }

        public Dictionary<string, int> VacacionesPorMes { get; set; }
        public Dictionary<string, int> RazonesVacaciones { get; set; }

        public Dictionary<string, int> IncidentesPorMes { get; set; }
        public Dictionary<string, int> RazonesIncidentes { get; set; }

        public List<SelectListItem> Anios { get; set; }
        public List<SelectListItem> Meses { get; set; }
    }

}
