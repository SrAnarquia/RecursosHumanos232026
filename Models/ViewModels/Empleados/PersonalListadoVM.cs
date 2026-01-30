using Microsoft.AspNetCore.Mvc.Rendering;

namespace RecursosHumanos.Models.ViewModels.Empleados
{
    public class PersonalListadoVM
    {
        // ===================== LISTADO =====================
        public List<PersonalListadoVM> Datos { get; set; }

        // ===================== DATOS PERSONA =====================
        public int IdPersonal { get; set; }
        public byte[] FotoPersonal { get; set; }
        public string Nombre { get; set; }
        public string Departamento { get; set; }
        public string TipoEmpleado { get; set; }
        public string Curp { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Estado { get; set; }

        // ===================== FILTROS =====================
        public string FiltroNombre { get; set; }
        public string FiltroDepartamento { get; set; }
        public string FiltroTipoEmpleado { get; set; }
        public string FiltroEstado { get; set; }


        // COMBOS
        public List<SelectListItem> Departamentos { get; set; }
        public List<SelectListItem> TiposEmpleado { get; set; }
        public List<SelectListItem> Estados { get; set; }



        // ===================== PAGINACIÓN =====================
        public int PaginaActual { get; set; }
        public int TotalPaginas { get; set; }
    }
}
