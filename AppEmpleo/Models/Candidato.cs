using System;
using System.Collections.Generic;

namespace AppEmpleo.Models;

public partial class Candidato
{
    public int Id { get; set; }

    public string? Curp { get; set; }

    public string? Dirección { get; set; }

    public string? Telefono { get; set; }

    public virtual ICollection<Curriculum> Curricula { get; set; } = new List<Curriculum>();

    public virtual Usuario IdNavigation { get; set; } = null!;

    public virtual ICollection<Postulacion> Postulaciones { get; set; } = new List<Postulacion>();
}
