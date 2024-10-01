using System;
using System.Collections.Generic;

namespace AppEmpleo.Models;

public partial class OfertaHabilidad
{
    public int Id { get; set; }

    public int OfertaEmpleoId { get; set; }

    public int HabilidadId { get; set; }

    public virtual Habilidad Habilidades { get; set; } = null!;

    public virtual Oferta OfertaEmpleo { get; set; } = null!;
}
