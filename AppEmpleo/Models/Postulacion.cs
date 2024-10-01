using System;
using System.Collections.Generic;

namespace AppEmpleo.Models;

public partial class Postulacion
{
    public int Id { get; set; }

    public int OfertaEmpleoId { get; set; }

    public int CandidatoId { get; set; }

    public int CurriculumId { get; set; }

    public DateTime FechaPostulacion { get; set; }

    public virtual Candidato Candidato { get; set; } = null!;

    public virtual Curriculum Curriculum { get; set; } = null!;

    public virtual Oferta OfertaEmpleo { get; set; } = null!;
}
