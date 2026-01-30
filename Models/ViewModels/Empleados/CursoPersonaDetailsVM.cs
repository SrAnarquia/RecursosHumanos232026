namespace RecursosHumanos.Models.ViewModels.Empleados
{
    public class CursoPersonaDetailsVM
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
