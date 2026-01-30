namespace RecursosHumanos.Models.ViewModels.Empleados
{

    public class CursoPersonaDeleteVM
    {
        public int Id { get; set; }
        public int IdPersona { get; set; }

        public string NombreCurso { get; set; }
        public string Descripcion { get; set; }

        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFinalizacion { get; set; }

        public string Estatus { get; set; }
    }
}
