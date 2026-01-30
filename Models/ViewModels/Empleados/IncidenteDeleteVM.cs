namespace RecursosHumanos.Models.ViewModels.Empleados
{
    public class IncidenteDeleteVM
    {
        public int Id { get; set; }
        public int IdPersona { get; set; }

        public string NombreIncidente { get; set; }
        public string Descripcion { get; set; }
        public string Razon { get; set; }
        public DateTime FechaIncidente { get; set; }
        public string Evidencia { get; set; }
    }
}
