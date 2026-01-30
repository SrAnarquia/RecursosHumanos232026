using Microsoft.AspNetCore.Mvc.Rendering;

namespace RecursosHumanos.Models.ViewModels.Empleados
{
    public class PersonalIncidentesVM
    {
        // ===================== PERSONA =====================
        public int IdPersonal { get; set; }
        public byte[] FotoPersonal { get; set; }
        public string Nombre { get; set; }
        public string Departamento { get; set; }
        public string TipoEmpleado { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Estado { get; set; }

        // ===================== FILTROS =====================
        public string? FiltroNombreIncidente { get; set; }
        public DateTime? FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }

        // ===================== INCIDENTES =====================
        public List<IncidenteVM> Incidentes { get; set; } = new();

        // ===================== PAGINACIÓN =====================
        public int PaginaActual { get; set; }
        public int TotalPaginas { get; set; }

        // ===================== CREATE =====================
        public IncidenteCreateVM NuevoIncidente { get; set; }
    }

    public class IncidenteVM
    {
        public int Id { get; set; }
        public string NombreIncidente { get; set; }
        public string Descripcion { get; set; }
        public string Razon { get; set; }
        public DateTime FechaIncidente { get; set; }
        public string Evidencia { get; set; }
    }
}
