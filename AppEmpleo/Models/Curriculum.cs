using System;
using System.Collections.Generic;

namespace AppEmpleo.Models;

public partial class Curriculum
{
    public int Id { get; set; }

    public int CandidatoId { get; set; }

    public string NombreArchivo { get; set; } = null!;

    public string RutaArchivo { get; set; } = null!;

    public DateTime FechaCarga { get; set; }

    public bool EsPreferido { get; set; }

    public virtual Candidato Candidato { get; set; } = null!;

    public virtual ICollection<Postulacion> Postulaciones { get; set; } = new List<Postulacion>();
}
