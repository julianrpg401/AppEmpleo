using System;
using System.Collections.Generic;

namespace AppEmpleo.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string ClaveHash { get; set; } = null!;

    public string Rol { get; set; } = null!;

    public DateTime FechaRegistro { get; set; }

    public DateOnly FechaNacimiento { get; set; }

    public virtual Candidato? Candidato { get; set; }

    public virtual Empleador? Empleador { get; set; }
}
