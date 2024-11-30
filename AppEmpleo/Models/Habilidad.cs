using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppEmpleo.Models;

public partial class Habilidad
{
    [Key]
    public int HabilidadId { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<OfertaHabilidad> OfertaHabilidades { get; set; } = new List<OfertaHabilidad>();
}
