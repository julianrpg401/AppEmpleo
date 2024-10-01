using System;
using System.Collections.Generic;

namespace AppEmpleo.Models;

public partial class Habilidad
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<OfertaHabilidad> OfertaHabilidades { get; set; } = new List<OfertaHabilidad>();
}
