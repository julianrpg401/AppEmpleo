using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppEmpleo.Models;

public partial class OfertaHabilidad
{
    [Key]
    public int OfertaHabilidadId { get; set; }

    [ForeignKey("OfertaEmpleo")]
    public int OfertaEmpleoId { get; set; }

    [ForeignKey("Habilidad")]
    public int HabilidadId { get; set; }

    public virtual Habilidad Habilidad { get; set; } = null!;

    public virtual Oferta OfertaEmpleo { get; set; } = null!;
}
