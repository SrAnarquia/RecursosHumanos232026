namespace RecursosHumanos.Models.ViewModels.Empleados
{
    public class PersonalAlarmaVM
    {

        public int IdPersonal { get; set; }
        public byte[] FotoPersonal { get; set; }
        public string Nombre { get; set; }

        public string NombreDepartamento { get; set; }

        public DateTime FechaIngreso { get; set; }
        public DateTime? FechaBaja { get; set; }
        public DateTime? FechaReingreso { get; set; }
        public string Estado { get; set; }
        public DateTime? FechaUltimaLiquidacion { get; set; }
        public int? DiasDesdeUltimaLiquidacion { get; set; }

    }
}
