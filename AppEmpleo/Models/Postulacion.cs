using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppEmpleo.Models;

public partial class Postulacion
{
    [Key]
    public int PostulacionId { get; set; }

    [ForeignKey("OfertaEmpleo")]
    public int OfertaEmpleoId { get; set; }

    [ForeignKey("Candidato")]
    public int CandidatoId { get; set; }

    [ForeignKey("Curriculum")]
    public int CurriculumId { get; set; }

    public DateTime FechaPostulacion { get; set; }

    public virtual Candidato Candidato { get; set; } = null!;

    public virtual Curriculum Curriculum { get; set; } = null!;

    public virtual Oferta OfertaEmpleo { get; set; } = null!;
}
