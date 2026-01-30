using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;


namespace RecursosHumanos.Models.ViewModels.Reclutamiento
{
    public class ReclutamientoIndexVM
    {

        public List<DatosReclutamiento> Datos { get; set; }


        //Filtros
        [Required(ErrorMessage = "Rellenar campo")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Rellenar campo")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "Rellenar campo")]
        public int? IdDepartamento { get;set; }

        [Required(ErrorMessage = "Rellenar campo")]
        public int? IdReclutador { get;set;}
        [Required(ErrorMessage = "Rellenar campo")]
        public int? IdSexo { get; set;   }
        [Required(ErrorMessage = "Rellenar campo")]
        public int? IdEstatus { get; set; }
        [Required(ErrorMessage = "Rellenar campo")]
        public int? IdFuente { get; set; }

        
        public DateTime? FechaDesde { get; set; }

        public DateTime? FechaHasta { get; set; }

        
        //Paginación
        public int PaginaActual { get; set; }
        public int? TotalPaginas { get; set; }

        //Formulario
        public DatosReclutamiento Nuevo { get; set; }  

        //Models
        public IEnumerable<SelectListItem> Empresas { get; set; }
        public IEnumerable<SelectListItem> Bases { get; set; }
        public IEnumerable<SelectListItem> Fuentes { get; set; }
        public IEnumerable<SelectListItem> Estatus { get; set; }
        public IEnumerable<SelectListItem> Posiciones { get; set; }
        public IEnumerable<SelectListItem> Sexos { get; set; }
        public IEnumerable<SelectListItem> Reclutadores { get; set; }

        public bool MostrarModal { get; set; }


    }
}
