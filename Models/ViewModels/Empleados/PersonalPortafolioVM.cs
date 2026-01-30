using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;


namespace RecursosHumanos.Models.ViewModels.Empleados
{
    public class PersonalPortafolioVM
    {
        // ===================== PERSONA =====================
        public int IdPersonal { get; set; }
        public byte[] FotoPersonal { get; set; }
        public string Nombre { get; set; }
        public string Departamento { get; set; }
        public string TipoEmpleado { get; set; }
        public string Curp { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Estado { get; set; }

        public string? FiltroCurso { get; set; }
        public DateTime? FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }


        // ===================== CURSOS =====================
        public List<CursoPersonaVM> Cursos { get; set; }

        public int IdEstatus { get; set; }

        public CursoPersonaCreateVM NuevoCurso { get; set; }

        public int TotalPaginas { get; set; }


        public int PaginaActual { get; set; }

    }

    public class CursoPersonaVM
    {
        public int Id { get; set; }
        public string NombreCurso { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFinalizacion { get; set; }
        public string Estatus { get; set; }
        public string Diploma { get; set; }


       



    }
}
