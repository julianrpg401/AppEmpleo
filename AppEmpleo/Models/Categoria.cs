using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppEmpleo.Models;

public partial class Categoria
{
    [Key]
    public int CategoriaId { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Oferta> Ofertas { get; set; } = new List<Oferta>();
}
