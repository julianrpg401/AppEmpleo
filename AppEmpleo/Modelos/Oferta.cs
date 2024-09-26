using System;
using System.Collections.Generic;

namespace AppEmpleo.Modelos;

public partial class Oferta
{
    public int IdOferta { get; set; }

    public int CodEmpleador { get; set; }

    public int CodCategoria { get; set; }

    public string NombreOferta { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public string Requisitos { get; set; } = null!;

    public string Modalidad { get; set; } = null!;

    public string? Ubicacion { get; set; }

    public DateOnly FechaInicio { get; set; }

    public DateOnly FechaCierre { get; set; }

    public virtual Empleador CodEmpleadorNavigation { get; set; } = null!;

    public virtual Categoria CodcategoriaNavigation { get; set; } = null!;

    public virtual ICollection<Postulacion> Postulacions { get; set; } = new List<Postulacion>();
}
