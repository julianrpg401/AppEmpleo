using System;
using System.Collections.Generic;

namespace AppEmpleo.Modelos;

public partial class Empleador
{
    public int IdEmpleador { get; set; }

    public string NombreEmpresa { get; set; } = null!;

    public string Sector { get; set; } = null!;

    public string? EmailEmpresa { get; set; }

    public virtual ICollection<Oferta> Oferta { get; set; } = new List<Oferta>();

    public virtual ICollection<Postulacion> Postulacions { get; set; } = new List<Postulacion>();

    public virtual Usuario? Usuario { get; set; }
}
