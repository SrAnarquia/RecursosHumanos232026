using System;
using System.Collections.Generic;

namespace RecursosHumanos.Models;

public partial class UsuarioEspejo
{
    public string? Id { get; set; }

    public string? Usuario { get; set; }

    public string? Contraseña { get; set; }

    public string? EsAdministrador { get; set; }

    public string? Nombre { get; set; }

    public string? Correo { get; set; }
}
