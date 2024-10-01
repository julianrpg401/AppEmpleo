using System;
using System.Collections.Generic;

namespace AppEmpleo.Models;

public partial class Categoria
{
    public string Codigo { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Oferta> Ofertas { get; set; } = new List<Oferta>();
}
