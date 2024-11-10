using System;
using System.Collections.Generic;

namespace AppEmpleo.Models;

public partial class Empresa
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Dirección { get; set; }

    public string? SitioWeb { get; set; }

    public string? Telefono { get; set; }

    public virtual ICollection<Empleador> Empleadores { get; set; } = new List<Empleador>();
}
