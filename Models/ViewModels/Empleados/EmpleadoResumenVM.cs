using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;


namespace RecursosHumanos.Models.ViewModels.Empleados
{
    public class EmpleadoResumenVM
    {
        // ===================== EMPLEADO =====================
        public int IdPersonal { get; set; }
        public byte[] FotoPersonal { get; set; }
        public string Nombre { get; set; }
        public string Departamento { get; set; }
        public DateTime FechaIngreso { get; set; }

        // ===================== CURSOS =====================
        public int TotalCursos { get; set; }
        public int CursosTerminados { get; set; }
        public int CursosEnProceso { get; set; }
        public int CursosSinIniciar { get; set; }

        // ===================== INCIDENTES =====================
        public int TotalIncidentes { get; set; }
        public DateTime? UltimoIncidente { get; set; }
        public string RazonMasComun { get; set; }

        // ===================== VACACIONES =====================
        public int DiasVacacionesTomados { get; set; }
        public int TotalSolicitudesVacaciones { get; set; }
        public DateTime? UltimaVacacion { get; set; }
    }
}