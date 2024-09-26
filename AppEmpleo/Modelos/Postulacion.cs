using System;
using System.Collections.Generic;

namespace AppEmpleo.Modelos;

public partial class Postulacion
{
    public int CodPostulacion { get; set; }

    public int CodOferta { get; set; }

    public int CodCandidato { get; set; }

    public int CodEmpleador { get; set; }

    public byte[]? Curriculum { get; set; }

    public DateOnly FechaSubida { get; set; }

    public virtual Candidato CodCandidatoNavigation { get; set; } = null!;

    public virtual Empleador CodEmpleadorNavigation { get; set; } = null!;

    public virtual Oferta CodOfertaNavigation { get; set; } = null!;
}
