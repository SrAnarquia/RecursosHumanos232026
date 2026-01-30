using Microsoft.AspNetCore.Mvc.Rendering;

namespace RecursosHumanos.Models.ViewModels.Reclutamiento
{
    public class ReclutamientoEstadisticasVM
    {
        // ================= FILTROS =================
        // ===== FILTROS =====
        public int Anio { get; set; }
        public int Mes { get; set; }

        public List<SelectListItem> Anios { get; set; } = new();
        public List<SelectListItem> Meses { get; set; } = new();

        // ===== GRÁFICAS =====
        public int TotalContratados { get; set; }
        public int TotalNoContratados { get; set; }

        public Dictionary<string, int> RazonesNoContratacion { get; set; } = new();
    }
}
