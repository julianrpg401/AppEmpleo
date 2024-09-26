using System;
using System.Collections.Generic;

namespace AppEmpleo.Modelos;

public partial class Candidato
{
    public int IdCandidato { get; set; }

    public byte[]? Curriculum { get; set; }

    public virtual ICollection<Postulacion> Postulacions { get; set; } = new List<Postulacion>();

    public virtual Usuario? Usuario { get; set; }
}
