namespace RecursosHumanos.Models.ViewModels.Empleados
{
    public class DiasEmpleadoEditVM
    {
        // Persona
        public int IdEmpleado { get; set; }
        public byte[] FotoPersonal { get; set; }
        public string Nombre { get; set; }
        public string Departamento { get; set; }
        public string TipoEmpleado { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }

        // Vacaciones
        public int TotalDias { get; set; }
    }
}
