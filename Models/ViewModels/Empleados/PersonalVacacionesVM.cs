namespace RecursosHumanos.Models.ViewModels.Empleados
{
    public class PersonalVacacionesVM
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
        public DateTime? FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }

        // ===================== VACACIONES =====================
        public List<VacacionVM> Vacaciones { get; set; } = new();

        // ===================== PAGINACIÓN =====================
        public int PaginaActual { get; set; }
        public int TotalPaginas { get; set; }
    }

    public class VacacionVM
    {
        public int Id { get; set; }
        public string Detalles { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFinalizacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Estatus { get; set; }
    }
}
