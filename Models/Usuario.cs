using System;
using System.Collections.Generic;

namespace RecursosHumanos.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string Usuario1 { get; set; } = null!;

    public string Contraseña { get; set; } = null!;

    public bool EsAdministrador { get; set; }

    public string Nombre { get; set; } = null!;

    public string Correo { get; set; } = null!;
}
