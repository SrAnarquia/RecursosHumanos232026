namespace RecursosHumanos.Models.ViewModels.Vacaciones
{
    public class VacacionAprobarVM
    {
        public int Id { get; set; }

        public string Nombre { get; set; }
        public string Departamento { get; set; }

        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFinalizacion { get; set; }

        public string Razon { get; set; }
        public string Detalles { get; set; }
    }
}
