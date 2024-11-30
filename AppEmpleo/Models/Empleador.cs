using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppEmpleo.Models;

public partial class Empleador
{
    [Key]
    public int EmpleadorId { get; set; }

    [ForeignKey("Usuario")]
    public int UsuarioId { get; set; }

    public virtual Usuario Usuario { get; set; } = null!;

    [ForeignKey("Empresa")]
    public int EmpresaId { get; set; }

    public virtual Empresa Empresa { get; set; } = null!;

    public virtual ICollection<Oferta> Ofertas { get; set; } = new List<Oferta>();
}
