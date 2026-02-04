namespace RecursosHumanos.Models.ViewModels.Empleados
{
    public class PersonalAlarmaDetalleVM
    {
        /* ===== Identificación ===== */
        public int IdPersonal { get; set; }
        public byte[] FotoPersonal { get; set; }
        public string Nombre { get; set; }
        public string NombreDepartamento { get; set; }
        public string Estado { get; set; }

        /* ===== Información laboral ===== */
        public DateTime? FechaIngreso { get; set; }
        public DateTime? FechaBaja { get; set; }
        public DateTime? FechaReingreso { get; set; }
        public int AntiguedadAnios { get; set; }

        /* ===== Información de liquidaciones ===== */
        public DateTime? FechaUltimaLiquidacion { get; set; }
        public int? DiasDesdeUltimaLiquidacion { get; set; }
        public int TotalLiquidaciones { get; set; }
        public int TotalViajes { get; set; }
        public decimal TotalPagado { get; set; }
        public decimal TotalGastos { get; set; }
        public DateTime? UltimaFechaPago { get; set; }

        /* ===== Información de contacto ===== */
        public string TelefonoPersonal { get; set; }
        public string Email { get; set; }
        public string TelefonoEmergencia { get; set; }
        public string Direccion { get; set; }
    }
}
