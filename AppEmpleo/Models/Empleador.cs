using System;
using System.Collections.Generic;

namespace AppEmpleo.Models;

public partial class Empleador
{
    public int Id { get; set; }

    public int EmpresaId { get; set; }

    public virtual Empresa Empresa { get; set; } = null!;

    public virtual Usuario IdNavigation { get; set; } = null!;

    public virtual ICollection<Oferta> Ofertas { get; set; } = new List<Oferta>();
}
