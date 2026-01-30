using Microsoft.AspNetCore.Mvc.Rendering;

namespace RecursosHumanos.Models.ViewModels.Catalogos
{
    public class CatalogoCursosIndexVM
    {
        public List<CatalogoCurso> Datos { get; set; }

        // ===== FILTROS =====
        public string Nombre { get; set; }
        public int? IdDepartamento { get; set; }
        public int? IdNivel { get; set; }
        public int? IdTipoCurso { get; set; }

        public DateTime? FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }

        // ===== PAGINACIÓN =====
        public int PaginaActual { get; set; }
        public int TotalPaginas { get; set; }

        // ===== COMBOS INDEX =====
        public IEnumerable<SelectListItem> Departamentos { get; set; }
        public IEnumerable<SelectListItem> Niveles { get; set; }
        public IEnumerable<SelectListItem> TiposCurso { get; set; }

        // ✅ ESTE ES EL CAMBIO CLAVE
        public CatalogoCursoCreateVM Nuevo { get; set; }
    }
}
