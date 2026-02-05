namespace RecursosHumanos.Models.ViewModels.Empleados
{
    public class EmpleadosCookiesValidationVM
    {
        public int IdPersonal { get; set; }
        public string NombreCompleto { get; set; }
        public string Departamento { get; set; }
        public string TipoEmpleado { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public bool EstaActivo { get; set; }
        public DateTime? FechaIngreso { get; set; }
    }
}
