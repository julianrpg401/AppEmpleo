using System;
using System.Collections.Generic;

namespace AppEmpleo.Modelos;

public partial class Categoria
{
    public int IdCategoria { get; set; }

    public string NombreCategoria { get; set; } = null!;

    public virtual ICollection<Oferta> Oferta { get; set; } = new List<Oferta>();
}
