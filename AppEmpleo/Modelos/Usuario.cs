using System;
using System.Collections.Generic;

namespace AppEmpleo.Modelos;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string NombreUsuario { get; set; } = null!;

    public string ApellidoUsuario { get; set; } = null!;

    public string EmailUsuario { get; set; } = null!;

    public string ClaveUsuario { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public DateOnly FechaNacimiento { get; set; }

    public virtual Empleador IdUsuario1 { get; set; } = null!;

    public virtual Candidato IdUsuarioNavigation { get; set; } = null!;
}
