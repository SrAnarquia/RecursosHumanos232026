namespace RecursosHumanos.Models.ViewModels.Vacaciones
{
    public class VacacionIndexVM
    {
        // Datos
        public List<Vacacion> Datos { get; set; }

        // Filtros
        public string Nombre { get; set; }
        public string Departamento { get; set; }
        public DateTime? FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }

        // Paginación
        public int PaginaActual { get; set; }
        public int TotalPaginas { get; set; }
    }
}
