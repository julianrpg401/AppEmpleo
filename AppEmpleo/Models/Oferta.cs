using System;
using System.Collections.Generic;

namespace AppEmpleo.Models;

public partial class Oferta
{
    public int Id { get; set; }

    public int EmpleadorId { get; set; }

    public string NombreOferta { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public DateOnly FechaInicio { get; set; }

    public DateOnly FechaCierre { get; set; }

    public decimal Salario { get; set; }

    public string ModalidadTrabajo { get; set; } = null!;

    public string UbicacionTrabajo { get; set; } = null!;

    public string CategoriaCodigo { get; set; } = null!;

    public virtual Categoria CategoriaCodigoNavigation { get; set; } = null!;

    public virtual Empleador Empleador { get; set; } = null!;

    public virtual ICollection<OfertaHabilidad> OfertaHabilidades { get; set; } = new List<OfertaHabilidad>();

    public virtual ICollection<Postulacion> Postulaciones { get; set; } = new List<Postulacion>();
}
